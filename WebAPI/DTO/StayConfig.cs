using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO
{
    public class StayConfig
    {
        public Stay Stay { get; set; }
        public List<Activity> Activities { get; set; }
        public List<Thematic> Thematics { get; set; }
    }
}
