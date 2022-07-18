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
    public class StayService
    {
        private readonly ILogger<StayService> logger;
        private readonly Context context;
        private readonly IConfiguration config;
        private readonly StayActivityService stayActivityService;
        private readonly StayThematicService stayThematicService;
        private readonly StayTagService stayTagService;
        private readonly StayUtilsService stayUtilsService;
        private readonly StayFurtherInformationService stayFurtherInformationService;

        public StayService(ILogger<StayService> logger, Context context, IConfiguration config, StayActivityService stayActivityService,
            StayThematicService stayThematicService, StayTagService stayTagService, StayUtilsService stayUtilsService,
            StayFurtherInformationService stayFurtherInformationService)
        {
            this.context = context;
            this.config = config;
            this.stayActivityService = stayActivityService;
            this.stayThematicService = stayThematicService;
            this.stayTagService = stayTagService;
            this.stayUtilsService = stayUtilsService;
            this.stayFurtherInformationService = stayFurtherInformationService;
        }

        public async Task<Stay> GetStay(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();

            var stay = await context.Stay
                .Include(s => s.Structure)
                .Include(s => s.PartnersList)
                .Include(s => s.EquipmentsList)
                .Include(s => s.AccessesList)
                .Include(s => s.PicturesList)
                .Include(s => s.FurtherInformationsList)
                .Include(s => s.ThematicsList).ThenInclude(st => st.Thematic)
                .Include(s => s.ActivitiesList).ThenInclude(sa => sa.Activity)
                .FirstOrDefaultAsync(s => s.Id == id);
            return stay;
        }

        public async Task<List<Stay>> StayList()
        {
            return await context.Stay
                .Include(s => s.FurtherInformationsList)
                .Include(s => s.PicturesList)
                .Include(s => s.PartnersList)
                .Include(s => s.EquipmentsList)
                .Include(s => s.AccessesList)
                .Include(s => s.ThematicsList)
                .Include(s => s.ActivitiesList)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Stay>> StayListByStructure(int? structureId)
        {
            return await context.Stay
                .Where(s => s.StructureId == structureId)
                .Include(s => s.FurtherInformationsList)
                .Include(s => s.PicturesList)
                .Include(s => s.PartnersList)
                .Include(s => s.EquipmentsList)
                .Include(s => s.AccessesList)
                .Include(s => s.ThematicsList)
                .Include(s => s.ActivitiesList)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Stay> CreateStay(Stay newStay, Structure currentStructure)
        {
            if (newStay == null || currentStructure.Id == null)
                throw new ArgumentNullException();

            CheckStay(newStay);
            newStay.FurtherInformationsList.ForEach(sf => StayFurtherInformationService.CheckFurtherInformation(sf));

            var dStay = new Stay
            {
                Title = newStay.Title,
                StructureId = currentStructure.Id,
                MinYear = newStay.MinYear,
                MaxYear = newStay.MaxYear,
                Abstract = newStay.Abstract,
                Description = newStay.Description,
                Program = newStay.Program,
                CreatedDate = DateTime.Now,
                Housing = newStay.Housing,
                MoreInformations = newStay.MoreInformations,
                Street = newStay.Street,
                PostCode = newStay.PostCode,
                City = newStay.City,
                State = newStay.State,
                Country = newStay.Country,
                Latitude = newStay.Latitude,
                Longitude = newStay.Longitude,
                Phone = newStay.Phone,
                ViewCount = 0
            };

            context.Stay.Add(dStay);
            await context.SaveChangesAsync();

            await stayActivityService.AddListStayActivitiesForStay(newStay.ActivitiesList, dStay.Id);
            await stayThematicService.AddListStayThematicsForStay(newStay.ThematicsList, dStay.Id);
            await stayUtilsService.AddListTeam(newStay.PartnersList, dStay.Id);
            await stayUtilsService.AddListEquipment(newStay.EquipmentsList, dStay.Id);
            await stayUtilsService.AddListAccesses(newStay.AccessesList, dStay.Id);
            await stayUtilsService.AddListPictures(newStay.PicturesList, dStay.Id);
            await stayFurtherInformationService.AddListFurtherInformations(newStay.FurtherInformationsList, dStay.Id);


            //await stayTagService.AddListStayTagsForStay(tags, dStay.Id);

            return dStay;
        }

        public Task<object> FavoriteStaysListByStructure(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Stay> UpdateStay(Stay stay, Structure currentStructure, List<Activity> activities, List<Thematic> thematics)
        {
            if (stay == null || !stay.Id.HasValue)
                throw new ArgumentNullException();

            CheckStay(stay);

            var dStay = await GetStay(stay.Id.Value);

            dStay.Title = stay.Title;
            dStay.MinYear = stay.MinYear;
            dStay.MaxYear = stay.MaxYear;
            dStay.Abstract = stay.Abstract;
            dStay.Description = stay.Description;
            dStay.Program = stay.Program;

            //dStay.Description = stay.Description;
            //dStay.EndDate = stay.EndDate;
            //dStay.Name = stay.Name;
            //dStay.StartDate = stay.StartDate;
            //dStay.Abstract = stay.Abstract;
            //dStay.Activities = stay.Activities;
            //dStay.City = stay.City;
            //dStay.Country = stay.Country;
            //dStay.NbPlaces = stay.NbPlaces;
            //dStay.OtherInformations = stay.OtherInformations;
            //dStay.Picture1 = stay.Picture1;
            //dStay.Picture2 = stay.Picture2;
            //dStay.Picture3 = stay.Picture3;
            //dStay.Picture4 = stay.Picture4;
            //dStay.Picture5 = stay.Picture5;
            //dStay.Picture6 = stay.Picture6;
            //dStay.Picture7 = stay.Picture7;
            //dStay.PraticLevel = stay.PraticLevel;
            //dStay.Street = stay.Street;
            //dStay.ZipCode = stay.ZipCode;
            //dStay.Latitude = stay.Latitude;
            //dStay.Longitude = stay.Longitude;
            //dStay.Department = stay.Department;
            //dStay.Region = stay.Region;
            //dStay.Status = stay.StartDate > DateTime.Now ? Stay.ACTIVE_STATUS : Stay.EXPIRE_STATUS;
            //dStay.MinYear = stay.MinYear;
            //dStay.MaxYear = stay.MaxYear;

            //if (dStay.Price <= stay.Price)
            //{
            //    dStay.Price = stay.Price;
            //}
            //else
            //{
            //    dStay.NewPrice = stay.Price;
            //}


            //await stayActivityService.RemoveStayActivitiesByStay(dStay.Id);
            //await stayActivityService.AddListStayActivitiesForStay(activities, dStay.Id);

            //await stayThematicService.RemoveStayThematicsByStay(dStay.Id);
            //await stayThematicService.AddListStayThematicsForStay(thematics, dStay.Id);

            //await stayTagService.RemoveStayTagsByStay(dStay.Id);
            //await stayTagService.AddListStayTagsForStay(tags, dStay.Id);

            await context.SaveChangesAsync();
            return dStay;
        }

        public async Task DeleteStay(int? id, Structure currentStructure)
        {
            if (id == null)
                throw new ArgumentNullException();

            var dStay = await GetStay(id.Value);

            if (dStay.StructureId != currentStructure.Id)
                throw new UnauthoriseException();

            await stayActivityService.RemoveStayActivitiesByStay(dStay.Id);
            await stayThematicService.RemoveStayThematicsByStay(dStay.Id);

            context.Stay.Remove(dStay);
            await context.SaveChangesAsync();
        }

        public async Task DeleteStaysList(List<Stay> stays, Structure currentStructure)
        {
            if (stays.Count == 0 || currentStructure == null)
                throw new ArgumentNullException();

            foreach (var stay in stays)
            {
                await DeleteStay(stay.Id, currentStructure);
            }
        }

        public async Task<List<Stay>> FavoriteStaysListByUser(int? userId)
        {
            var list = await context.UserFavoriteStay
                .Include(us => us.Stay)
                .Where(us => us.UserId == userId.Value)
                .AsNoTracking()
                .ToListAsync();
            return list.Select(fs => fs.Stay).ToList();
        }

        public async Task DeleteFavoriteStay(int? stayId, int? userId)
        {
            if (!userId.HasValue || !stayId.HasValue)
                throw new ArgumentNullException();

            var favoriteStay = await context.UserFavoriteStay.FirstOrDefaultAsync(fs => fs.UserId == userId.Value && fs.StayId == stayId.Value);
            context.UserFavoriteStay.Remove(favoriteStay);
            await context.SaveChangesAsync();
        }

        public async Task DeleteFavoriteStaysList(int? userId)
        {
            if (!userId.HasValue)
                throw new ArgumentNullException();

            var list = await FavoriteStaysListByUser(userId.Value);
            list.ForEach(async s =>
            {
                await DeleteFavoriteStay(userId.Value, s.Id);
            });
        }

        public async Task<UserFavoriteStay> AddFavoriteStay(int? stayId, int? userId)
        {
            if (!userId.HasValue || !stayId.HasValue)
                throw new ArgumentNullException();

            var checkFavorite = await context.UserFavoriteStay.FirstOrDefaultAsync(ufs => ufs.UserId == userId && ufs.StayId == stayId);

            if (checkFavorite != null)
                return new UserFavoriteStay();

            var newFavoriteStay = new UserFavoriteStay
            {
                StayId = stayId.Value,
                UserId = userId.Value
            };

            context.UserFavoriteStay.Add(newFavoriteStay);
            await context.SaveChangesAsync();

            return newFavoriteStay;
        }

        public async Task<List<Stay>> FilterStaysList(string wordText, DateTime? startDate, string stayCity, List<Thematic> thematics,
            List<string> yearList, List<int> durationList, List<Activity> activities)
        {
            var stayList = await context.Stay
                .Include(s => s.Structure)
                .Include(s => s.PartnersList)
                .Include(s => s.EquipmentsList)
                .Include(s => s.AccessesList)
                .Include(s => s.PicturesList)
                .Include(s => s.FurtherInformationsList)
                .Include(s => s.ThematicsList).ThenInclude(st => st.Thematic)
                .Include(s => s.ActivitiesList).ThenInclude(sa => sa.Activity)
                .AsNoTracking()
                .ToListAsync();

            if (!string.IsNullOrEmpty(wordText) || !string.IsNullOrWhiteSpace(wordText))
                stayList = FilteredByWordKey(wordText, stayList);

            if (startDate.HasValue)
                stayList = FilteredByStartDate(startDate.Value.AddHours(2), stayList);

            if (!string.IsNullOrEmpty(stayCity) && !string.IsNullOrWhiteSpace(stayCity))
                stayList = FilteredByStayCity(stayCity, stayList);

            if (thematics != null && thematics.Count > 0)
                stayList = FilteredByThematics(thematics, stayList);

            if (yearList != null && yearList.Count > 0)
                stayList = FilteredByYear(yearList, stayList);

            if (durationList != null && durationList.Count > 0)
                stayList = FilteredByDuration(durationList, stayList);

            if (activities != null && activities.Count > 0)
                stayList = FilteredByActivities(activities, stayList);


            return stayList;
        }

        public async Task AddView(int stayId)
        {
            var stay = await GetStay(stayId);
            stay.ViewCount += 1;

            await context.SaveChangesAsync();
        }

        public async Task<IList<Stay>> GetPopularStays()
        {
            var staysList = await context.Stay
                .Include(s => s.FurtherInformationsList)
                .Include(s => s.PicturesList)
                .OrderByDescending(s => s.ViewCount)
                .AsNoTracking()
                .ToListAsync();

            //foreach (var stay in staysList)
            //{
            //    stay.FurtherInformationsList = await stayFurtherInformationService.GetStayFurtherInformationsList(stay.Id);
            //    stay.PicturesList = await stayUtilsService.GetStayPicturesList(stay.Id);
            //}

            return staysList;
        }

        //public async Task<IList<Stay>> GetLastMinutesStays()
        //{
        //    var today = DateTime.Now;

        //    return await context.Stay
        //        .Where(s => s.StartDate <= today.AddMonths(1))
        //        .OrderBy(s => s.StartDate)
        //        .AsNoTracking()
        //        .ToListAsync();
        //}

        private void CheckStay(Stay stay)
        {
            if (string.IsNullOrEmpty(stay.Title))
                throw new StayDataException();

            if(string.IsNullOrEmpty(stay.Abstract) && string.IsNullOrEmpty(stay.Description))
                throw new StayDataException();

            if (stay.MinYear == null || stay.MaxYear == null)
                throw new StayDataException();
        }

        private List<Stay> FilteredByWordKey(string wordText, List<Stay> stayList)
        {
            var keys = wordText.Split(" ");

            return stayList.Where(
                        s =>
                            keys.Any(
                                k =>
                                s.City.ToLower().Contains(k.ToLower())
                                || s.Country.ToLower().Contains(k.ToLower())
                                || s.Title.ToLower().Contains(k.ToLower())
                                || s.Abstract.ToLower().Contains(k.ToLower())
                                || s.Description.ToLower().Contains(k.ToLower())
                                || s.PostCode.ToLower().Contains(k.ToLower())
                                || s.State.ToLower().Contains(k.ToLower())
                                || s.ActivitiesList.Any(a => a.Activity.Label.ToLower().Contains(k.ToLower()))
                            )
            ).ToList();
        }

        private List<Stay> FilteredByStartDate(DateTime startDate, List<Stay> stayList)
        {
            return stayList.Where(s =>
                s.FurtherInformationsList.Any(
                    fi =>
                        fi.StartDate == startDate
                        || fi.StartDate.Value.AddDays(-1) == startDate
                        || fi.StartDate.Value.AddDays(1) == startDate
                    )
            ).ToList();
        }

        private List<Stay> FilteredByStayCity(string stayCity, List<Stay> stayList)
        {
            return stayList.Where(
                s =>
                        s.City == stayCity
                    )
                .ToList();
        }

        private List<Stay> FilteredByThematics(List<Thematic> thematics, List<Stay> staysList)
        {
            return staysList.Where(
                s =>
                    s.ThematicsList.Any(
                        st =>
                            thematics.Any(t => t.Id == st.ThematicId)
                        )
                    )
            .ToList();
        }

        private List<Stay> FilteredByYear(List<string> yearList, List<Stay> stayList)
        {
            return stayList.Where(
                s =>
                    yearList.Any(
                        y =>
                            (int.Parse(y.Split("-")[0]) <= s.MinYear && s.MinYear <= int.Parse(y.Split("-")[1]))
                            ||
                            (int.Parse(y.Split("-")[0]) <= s.MaxYear && s.MaxYear <= int.Parse(y.Split("-")[1]))
                    )
                )
            .ToList();
        }

        private List<Stay> FilteredByDuration(List<int> durationList, List<Stay> stayList)
        {
            return stayList.Where(
                s =>
                    s.FurtherInformationsList.Any(
                        fi =>
                            durationList.Any(
                                d =>
                                    GapDay(fi.StartDate.Value, fi.EndDate.Value) - 2 <= d
                                    &&
                                    GapDay(fi.StartDate.Value, fi.EndDate.Value) + 2 >= d
                            )
                    )
                )
            .ToList();
        }

        private List<Stay> FilteredByActivities(List<Activity> activities, List<Stay> staysList)
        {
            return staysList.Where(
                s =>
                    s.ActivitiesList.Any(
                        st =>
                            activities.Any(t => t.Id == st.ActivityId)
                        )
                    )
            .ToList();
        }

        private int GapDay(DateTime startDate, DateTime endDate)
        {
            TimeSpan Ts = endDate - startDate;
            return Ts.Days;
        }
    }
}
