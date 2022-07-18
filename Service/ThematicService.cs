using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using Model.Exceptions;
using Provider.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class ThematicService
    {
        private readonly ILogger<ThematicService> logger;
        private readonly Context context;
        private readonly IConfiguration config;

        public ThematicService(ILogger<ThematicService> logger, Context context, IConfiguration config) { 
            this.logger = logger;
            this.context = context;
            this.config = config;
        }
        
        public async Task<Thematic> GetThematic(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();

            return await context.Thematic.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Thematic>> ListThematic()
        {
            return await context.Thematic.AsNoTracking().ToListAsync();
        }

        public async Task<Thematic> AddThematic(Thematic thematic)
        {
            if (thematic == null)
                throw new ArgumentNullException();

            CheckThematic(thematic);

            context.Thematic.Add(thematic);
            await context.SaveChangesAsync();

            return thematic;
        }

        public async Task<Thematic> UpdateThematic(Thematic thematic)
        {
            if (thematic == null && thematic.Id == null)
                throw new ArgumentNullException();

            CheckThematic(thematic);

            var oldThematic = await GetThematic(thematic.Id);

            oldThematic.Label = thematic.Label;

            await context.SaveChangesAsync();

            return thematic;
        }


        public async Task RemoveThematic(int? thematicId)
        {
            if (thematicId == null)
                throw new ArgumentNullException();

            var thematic = await GetThematic(thematicId);

            context.Thematic.Remove(thematic);
            await context.SaveChangesAsync();
        }


        private void CheckThematic(Thematic thematic)
        {
            if (string.IsNullOrEmpty(thematic.Label))
                throw new ThematicLabelException();
        }
    }
}
