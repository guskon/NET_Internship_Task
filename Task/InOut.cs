using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace NET_Internship_Task
{
    internal class InOut
    {
        public static List<Meeting> ReadJSONToList()
        {
            if (File.Exists("meetingsStorage.json"))
                if (new FileInfo("meetingsStorage.json").Length != 0)
                {
                    string json = File.ReadAllText("meetingsStorage.json");
                    return JsonConvert.DeserializeObject<List<Meeting>>(json);
                }
            return new List<Meeting>();
        }

        public static void PrintJSONToFile(List<Meeting> meetings)
        {
            string json = JsonConvert.SerializeObject(meetings);
            File.WriteAllText("meetingsStorage.json", json);
        }

        public static void PrintMeetings(List<Meeting> meetings)
        {
            Console.WriteLine();
            Console.WriteLine(" | Meetings |");
            Console.WriteLine();
            Console.WriteLine("  ---------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" | {0, -20} | {1, -10} | {2, -20} | {3, -12} | {4, -10} | {5, -20} | {6, -20} | {7, -10} |", "Name", "Responsible person", "Description", "Category",
                "Type", "Start date", "End date", "Attendees");
            Console.WriteLine("  ---------------------------------------------------------------------------------------------------------------------------------------------------------");

            foreach (Meeting meeting in meetings)
            {
                Console.WriteLine(" | {0, -20} | {1, -18} | {2, -20} | {3, -12} | {4, -10} | {5, -20} | {6, -20} | {7, 10} |", meeting.Name, meeting.ResponsiblePerson,
                    meeting.Description, meeting.Category, meeting.Type, meeting.StartDate.ToString("yyyy-MM-dd HH:mm"), meeting.EndDate.ToString("yyyy-MM-dd HH:mm"), meeting.Attendees.Count);
            }

            Console.WriteLine("  ---------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
        }


        public static void PrintAttendees(List<(string, DateTime)> attendees)
        {
            Console.WriteLine();
            Console.WriteLine(" | Attendees |");
            Console.WriteLine();
            Console.WriteLine("  ------------------------------");
            Console.WriteLine(" | {0, -10} | {1, -10} |", "Attendee", "Time when added");
            Console.WriteLine("  ------------------------------");
            foreach ((string person, DateTime date) in attendees)
            {
                Console.WriteLine(" | {0, -10} | {1, -15} |", person, date.ToString("HH:mm"));
            }
            Console.WriteLine(" -------------------------------");
            Console.WriteLine();
        }
    }
}
