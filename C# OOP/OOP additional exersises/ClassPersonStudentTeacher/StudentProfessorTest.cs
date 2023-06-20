using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassPersonStudentTeacher
{
    public class StudentProfessorTest
    {
        static void Main(string[] arg)
        {
            Person person = new Person();
            person.Greet();

            Student student = new Student();
            student.Greet();
            student.SetAge(21);
            student.ShowAge();
            student.Study();

            Teacher teacher = new Teacher();
            teacher.Greet();
            teacher.SetAge(50);
            teacher.Explain();
        }
    }
}
