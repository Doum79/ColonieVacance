using GoogleMaps.LocationServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using Model.Exceptions;
using Provider.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class StayTagService
    {
        private readonly ILogger<StayTagService> logger;
        private readonly Context context;
        private readonly IConfiguration config;

        public StayTagService(ILogger<StayTagService> logger, Context context, IConfiguration config)
        {
            this.logger = logger;
            this.context = context;
            this.config = config;
        }

        public async Task<StayTag> GetStayTag(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();

            return await context.StayTag.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Tag>> ListTagOfStay(int? stayId)
        {
            if (stayId == null)
                throw new ArgumentNullException();

            var stay = await context.Stay.FirstOrDefaultAsync(s => s.Id == stayId);

            var stayTags = await context.StayTag
                .Include(st => st.Tag)
                .Where(st => st.StayId == stayId)
                .Select(st => st.Tag)
                .AsNoTracking()
                .ToListAsync();

            //Ajout de l'étiquette "Populaire"
            AddPopularTag(stayTags, stay);
            //Ajout de l'étiquette "Promotion"
            //AddPromotionTag(stayTags, stay);

            return stayTags;
        }

        public async Task<StayTag> AddStayTag(int? tagId, int? stayId)
        {
            if (tagId == null || stayId == null)
                throw new ArgumentNullException();

            var stayTag = new StayTag
            {
                TagId = tagId,
                StayId = stayId
            };

            context.StayTag.Add(stayTag);
            await context.SaveChangesAsync();

            return stayTag;
        }

        public async Task<List<Tag>> AddListStayTagsForStay(List<Tag> tags, int? stayId)
        {
            if (tags.Count == 0)
                return tags;

            if (stayId == null)
                throw new ArgumentNullException();

            foreach (var tag in tags)
            {
                if(tag.Id != null)
                    await AddStayTag(tag.Id, stayId);
            }

            return tags;
        }

        public async Task RemoveStayTagsByStay(int? stayId)
        {
            if (stayId == null)
                throw new ArgumentNullException();

            var stayTagsList = await context.StayTag.Where(sa => sa.StayId == stayId).AsNoTracking().ToListAsync();

            foreach (var stayTag in stayTagsList)
            {
                await RemoveStayTag(stayTag.Id);
            }
        }

        public async Task RemoveStayTag(int? stayTagId)
        {
            if (stayTagId == null)
                throw new ArgumentNullException();

            var stayTag = await GetStayTag(stayTagId);

            context.StayTag.Remove(stayTag);
            await context.SaveChangesAsync();
        }

        private void AddPopularTag(IList<Tag> tags, Stay stay)
        {
            //Si le séjour a un nombre de clique égale ou supérieur à la constante alors on ajoute l'étiquette
            if(stay.ViewCount >= Stay.POPULAR)
            {
                var popularTag = new Tag { Label = "Populaire" };
                tags.Add(popularTag);
            }

        }

        //private void AddPromotionTag(IList<Tag> tags, Stay stay)
        //{
        //    //Si il n'y a pas de nouveau prix on n'ajoute pas d'étiquette
        //    if (stay.NewPrice == null)
        //        return;

        //    //Si le nouveau prix du séjour est inférieur au prix de base alors on ajoute l'étiquette
        //    if (stay.Price > stay.NewPrice)
        //    {
        //        //On calcul la promotion
        //        var promotion = UtilsFunctionsService.DiscountPercentage(stay.Price.Value, stay.NewPrice.Value);
        //        //On créé l'étiquette
        //        var promotionTag = new Tag { Label = $"Promotion -{promotion}%" };
        //        //On ajoute l'étiquette à la liste
        //        tags.Add(promotionTag);
        //    }
        //}


    }
}
