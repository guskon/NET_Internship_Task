using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_Internship_Task
{
    public enum Category
    {
        CodeMonkey,
        Hub,
        Short,
        TeamBuilding
    }

    public enum Type
    {
        Live,
        InPerson
    }

    public class Meeting
    {
        public string Name { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public Type Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<(string Person, DateTime Date)> Attendees { get; set; }

        public Meeting(string name, string responsiblePerson, string description, Category category, Type type, DateTime startDate, DateTime endDate, List<(string Person, DateTime Date)> attendees)
        {
            Name = name;
            ResponsiblePerson = responsiblePerson;
            Description = description;
            Category = category;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            Attendees = attendees;
        }
    }
}
