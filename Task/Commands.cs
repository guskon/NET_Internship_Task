using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace NET_Internship_Task
{
    internal class Commands
    {
        public static void CreateMeeting(List<Meeting> meetings, string username)
        {
            Console.WriteLine("  ------------------");
            Console.WriteLine(" | Create a meeting |");
            Console.WriteLine("  ------------------");
            Console.WriteLine();

            string name = "";
            while (true)
            {
                Console.Write(" Enter name: ");
                name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                    break;
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(" Invalid entry ");
                }
            }
            Console.WriteLine();

            string description = "";
            while (true)
            {
                Console.Write(" Enter description: ");
                description = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(description))
                    break;
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(" Invalid entry ");
                }
            }
            Console.WriteLine();

            Category category = new Category();
            while (true)
            {
                Console.WriteLine(" Select category: ");
                Console.WriteLine(" a: " + Category.CodeMonkey);
                Console.WriteLine(" b: " + Category.Hub);
                Console.WriteLine(" c: " + Category.Short);
                Console.WriteLine(" d: " + Category.TeamBuilding);
                string categorySelection = Console.ReadLine();
                
                if (categorySelection == "a") { category = Category.CodeMonkey; break; }
                else if (categorySelection == "b") { category = Category.Hub; break; }
                else if (categorySelection == "c") { category = Category.Short; break; }
                else if (categorySelection == "d") { category = Category.TeamBuilding; break;}
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(" Invalid category ");
                }
            }
            Console.WriteLine();

            Type type = new Type();
            while (true)
            {
                Console.WriteLine(" Select type: ");
                Console.WriteLine(" a: " + Type.Live);
                Console.WriteLine(" b: " + Type.InPerson);
                string typeSelection = Console.ReadLine();

                if (typeSelection == "a") { type = Type.Live; break; }
                else if (typeSelection == "b") { type = Type.InPerson; break; }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(" Invalid type ");
                }
            }
            Console.WriteLine();

            while(true)
            {
                DateTime startDate = new DateTime();
                while (true)
                {
                    Console.Write(" Enter start date in yyyy-mm-dd HH:mm format: ");
                    string dateInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParse(dateInput, out startDate))
                    {
                        startDate = DateTime.Parse(dateInput);
                        break;
                    } 
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(" Invalid start date ");
                    }
                }
                Console.WriteLine();

                DateTime endDate = new DateTime();
                while (true)
                {
                    Console.Write(" Enter end date in yyyy-mm-dd HH:mm format: ");
                    string dateInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParse(dateInput, out endDate))
                    {
                        endDate = DateTime.Parse(dateInput);
                        break;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(" Invalid end date ");
                    }
                }
                Console.WriteLine();


                if (!Methods.DoDatesIntersect(meetings, username, startDate, endDate))
                {
                    List<(string, DateTime)> attendees = new List<(string, DateTime)>();
                    attendees.Add((username, startDate));
                    meetings.Add(new Meeting(name, username, description, category, type, startDate, endDate, attendees));
                    Console.WriteLine();
                    Console.WriteLine(" Meeting created successfully");
                    Console.WriteLine();
                    break;
                }
                else
                    Console.WriteLine();
                    Console.WriteLine(" This meeting intersects with other meeting/meetings, so you need to change the date");
                    Console.WriteLine();
            }
        }

       public static void DeleteMeeting(List<Meeting> meetings, string username)
       {
            Console.WriteLine("  -----------------");
            Console.WriteLine(" | Delete a meeting |");
            Console.WriteLine("  -----------------");
            Console.WriteLine();

            List<Meeting> selectedMeetings = meetings.Where(a => a.ResponsiblePerson == username).ToList();

            if (selectedMeetings.Count == 0)
            {
                Console.WriteLine(" You are not responsible for any meetings");
                return;
            }
            else
            {
                InOut.PrintMeetings(selectedMeetings);

                while(true)
                {
                    string name = "";
                    while (true)
                    {
                        Console.Write(" Enter the name of the meeting you want to delete: ");
                        name = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(name))
                            break;
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(" Invalid entry ");
                        }
                    }
                    if (meetings.Where(a => a.Name == name && a.ResponsiblePerson == username).ToList().Count > 0)
                    {
                        meetings.RemoveAll(a => a.Name == name && a.ResponsiblePerson == username);
                        Console.WriteLine();
                        Console.WriteLine(" Meeting deleted successfully");
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(" This name does not exist ");
                        Console.WriteLine();
                    }
                        
                }
            }
       }
    
       public static void AddPerson(List<Meeting> meetings)
       {
            Console.WriteLine("  --------------");
            Console.WriteLine(" | Add a person |");
            Console.WriteLine("  --------------");
            Console.WriteLine();

            if (meetings.Count == 0)
            {
                Console.WriteLine(" Currently there are no meetings planned ");
                Console.WriteLine();
            }
            else
            {
                InOut.PrintMeetings(meetings);

                string name = "";

                while (true)
                {
                    while (true)
                    {
                        Console.Write(" Enter the name of the meeting: ");
                        name = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(name))
                            break;
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(" Invalid entry ");
                        }
                    }
                    if (meetings.Where(a => a.Name == name).ToList().Count > 0)
                        break;
                    else
                        Console.WriteLine();
                    Console.WriteLine(" This name does not exist ");
                    Console.WriteLine();
                }

                List<(string, DateTime)> attendees = meetings.Where(a => a.Name == name).Select(a => a.Attendees).FirstOrDefault();

                InOut.PrintAttendees(attendees);

                string newPerson = "";

                while (true)
                {
                    while (true)
                    {
                        Console.Write(" Enter the name of the new attendee: ");
                        newPerson = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newPerson))
                            break;
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(" Invalid entry ");
                        }
                    }

                    if (attendees.Where(a => a.Item1 == newPerson).ToList().Count == 0)
                        break;
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(" This attendee is already added to this meeting ");
                        Console.WriteLine();
                    }

                }

                Meeting meeting = meetings.Where(a => a.Name == name).FirstOrDefault();

                while (true)
                {
                    TimeSpan inputTime = new TimeSpan();
                    DateTime dateTime = new DateTime();
                    while (true)
                    {
                        Console.Write(" Enter at what time this attendee will be added in HH:mm format (or enter c to cancel): ");
                        string timeInput = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(timeInput) && DateTime.TryParse(timeInput, out dateTime))
                        {
                            inputTime = TimeSpan.Parse(timeInput);
                            break;
                        }
                        else if (timeInput == "c") return;
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(" Invalid time ");
                        }
                    }
                    Console.WriteLine();

                    DateTime time = Methods.FindStartTime(meeting, inputTime);

                    if (!Methods.DoDatesIntersect(meetings, newPerson, time, meeting.EndDate))
                    {
                        meetings.Where(a => a.Name == name).FirstOrDefault().Attendees.Add((newPerson, time));
                        Console.WriteLine();
                        Console.WriteLine(" Attendee is added successfully ");
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(" Attendee cannot be added at this time as it intersects with other meeting/meetings ");
                        Console.WriteLine();
                    }
                }
            }
       }

       public static void RemovePerson(List<Meeting> meetings)
       {
            Console.WriteLine();
            Console.WriteLine("  -----------------");
            Console.WriteLine(" | Remove a person |");
            Console.WriteLine("  -----------------");
            Console.WriteLine();

            if (meetings.Count == 0)
            {
                Console.WriteLine(" Currently there are no meetings planned ");
                Console.WriteLine();
            }
            else
            {
                InOut.PrintMeetings(meetings);

                string name = "";

                while (true)
                {
                    while (true)
                    {
                        Console.Write(" Enter the name of the meeting: ");
                        name = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(name))
                            break;
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(" Invalid entry ");
                        }
                    }

                    if (meetings.Where(a => a.Name == name).ToList().Count > 0)
                        break;
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(" This name does not exist ");
                        Console.WriteLine();
                    }

                }

                Meeting meeting = meetings.Where(a => a.Name == name).FirstOrDefault();
                List<(string, DateTime)> attendees = meeting.Attendees;

                string deletedPerson = "";

                if (attendees.Count == 1)
                {
                    Console.WriteLine();
                    Console.WriteLine(" Only the responsible person is left in the meeting ");
                    return;
                }
                else
                {
                    InOut.PrintAttendees(attendees);

                    while (true)
                    {
                        deletedPerson = "";
                        while (true)
                        {
                            Console.Write(" Enter the name of the attendee you want to remove: ");
                            deletedPerson = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(deletedPerson))
                                break;
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine(" Invalid entry ");
                            }
                        }
                        if (deletedPerson == meeting.ResponsiblePerson && attendees.Count > 1)
                        {
                            Console.WriteLine();
                            Console.WriteLine(" Responsible person cannot be removed ");
                            Console.WriteLine();
                        }

                        else if (attendees.Where(a => a.Item1 == deletedPerson).ToList().Count > 0)
                        {
                            meetings.Where(a => a.Name == name).FirstOrDefault().Attendees.RemoveAll(a => a.Person == deletedPerson);
                            Console.WriteLine();
                            Console.WriteLine(" Attendee removed successfully ");
                            Console.WriteLine();
                            break;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(" This person does not exist in this meeting ");
                            Console.WriteLine();
                        }

                    }
                }
            }
       }

       public static void ListAllMeetings(List<Meeting> meetings)
       {
            Console.WriteLine();
            Console.WriteLine("  ------------------");
            Console.WriteLine(" | List all meetings |");
            Console.WriteLine("  ------------------");
            Console.WriteLine();

            if (meetings.Count == 0)
            {
                Console.WriteLine(" Currently there are no meetings planned ");
                Console.WriteLine();
            }
            else
            {
                InOut.PrintMeetings(meetings);

                while (true)
                {
                    Console.WriteLine(" Choose a filtering option or enter x to exit: ");
                    Console.WriteLine(" a: By description");
                    Console.WriteLine(" b: By responsible person");
                    Console.WriteLine(" c: By category");
                    Console.WriteLine(" d: By type");
                    Console.WriteLine(" e: By date interval (from - to)");
                    Console.WriteLine(" f: By starting date (from)");
                    Console.WriteLine(" g: By minimum number of attendees");

                    string selection = Console.ReadLine();

                    if (selection == "a")
                    {
                        string description = "";
                        while (true)
                        {
                            Console.Write(" Enter description: ");
                            description = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(description))
                                break;
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine(" Invalid entry ");
                                Console.WriteLine();
                            }
                        }
                        List<Meeting> selectedMeetings = meetings.Where(a => a.Description == description).ToList();
                        if (selectedMeetings.Count > 0)
                            InOut.PrintMeetings(selectedMeetings);
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(" There are no such meetings ");
                            Console.WriteLine();
                        }
                    }
                    else if (selection == "b")
                    {
                        string responsiblePerson = "";
                        while (true)
                        {
                            Console.Write(" Enter responsible person: ");
                            responsiblePerson = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(responsiblePerson))
                                break;
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine(" Invalid entry ");
                                Console.WriteLine();
                            }
                        }
                        List<Meeting> selectedMeetings = meetings.Where(a => a.ResponsiblePerson == responsiblePerson).ToList();
                        if (selectedMeetings.Count > 0)
                            InOut.PrintMeetings(selectedMeetings);
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(" There are no such meetings ");
                            Console.WriteLine();
                        }
                    }
                    else if (selection == "c")
                    {
                        Category category = new Category();
                        while (true)
                        {
                            Console.WriteLine(" Select category: ");
                            Console.WriteLine(" a: " + Category.CodeMonkey);
                            Console.WriteLine(" b: " + Category.Hub);
                            Console.WriteLine(" c: " + Category.Short);
                            Console.WriteLine(" d: " + Category.TeamBuilding);
                            string categorySelection = Console.ReadLine();

                            if (categorySelection == "a") { category = Category.CodeMonkey; break; }
                            else if (categorySelection == "b") { category = Category.Hub; break; }
                            else if (categorySelection == "c") { category = Category.Short; break; }
                            else if (categorySelection == "d") { category = Category.TeamBuilding; break; }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine(" Invalid category ");
                                Console.WriteLine();
                            }
                        }
                        List<Meeting> selectedMeetings = meetings.Where(a => a.Category == category).ToList();
                        if (selectedMeetings.Count > 0)
                            InOut.PrintMeetings(selectedMeetings);
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(" There are no such meetings ");
                            Console.WriteLine();
                        }
                    }
                    else if (selection == "d")
                    {
                        Type type = new Type();
                        while (true)
                        {
                            Console.WriteLine(" Select type: ");
                            Console.WriteLine(" a: " + Type.Live);
                            Console.WriteLine(" b: " + Type.InPerson);
                            string typeSelection = Console.ReadLine();

                            if (typeSelection == "a") { type = Type.Live; break; }
                            else if (typeSelection == "b") { type = Type.InPerson; break; }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine(" Invalid type ");
                                Console.WriteLine();
                            }
                        }
                        List<Meeting> selectedMeetings = meetings.Where(a => a.Type == type).ToList();
                        if (selectedMeetings.Count > 0)
                            InOut.PrintMeetings(selectedMeetings);
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(" There are no such meetings ");
                            Console.WriteLine();
                        }
                    }
                    else if (selection == "e")
                    {
                        DateTime startDate = new DateTime();
                        while (true)
                        {
                            Console.Write(" Enter start date in yyyy-mm-dd HH:mm format: ");
                            string dateInput = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParse(dateInput, out startDate))
                            {
                                startDate = DateTime.Parse(dateInput);
                                break;
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine(" Invalid start date ");
                                Console.WriteLine();
                            }
                        }
                        Console.WriteLine();

                        DateTime endDate = new DateTime();
                        while (true)
                        {
                            Console.Write(" Enter end date in yyyy-mm-dd HH:mm format: ");
                            string dateInput = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParse(dateInput, out endDate))
                            {
                                endDate = DateTime.Parse(dateInput);
                                break;
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine(" Invalid end date ");
                                Console.WriteLine();
                            }
                        }
                        Console.WriteLine();

                        List<Meeting> selectedMeetings = meetings.Where(a => a.StartDate >= startDate && a.EndDate <= endDate).ToList();
                        if (selectedMeetings.Count > 0)
                            InOut.PrintMeetings(selectedMeetings);
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(" There are no such meetings ");
                            Console.WriteLine();
                        }
                    }
                    else if (selection == "f")
                    {
                        DateTime startDate = new DateTime();
                        while (true)
                        {
                            Console.Write(" Enter start date in yyyy-mm-dd HH:mm format: ");
                            string dateInput = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParse(dateInput, out startDate))
                            {
                                startDate = DateTime.Parse(dateInput);
                                break;
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine(" Invalid start date ");
                                Console.WriteLine();
                            }
                        }
                        Console.WriteLine();
                        List<Meeting> selectedMeetings = meetings.Where(a => a.StartDate >= startDate).ToList();
                        if (selectedMeetings.Count > 0)
                            InOut.PrintMeetings(selectedMeetings);
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(" There are no such meetings ");
                            Console.WriteLine();
                        }
                    }
                    else if (selection == "g")
                    {
                        int numberOfAttendees = 0;
                        while (true)
                        {
                            Console.Write(" Enter minimum amount of attendees: ");
                            string input = Console.ReadLine();
                            if (Int32.TryParse(input, out numberOfAttendees))
                            {
                                numberOfAttendees = Convert.ToInt32(input);
                                break;
                            }

                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine(" Invalid entry ");
                                Console.WriteLine();
                            }
                        }
                        Console.WriteLine();
                        List<Meeting> selectedMeetings = meetings.Where(a => a.Attendees.Count >= numberOfAttendees).ToList();
                        if (selectedMeetings.Count > 0)
                            InOut.PrintMeetings(selectedMeetings);
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(" There are no such meetings ");
                            Console.WriteLine();
                        }

                    }
                    else if (selection == "x") break;
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(" Invalid selection ");
                        Console.WriteLine();
                    }
                }
            }
       }
    }
}
