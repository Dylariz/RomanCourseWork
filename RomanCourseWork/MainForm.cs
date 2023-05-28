using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RomanCourseWork
{
    public partial class MainForm : Form
    {
        private CircularLinkedList<Student> _group = new CircularLinkedList<Student>();
        public MainForm()
        {
            InitializeComponent();
        }

        private CircularLinkedList<Student> ReadFileToList(string filePath) // Считывание с файла в Лист
        {
            using var streamReader = new StreamReader(filePath);
            var group = new CircularLinkedList<Student>();

            while (!streamReader.EndOfStream)
            {
                Student student;
                string[] studentStrings = streamReader.ReadLine().Split(';');
                
                if (studentStrings.Length == 2 || studentStrings[2] == string.Empty)
                {
                    student = new Student(studentStrings[1].Split(',').Select(int.Parse).ToArray(), studentStrings[0]);
                }
                else
                {
                    student = new StudentWithBonus(studentStrings[1].Split(',').Select(int.Parse).ToArray(),
                        studentStrings[0], int.Parse(studentStrings[2]));
                }
                group.AddToTail(student);
            }
            
            return group;
        }

        private void CopyListToListBox() // Копирование строковых данных из _group в outListBox
        {
            outListBox.Items.Clear();
            for (int i = 0; i < _group.Count; i++)
            {
                outListBox.Items.Add(_group[i].Value.GetStat());
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = @"Text files(*.txt)|*.txt|All files(*.*)|*.*";
            
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filePath = dialog.FileName;
            
            _group = ReadFileToList(filePath);
            
            for (int i = 0; i < _group.Count; i++)
            {
                var student = _group[i];
                foreach (var grade in student.Value.Grades)
                {
                    if (grade <= 2)
                    {
                        _group.Remove(student.Value);
                        i--;
                        break; // выход из внутреннего цикла, так как элемент уже удален
                    }
                }
            }
            
            CopyListToListBox();
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            _group.SortFromHeadToTailBy(s => s.AverageGrade, true);
            CopyListToListBox();
        }
    }
}