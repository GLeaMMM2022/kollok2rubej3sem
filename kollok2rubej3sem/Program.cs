using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kollok2rubej3sem
{
    public enum Stages
    {
        Elementary,
        Secondary,
        Higher
    }

    //ФИО (FIO), класс (grade), средняя успеваемость (performance) и
    //уровень образования (stage: младшая 1-4 класс - elementary , средняя 4-8 класс - secondary,  старшая 9-11 класс - higher) .
    public class Student
    {
        public string FIO { get; set; }
        public int Grade { get; set; }
        public double Performance { get; set; }
        public Stages Stage { get; set; }

        private static char currentName = 'A';

        public Student()
        {
            FIO = currentName.ToString();
            Grade = new Random().Next(1, 12); //класс
            Performance = Math.Round(new Random().NextDouble() * 5, 1); //успеваемость
            SetStage();
            currentName++;
        }

        public Student(string name, int grade, double performance)
        {
            FIO = name;
            Grade = grade;
            Performance = performance;
            SetStage();
        }

        public void Pass()
        {
            Grade++;
            SetStage();
        }

        public void SetStage()
        {
            if (Grade >= 1 && Grade <= 4)
                Stage = Stages.Elementary;
            else if (Grade >= 5 && Grade <= 8)
                Stage = Stages.Secondary;
            else
                Stage = Stages.Higher;
        }

        public override string ToString()
        {
            return $"{FIO}, {GetStageName()}, {Grade} класс, {Performance} балла";
        }

        private string GetStageName()
        {
            switch (Stage)
            {
                case Stages.Elementary:
                    return "младшая школа";
                case Stages.Secondary:
                    return "средняя школа";
                case Stages.Higher:
                    return "старшая школа";
                default:
                    return "";
            }
        }
    }

    public class School
    {
        public string Name { get; set; }
        public List<Student> ListStudents { get; set; }

        public School(string name)
        {
            Name = name;
            ListStudents = new List<Student>();
        }

        public void Add(Student student)
        {
            ListStudents.Add(student);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < ListStudents.Count; i++)
            {
                result.AppendLine($"{i + 1}. {ListStudents[i]}");
            }

            return result.ToString();
        }

        //ПРИ ИСПОЛЬЗОВАНИИ ДЕЛЕГАТА ВОЗНИКАЕТ ОШИБКА В ТРЕТЬЕМ 
        public int Count(Func<Student, bool> filter)
        {
            return ListStudents.Count(filter);
        }

        /*ВЫВОД БЕЗ ИСПОЛЬЗОВАНИЯ ДЕЛЕГАТА
        public int CountByGrade(int minGrade)
        {
            return ListStudents.Count(student => student.Grade > minGrade);
        }

        public int CountByStage(Stages stage)
        {
            return ListStudents.Count(student => student.Stage == stage);
        }

        public int CountByPerformanceAndGrade(double minPerformance, int targetGrade)
        {
            return ListStudents.Count(student => student.Performance >= minPerformance && student.Grade == targetGrade);
        }

        public int CountByName(string name)
        {
            return ListStudents.Count(student => student.FIO.Contains(name));
        }*/


        
    }

    class Program
    {
        static void Main(string[] args)
        {
            Student studA = new Student();
            Student studB = new Student();
            Student studAbaev = new Student("Абаев Георгий", 7, 3.4);
            Student studBagaev = new Student("Багаев Аслан", 4, 4);

            Console.WriteLine(studA);
            Console.WriteLine(studB);
            Console.WriteLine(studAbaev);
            Console.WriteLine(studBagaev);

            studBagaev.Pass();
            Console.WriteLine(studBagaev);

            School school = new School("ФизМат");
            school.Add(studA);
            school.Add(studB);
            school.Add(studAbaev);
            school.Add(studBagaev);

            Console.WriteLine(school);
            
            /*ВЫВОД БЕЗ ИСПОЛЬЗОВАНИЯ ДЕЛЕГАТА
            Console.WriteLine(school.CountByGrade(1));           

            Console.WriteLine(school.CountByStage(Stages.Elementary));           

            Console.WriteLine(school.CountByPerformanceAndGrade(3.0, 1));
            
            Console.WriteLine(school.CountByName("Багаев"));
            */

             //ВЫВОД С ИСПОЛЬЗОВАНИЕМ ДЕЛЕГАТА
             
              Console.WriteLine(school.Count(x => x.Grade > 1));
              Console.WriteLine(school.Count(x => x.Stage == Stages.Elementary));
              //Console.WriteLine(school.Count((x, y) => (x.Performance >= 3.0 && x.Performance <= 5 && y == 1))); С ТЕКУЩЕЙ СТРОКОЙ НЕ РАБОТАЕТ ТК ВЫДАЕТСЯ ОШИБКА 
              Console.WriteLine(school.Count(student => student.Performance >= 3.0 && student.Performance <= 5.0));
              Console.WriteLine(school.Count(x => x.FIO.Contains("Багаев")));
             
        }
    }
}


