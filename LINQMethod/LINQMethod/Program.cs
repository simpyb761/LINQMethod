using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;
using System.Runtime.InteropServices;

namespace linqmethod
{
    class Program
    {
          static void Main(string[] args)
          {
            // Student collection
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major="Hospitality", Tuition=3500.00} ,
                new Student() { StudentID = 1, StudentName = "Gina Host", Age = 21, Major="Hospitality", Tuition=4500.00 } ,
                new Student() { StudentID = 2, StudentName = "Cookie Crumb",  Age = 21, Major="CIT", Tuition=2500.00 } ,
                new Student() { StudentID = 3, StudentName = "Ima Script",  Age = 48, Major="CIT", Tuition=5500.00 } ,
                new Student() { StudentID = 3, StudentName = "Cora Coder",  Age = 35, Major="CIT", Tuition=1500.00 } ,
                new Student() { StudentID = 4, StudentName = "Ura Goodchild" , Age = 40, Major="Marketing", Tuition=500.00} ,
                new Student() { StudentID = 5, StudentName = "Take Mewith" , Age = 29, Major="Aerospace Engineering", Tuition=5500.00 }
            };
            // Student GPA Collection
            IList<StudentGPA> studentGPAList = new List<StudentGPA>() {
                new StudentGPA() { StudentID = 1,  GPA=4.0} ,
                new StudentGPA() { StudentID = 2,  GPA=3.5} ,
                new StudentGPA() { StudentID = 3,  GPA=2.0 } ,
                new StudentGPA() { StudentID = 4,  GPA=1.5 } ,
                new StudentGPA() { StudentID = 5,  GPA=4.0 } ,
                new StudentGPA() { StudentID = 6,  GPA=2.5} ,
                new StudentGPA() { StudentID = 7,  GPA=1.0 }
            };
            // Club collection
            IList<StudentClubs> studentClubList = new List<StudentClubs>() {
                new StudentClubs() {StudentID=1, ClubName="Photography" },
                new StudentClubs() {StudentID=1, ClubName="Game" },
                new StudentClubs() {StudentID=2, ClubName="Game" },
                new StudentClubs() {StudentID=5, ClubName="Photography" },
                new StudentClubs() {StudentID=6, ClubName="Game" },
                new StudentClubs() {StudentID=7, ClubName="Photography" },
                new StudentClubs() {StudentID=3, ClubName="PTK" },
            };
            var gpaGroups = studentGPAList.GroupBy(g => g.GPA);
            foreach(var grouping in gpaGroups)
            {
                WriteLine($"GPA: {grouping.Key}");
                foreach(StudentGPA id in grouping)
                {
                    WriteLine(id.StudentID);
                }
            }
            WriteLine();
            var clubs = studentClubList.OrderBy(g=>g.ClubName).GroupBy(g=>g.ClubName);
            foreach(var grouping in clubs)
            {
                WriteLine($"Club: {grouping.Key}");
                foreach(StudentClubs id in grouping)
                {
                    WriteLine(id.StudentID);
                }
            }
            WriteLine();
            var gpaCount = studentGPAList.Where(g=>g.GPA>2.5 && g.GPA<4.0).Count();
            WriteLine(gpaCount);
            WriteLine();
            var tuitionAverage = studentList.Average(g => g.Tuition);
            WriteLine(tuitionAverage.ToString("C2"));
            WriteLine();
            var highestTuition = studentList.Max(g => g.Tuition);
            foreach(var student in studentList)
            {
                if(student.Tuition == highestTuition)
                {
                    WriteLine($"{student.StudentName}, {student.Major}, {student.Tuition}");
                }
            }
            WriteLine();
            var GPAJoin = studentList.Join
                (studentGPAList, student=>student.StudentID, 
                gpa=> gpa.StudentID,(student,gpa) => new 
                { 
                    studentName = student.StudentName,
                    studentMajor = student.Major,
                    studentGPA = gpa.GPA
                });
            foreach(var name in GPAJoin)
            {
                WriteLine($"Student Name: {name.studentName}\nStudent Major: {name.studentMajor}\nStudent GPA: {name.studentGPA}");
            }
            WriteLine();
            var gameClub = studentList.Join(studentClubList, student => student.StudentID, club => club.StudentID, (student, club) => new
            {
                studentName = student.StudentName,
                studentClub = club.ClubName
            });
            foreach(var name in gameClub)
            {
                if(name.studentClub == "Game")
                {
                    WriteLine(name.studentName);
                }
            }
          }
    }
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
        public double Tuition { get; set; }
    }
    public class StudentClubs
    {
        public int StudentID { get; set; }
        public string ClubName { get; set; }
    }
    public class StudentGPA
    {
        public int StudentID { get; set; }
        public double GPA { get; set; }
    }

    
}