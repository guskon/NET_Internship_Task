using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET_Internship_Task;
using System;
using System.Collections.Generic;
using Type = NET_Internship_Task.Type;

namespace Test_Project
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Check_Dates_Overlaps()
        {
            //Arrange
            List<Meeting> meetings = new List<Meeting>();

            //First meeting object
            List<(string, DateTime)> attendees1 = new List<(string, DateTime)>();
            attendees1.Add(("John", new DateTime(2022, 05, 26, 10, 30, 00)));
            attendees1.Add(("Peter", new DateTime(2022, 05, 26, 10, 50, 00)));
            Meeting meeting1 = new Meeting("Meeting1", "John", "Awesome meeting", Category.CodeMonkey, Type.InPerson, new DateTime(2022, 05, 26, 10, 30, 00),
                new DateTime(2022, 05, 26, 11, 30, 00), attendees1);

            //Second meeting object
            List<(string, DateTime)> attendees2 = new List<(string, DateTime)>();
            attendees2.Add(("Dave", new DateTime(2022, 05, 26, 10, 20, 00)));

            Meeting meeting2 = new Meeting("Meeting2", "Dave", "Great meeting", Category.Hub, Type.Live, new DateTime(2022, 05, 26, 10, 20, 00),
                new DateTime(2022, 05, 26, 11, 20, 00), attendees2);

            meetings.Add(meeting1);
            meetings.Add(meeting2);

            bool expected = true;

            //Act 
            bool actual = Methods.DoDatesIntersect(meetings, "Peter", new DateTime(2022, 05, 26, 11, 00, 00), new DateTime(2022, 05, 26, 11, 20, 00));

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Check_Dates_DoesNotOverlap()
        {
            //Arrange
            List<Meeting> meetings = new List<Meeting>();

            //First meeting object
            List<(string, DateTime)> attendees1 = new List<(string, DateTime)>();
            attendees1.Add(("John", new DateTime(2022, 05, 26, 10, 30, 00)));
            attendees1.Add(("Peter", new DateTime(2022, 05, 26, 10, 50, 00)));
            Meeting meeting1 = new Meeting("Meeting1", "John", "Awesome meeting", Category.CodeMonkey, Type.InPerson, new DateTime(2022, 05, 26, 10, 30, 00),
                new DateTime(2022, 05, 26, 11, 30, 00), attendees1);

            //Second meeting object
            List<(string, DateTime)> attendees2 = new List<(string, DateTime)>();
            attendees2.Add(("Dave", new DateTime(2022, 05, 26, 10, 20, 00)));

            Meeting meeting2 = new Meeting("Meeting2", "Dave", "Great meeting", Category.Hub, Type.Live, new DateTime(2022, 05, 26, 10, 20, 00),
                new DateTime(2022, 05, 26, 15, 30, 00), attendees2);

            meetings.Add(meeting1);
            meetings.Add(meeting2);

            bool expected = false;

            //Act 
            bool actual = Methods.DoDatesIntersect(meetings, "Peter", new DateTime(2022, 05, 26, 14, 00, 00), new DateTime(2022, 05, 26, 15, 30, 00));

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_Date_FindsStartDate()
        {
            //Arrange
            //Creating Meeting object
            List<(string, DateTime)> attendees = new List<(string, DateTime)>();
            attendees.Add(("John", new DateTime(2022, 05, 26, 10, 30, 00)));
            Meeting meeting = new Meeting("Meeting1", "John", "Awesome meeting", Category.CodeMonkey, Type.InPerson, new DateTime(2022, 05, 26, 10, 30, 00),
                new DateTime(2022, 05, 26, 11, 30, 00), attendees);


            TimeSpan inputTime = new TimeSpan(10, 40, 00);

            DateTime expected = new DateTime(2022, 05, 26, 10, 40, 00);

            //Act
            DateTime actual = Methods.FindStartTime(meeting, inputTime);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_Date_DoesNotFindStartDate()
        {
            //Arrange
            //Creating Meeting object
            List<(string, DateTime)> attendees = new List<(string, DateTime)>();
            attendees.Add(("John", new DateTime(2022, 05, 26, 10, 30, 00)));
            Meeting meeting = new Meeting("Meeting1", "John", "Awesome meeting", Category.CodeMonkey, Type.InPerson, new DateTime(2022, 05, 26, 10, 30, 00),
                new DateTime(2022, 05, 26, 11, 30, 00), attendees);


            TimeSpan inputTime = new TimeSpan(1, 10, 40, 00);

            DateTime expected = new DateTime(2022, 05, 26, 10, 40, 00);

            //Act
            DateTime actual = Methods.FindStartTime(meeting, inputTime);

            //Assert
            Assert.AreNotEqual(expected, actual);
        }
    }
}