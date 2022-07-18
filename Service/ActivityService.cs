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
    public class ActivityService
    {
        private readonly ILogger<ActivityService> logger;
        private readonly Context context;
        private readonly IConfiguration config;

        public ActivityService(ILogger<ActivityService> logger, Context context, IConfiguration config) { 
            this.logger = logger;
            this.context = context;
            this.config = config;
        }
        
        public async Task<Activity> GetActivity(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();

            return await context.Activity.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Activity>> ListActivity()
        {
            return await context.Activity.AsNoTracking().ToListAsync();
        }

        public async Task<Activity> AddActivity(Activity activity)
        {
            if (activity == null)
                throw new ArgumentNullException();

            CheckActivity(activity);

            context.Activity.Add(activity);
            await context.SaveChangesAsync();

            return activity;
        }

        public async Task<Activity> UpdateActivity(Activity activity)
        {
            if (activity == null && activity.Id == null)
                throw new ArgumentNullException();

            CheckActivity(activity);

            var oldActivity = await GetActivity(activity.Id);

            oldActivity.Label = activity.Label;

            await context.SaveChangesAsync();

            return activity;
        }


        public async Task RemoveActivity(int? activityId)
        {
            if (activityId == null)
                throw new ArgumentNullException();

            var activity = await GetActivity(activityId);

            context.Activity.Remove(activity);
            await context.SaveChangesAsync();
        }


        private void CheckActivity(Activity activity)
        {
            if (string.IsNullOrEmpty(activity.Label))
                throw new ActivityLabelException();
        }
    }
}
