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
    public class StayFurtherInformationService
    {
        private readonly ILogger<StayFurtherInformationService> logger;
        private readonly Context context;
        private readonly IConfiguration config;

        public StayFurtherInformationService(ILogger<StayFurtherInformationService> logger, Context context, IConfiguration config)
        {
            this.logger = logger;
            this.context = context;
            this.config = config;
        }

        public async Task AddListFurtherInformations(List<StayFurtherInformation> list, int? stayId)
        {
            if (list == null || list.Count == 0)
                throw new AnyStayFurtherInformationException();

            if (stayId == null)
                throw new ArgumentNullException();

            foreach (var furtherInformation in list)
            {
                CheckFurtherInformation(furtherInformation);

                    var newFurtherInformation = new StayFurtherInformation
                    {
                        StayId = stayId,
                        StartDate = furtherInformation.StartDate.Value.AddHours(2),
                        EndDate = furtherInformation.EndDate.Value.AddHours(2),
                        WithTransport = furtherInformation.WithTransport,
                        StartCity = furtherInformation.StartCity,
                        Price = furtherInformation.Price,
                        RedirectionLink = furtherInformation.RedirectionLink
                    };
                context.StayFurtherInformation.Add(newFurtherInformation);
            }
            await context.SaveChangesAsync();
        }

        public async Task<List<StayFurtherInformation>> GetStayFurtherInformationsList(int? stayId)
        {
            if (stayId == null)
                throw new ArgumentNullException();

            return await context.StayFurtherInformation
                .Where(sfi => sfi.StayId == stayId && sfi.StartDate >= DateTime.Now)
                .AsNoTracking()
                .ToListAsync();
        }

        public static void CheckFurtherInformation(StayFurtherInformation information)
        {
            if (information.StartDate == null)
                throw new NoStayFurtherInformationStartDateException();

            if(information.EndDate == null)
                throw new NoStayFurtherInformationEndDateException();

            if (information.WithTransport == null)
                throw new NoStayFurtherInformationWithTransportException();

            if (string.IsNullOrEmpty(information.StartCity))
                throw new NoStayFurtherInformationStartCityException();

            if (information.Price == null)
                throw new NoStayFurtherInformationPriceException();

            if (string.IsNullOrEmpty(information.RedirectionLink))
                throw new NoStayFurtherInformationRedirectionLinkException();
        }
    }
}
