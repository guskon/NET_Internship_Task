using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_Internship_Task
{
    public class Methods
    {
        public static bool DoDatesIntersect(List<Meeting> meetings, string name, DateTime startA, DateTime endA)
        {
            foreach (Meeting meeting in meetings)
            {
                foreach ((string person, DateTime date) in meeting.Attendees)
                {
                    if (person == name)
                    {
                        DateTime startB = date;
                        DateTime endB = meeting.EndDate;

                        if (startA <= endB && startB <= endA)
                            return true;
                    }
                }
            }
            return false;
        }

        public static DateTime FindStartTime(Meeting meeting, TimeSpan inputTime)
        {
            int hours = meeting.StartDate.Hour;
            int minutes = meeting.StartDate.Minute;

            double timePeriod = inputTime.TotalMinutes - (hours * 60 + minutes);

            return meeting.StartDate.AddMinutes(timePeriod);
        }
    }
}


