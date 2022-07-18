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
    public class StayThematicService
    {
        private readonly ILogger<StayThematicService> logger;
        private readonly Context context;
        private readonly IConfiguration config;

        public StayThematicService(ILogger<StayThematicService> logger, Context context, IConfiguration config)
        {
            this.logger = logger;
            this.context = context;
            this.config = config;
        }

        public async Task<StayThematic> GetStayThematic(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();

            return await context.StayThematic.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<StayThematic>> ListThematicOfStay(int? stayId)
        {
            if (stayId == null)
                throw new ArgumentNullException();

            return await context.StayThematic
                .Include(sa => sa.Thematic)
                .Where(sa => sa.StayId == stayId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<StayThematic> AddStayThematic(int? thematicId, int? stayId)
        {
            if (thematicId == null || stayId == null)
                throw new ArgumentNullException();

            var stayThematic = new StayThematic
            {
                ThematicId = thematicId,
                StayId = stayId
            };

            context.StayThematic.Add(stayThematic);
            await context.SaveChangesAsync();

            return stayThematic;
        }

        public async Task<List<StayThematic>> AddListStayThematicsForStay(List<StayThematic> thematics, int? stayId)
        {
            if (thematics.Count == 0)
                return thematics;

            if (stayId == null)
                throw new ArgumentNullException();

            foreach (var thematic in thematics)
            {
                await AddStayThematic(thematic.ThematicId, stayId);
            }

            return thematics;
        }

        public async Task RemoveStayThematicsByStay(int? stayId)
        {
            if (stayId == null)
                throw new ArgumentNullException();

            var stayThematicsList = await context.StayThematic.Where(sa => sa.StayId == stayId).AsNoTracking().ToListAsync();

            foreach (var stayThematic in stayThematicsList)
            {
                await RemoveStayThematic(stayThematic.Id);
            }
        }

        public async Task RemoveStayThematic(int? stayThematicId)
        {
            if (stayThematicId == null)
                throw new ArgumentNullException();

            var stayThematic = await GetStayThematic(stayThematicId);

            context.StayThematic.Remove(stayThematic);
            await context.SaveChangesAsync();
        }
    }
}
