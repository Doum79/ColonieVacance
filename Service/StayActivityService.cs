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
    public class StayActivityService
    {
        private readonly ILogger<StayActivityService> logger;
        private readonly Context context;
        private readonly IConfiguration config;

        public StayActivityService(ILogger<StayActivityService> logger, Context context, IConfiguration config)
        {
            this.logger = logger;
            this.context = context;
            this.config = config;
        }

        public async Task<StayActivity> GetStayActivity(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();

            return await context.StayActivity.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<StayActivity>> ListActivityOfStay(int? stayId)
        {
            if (stayId == null)
                throw new ArgumentNullException();

            return await context.StayActivity
                .Include(sa => sa.Activity)
                .Where(sa => sa.StayId == stayId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<StayActivity> AddStayActivity(int? activityId, int? stayId)
        {
            if (activityId == null || stayId == null)
                throw new ArgumentNullException();

            var stayActivity = new StayActivity
            {
                ActivityId = activityId,
                StayId = stayId
            };

            context.StayActivity.Add(stayActivity);
            await context.SaveChangesAsync();

            return stayActivity;
        }

        public async Task<List<StayActivity>> AddListStayActivitiesForStay(List<StayActivity> activities, int? stayId)
        {
            if (activities.Count == 0)
                return activities;

            if (stayId == null)
                throw new ArgumentNullException();

            foreach (var activity in activities)
            {
                await AddStayActivity(activity.ActivityId, stayId);
            }

            return activities;
        }

        public async Task RemoveStayActivitiesByStay(int? stayId)
        {
            if (stayId == null)
                throw new ArgumentNullException();

            var stayActivitiesList = await context.StayActivity.Where(sa => sa.StayId == stayId).AsNoTracking().ToListAsync();

            foreach (var stayActivity in stayActivitiesList)
            {
                await RemoveStayActivity(stayActivity.Id);
            }
        }

        public async Task RemoveStayActivity(int? stayActivitiesId)
        {
            if (stayActivitiesId == null)
                throw new ArgumentNullException();

            var stayActivity = await GetStayActivity(stayActivitiesId);

            context.StayActivity.Remove(stayActivity);
            await context.SaveChangesAsync();
        }
    }
}
