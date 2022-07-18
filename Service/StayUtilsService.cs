using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using Provider.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace Service
{
    public class StayUtilsService
    {
        private readonly ILogger<StayUtilsService> logger;
        private readonly Context context;
        private readonly IConfiguration config;
        private DocumentService documentService;

        public StayUtilsService(ILogger<StayUtilsService> logger, Context context, IConfiguration config,
            DocumentService documentService) { 
            this.logger = logger;
            this.context = context;
            this.config = config;
            this.documentService = documentService;
        }

        //TEAM
        public async Task AddListTeam(List<StayTeam> list, int? stayId)
        {
            if (list == null || list.Count == 0)
                return;

            if (stayId == null)
                throw new ArgumentNullException();

            foreach (var partner in list)
            {
                if(!string.IsNullOrEmpty(partner.PartnerName) && !string.IsNullOrWhiteSpace(partner.PartnerName))
                {
                    var stayTeam = new StayTeam
                    {
                        PartnerName = partner.PartnerName,
                        StayId = stayId
                    };
                    context.StayTeam.Add(stayTeam);
                }
            }
            await context.SaveChangesAsync();
        }

        public async Task RemoveAllTeam(int? stayId)
        {
            if (stayId == null)
                throw new ArgumentNullException();

            var list = context.StayTeam.Where(st => st.StayId == stayId);
            context.StayTeam.RemoveRange(list);
            await context.SaveChangesAsync();
        }

        //EQUIPMENT
        public async Task AddListEquipment(List<StayEquipment> list, int? stayId)
        {
            if (list == null || list.Count == 0)
                return;

            if (stayId == null)
                throw new ArgumentNullException();

            foreach (var equipment in list)
            {
                if (!string.IsNullOrEmpty(equipment.Label) && !string.IsNullOrWhiteSpace(equipment.Label))
                {
                    var stayEquipment = new StayEquipment
                    {
                        Label = equipment.Label,
                        StayId = stayId,
                        IsIncluded = equipment.IsIncluded
                    };
                    context.StayEquipment.Add(stayEquipment);
                }
            }
            await context.SaveChangesAsync();
        }

        public async Task RemoveAllEquipment(int? stayId)
        {
            if (stayId == null)
                throw new ArgumentNullException();

            var list = await context.StayEquipment.Where(st => st.StayId == stayId).AsNoTracking().ToListAsync();
            context.StayEquipment.RemoveRange(list);
            await context.SaveChangesAsync();
        }

        //ACCESS
        public async Task AddListAccesses(List<StayAccess> list, int? stayId)
        {
            if (list == null || list.Count == 0)
                return;

            if (stayId == null)
                throw new ArgumentNullException();

            foreach (var access in list)
            {
                if (!string.IsNullOrEmpty(access.Label) && !string.IsNullOrWhiteSpace(access.Label))
                {
                    var stayEquipment = new StayAccess
                    {
                        Label = access.Label,
                        StayId = stayId,
                    };
                    context.StayAccess.Add(stayEquipment);
                }
            }
            await context.SaveChangesAsync();
        }

        public async Task RemoveAllAccess(int? stayId)
        {
            if (stayId == null)
                throw new ArgumentNullException();

            var list = await context.StayAccess.Where(st => st.StayId == stayId).AsNoTracking().ToListAsync();
            context.StayAccess.RemoveRange(list);
            await context.SaveChangesAsync();
        }

        //PICTURE
        public async Task AddListPictures(List<StayPicture> list, int? stayId)
        {
            if (list == null || list.Count == 0)
                return;

            if (stayId == null)
                throw new ArgumentNullException();

            foreach (var picture in list)
            {
                var stayPicture = new StayPicture
                {
                    StayId = stayId,
                    PictureName = picture.PictureName,
                    PictureUrl = picture.PictureUrl
            };
                context.StayPicture.Add(stayPicture);
            }

            await context.SaveChangesAsync();
        }

        public async Task RemoveAllPictures(int? stayId)
        {
            if (stayId == null)
                throw new ArgumentNullException();

            var list = await context.StayPicture.Where(st => st.StayId == stayId).AsNoTracking().ToListAsync();

            foreach (var stayPicture in list)
            {
                documentService.DeleteSharedDocumentByUrl(stayPicture.PictureUrl);
                context.StayPicture.Remove(stayPicture);
            }

            await context.SaveChangesAsync();
        }

        public async Task<List<StayPicture>> GetStayPicturesList(int? stayId)
        {
            if (stayId == null)
                throw new ArgumentNullException();

            return await context.StayPicture.Where(st => st.StayId == stayId).AsNoTracking().ToListAsync();
        }

        public async Task UpdatePicturesList(List<StayPicture> list, int? stayId)
        {
            if (stayId == null)
                throw new ArgumentNullException();

            var lastList = await context.StayPicture.Where(st => st.StayId == stayId).AsNoTracking().ToListAsync();

            lastList = lastList.Where(p => list.Any(p2 => p.Id == p2.Id)).ToList();

            foreach (var stayPicture in lastList)
            {
                documentService.DeleteSharedDocumentByUrl(stayPicture.PictureUrl);
                context.StayPicture.Remove(stayPicture);
            }

            list = list.Where(p => p.Id == null).ToList();

            //foreach (var picture in list)
            //{
            //    byte[] pictureBytes = Convert.FromBase64String(picture.PictureUrl.Split("base64,")[1]);
            //    Stream stream = new MemoryStream(pictureBytes);

            //    var stayPicture = new StayPicture
            //    {
            //        StayId = stayId,
            //        PictureName = picture.PictureName,
            //        PictureUrl = await documentService.UploadSharedFile(stream, UtilsFunctions.RemoveDiacritics(picture.PictureName))
            //    };
            //    context.StayPicture.Add(stayPicture);
            //}

            //await context.SaveChangesAsync();
        }
    }
}
