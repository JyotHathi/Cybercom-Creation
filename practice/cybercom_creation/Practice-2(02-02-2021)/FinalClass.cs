using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrentNameSpace=Practice_2_02_02_2021_;
namespace Practice_2_02_02_2021_
{
    public enum Grade {AA=10,AB=9,BB=8,BC=7,CC=6,CD=5,DD=4,FF=0 };
    class FinalClass<Y>
    {
        string student_name;
        Grade studentGrade;
        Y points;
        static List<FinalClass<Y>> finalClasses = new List<FinalClass<Y>>();
        public FinalClass(string name, Y points, Grade grade)
        {
            this.StudentName = name;
            this.points = points;
            this.studentGrade = grade;
            finalClasses.Add(this);
        }
        public string StudentName
        {
            get { return student_name; }
            set { student_name = value; }
        }
        public Grade StudentGrade
        {
            get { return studentGrade; }
            set { studentGrade = value; }
        }
        public Y Points
        {
            get { return points; }
            set { points = value; }
        }

        public void DisplayDeatils()
        {
            Console.WriteLine($"Student Name : {student_name}");
            Console.WriteLine($"Points : {points}");
            Console.WriteLine($"Student Grade : {studentGrade}");
        }
        public static void ShowUsers()
        {
            foreach(FinalClass<Y> user in finalClasses)
            {
                Console.WriteLine($"{user.StudentName}\t{user.points}\t{user.studentGrade}");
            }
        }
       
}
}
