namespace RomanCourseWork
{
    public class StudentWithBonus : Student
    {
        public int Bonus;

        public StudentWithBonus(int[] grades, string name, int bonus) : base(grades, name)
        {
            Bonus = bonus;
        }

        public override string GetStat()
        {
            return $"Имя: {Name}\tСр. Оценка: {AverageGrade}\tСтепендия: {Bonus}";
        }
    }
}