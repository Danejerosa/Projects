using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YelpProject.Classes
{
    class Checkins
    {
        enum Day
        {
            Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
        }

        public String checkin_day { get; set; }
        public DateTime checkin_time { get; set; }
        public int checkin_count { get; set; }

        public Checkins() { }

        public List<Checkins> SortDays(List<Checkins> unorderedDays)
        {
            unorderedDays.Sort((a, b) => a.CompareTo(b));
            return unorderedDays;
        }

        private int CompareTo(Checkins b)
        {
            Day thisDay;
            Day compareDay;
            Enum.TryParse(this.checkin_day, out thisDay);
            Enum.TryParse(b.checkin_day, out compareDay);
            return thisDay < compareDay ? -1 : 1;
        }
    }
}
