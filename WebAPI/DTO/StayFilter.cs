using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO
{
    public class StayFilter
    {
        public string wordText { get; set; }
        public DateTime? StartDate { get; set; }
        public string StayCity { get; set; }
        public List<Thematic> Thematics { get; set; }
        public List<string> YearList { get; set; }
        public List<int> DurationList { get; set; }
        public List<Activity> Activities { get; set; }
    }
}
