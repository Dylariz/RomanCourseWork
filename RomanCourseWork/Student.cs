namespace RomanCourseWork
{
    public class Student
    {
        public double AverageGrade;
        public string Name;
        public int MinGrade = 1000;
        public int[] Grades;

        public Student(int[] grades, string name)
        {
            Grades = grades;
            Name = name;
            foreach (var t in Grades)
            {
                AverageGrade += t;
                if (t < MinGrade)
                {
                    MinGrade = t;
                }
            }

            AverageGrade /= grades.Length;
        }

        public virtual string GetStat()
        {
            return $"Имя: {Name}\tСр. Оценка: {AverageGrade}";
        }
    }
}