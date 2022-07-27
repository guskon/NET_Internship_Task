using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_Internship_Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("  -------------------------------------");
            Console.WriteLine(" | Visma internal meetings application |");
            Console.WriteLine("  -------------------------------------");
            Console.WriteLine();
            Console.WriteLine();

            // Username input ---------------------------------
            string username = "";
            while (true)
            {
                Console.Write(" Enter your name: ");
                username = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(username))
                    break;
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(" Invalid name ");
                }
            }
            Console.WriteLine();
            // ------------------------------------------------

            List<Meeting> meetings = InOut.ReadJSONToList();


            while (true)
            {
                Console.WriteLine();
                Console.WriteLine(" Select command or enter x to exit: ");
                Console.WriteLine(" a: Create a new meeting ");
                Console.WriteLine(" b: Delete a meeting ");
                Console.WriteLine(" c: Add a person to the meeting ");
                Console.WriteLine(" d: Remove a person from the meeting ");
                Console.WriteLine(" e: List all the meetings ");
                string command = Console.ReadLine();

                if (command == "a") { Commands.CreateMeeting(meetings, username); InOut.PrintJSONToFile(meetings); }
                else if (command == "b") { Commands.DeleteMeeting(meetings, username); InOut.PrintJSONToFile(meetings); }
                else if (command == "c") { Commands.AddPerson(meetings); InOut.PrintJSONToFile(meetings); }
                else if (command == "d") { Commands.RemovePerson(meetings); InOut.PrintJSONToFile(meetings); }
                else if (command == "e") Commands.ListAllMeetings(meetings);
                else if (command == "x") break;
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(" Invalid command ");
                }
                    
            }

            
        }
    }
}
