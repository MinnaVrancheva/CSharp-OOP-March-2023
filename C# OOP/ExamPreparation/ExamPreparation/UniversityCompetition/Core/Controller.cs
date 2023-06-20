using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Repositories.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private IRepository<IStudent> students;
        private IRepository<ISubject> subjects;
        private IRepository<IUniversity> universities;

        public Controller()
        {
            this.students = new StudentRepository();
            this.subjects = new SubjectRepository();
            this.universities = new UniversityRepository();
        }
        public string AddStudent(string firstName, string lastName)
        {
            string nameCombined = firstName + " " + lastName;
            
            if (students.FindByName(nameCombined) != null)
            {
                return string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
            }

            IStudent student = new Student(this.students.Models.Count + 1, firstName, lastName);
            students.AddModel(student);
            return string.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName, students.GetType().Name);
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            if (subjects.FindByName(subjectName) != null)
            {
                return string.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            }
                        
            int currentId = subjects.Models.Count + 1;
            ISubject subject;

            if (subjectType == nameof(EconomicalSubject))
            {
                subject = new EconomicalSubject(currentId, subjectName);
            }
            else if (subjectType == nameof(TechnicalSubject))
            {
                subject = new TechnicalSubject(currentId, subjectName);
            }
            else if (subjectType == nameof(HumanitySubject))
            {
                subject = new HumanitySubject(currentId, subjectName);
            }
            else
            {
                return String.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            }

            subjects.AddModel(subject);
            return string.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName, subjects.GetType().Name);
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects) 
        {
            if (universities.FindByName(universityName) != null)
            {
                return String.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }
            
            List<int> subjectsIds = new List<int>();

            foreach (var subject in requiredSubjects)
            {
                subjectsIds.Add(subjects.FindByName(subject).Id);
            }

            IUniversity university = new University(this.universities.Models.Count + 1, universityName, category, capacity, subjectsIds);

            universities.AddModel(university);
            return string.Format(OutputMessages.UniversityAddedSuccessfully, universityName, universities.GetType().Name);
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            IStudent student = students.FindByName(studentName);
            if (student == null)
            {
                return string.Format(OutputMessages.StudentNotRegitered, student.FirstName, student.LastName);
            }

            IUniversity university = universities.FindByName(universityName);
            if (university == null)
            {
                return String.Format(OutputMessages.UniversityNotRegitered, universityName);
            }

            if (!university.RequiredSubjects.All(x => student.CoveredExams.Any(e => e == x)))
            {
                return String.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
            }

            if (student.University != null && student.University.Name == universityName)
            {
                return String.Format(OutputMessages.StudentAlreadyJoined, student.FirstName, student.LastName, universityName);
            }

            student.JoinUniversity(university);
            return String.Format(OutputMessages.StudentSuccessfullyJoined, student.FirstName, student.LastName, universityName);
        }

        public string TakeExam(int studentId, int subjectId)
        {
            IStudent student = students.FindById(studentId);
            if (student == null)
            {
                return string.Format(OutputMessages.InvalidStudentId);
            }

            ISubject subject = subjects.FindById(subjectId);
            if (subject == null)
            {
                return String.Format(OutputMessages.InvalidSubjectId);
            }

            if (students.FindById(studentId).CoveredExams.Any(x => x == subjectId))
            {
                return string.Format(OutputMessages.StudentAlreadyCoveredThatExam, student.FirstName, student.LastName, subject.Name);
            }

            student.CoverExam(subject);
            return string.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName, subject.Name);
        }

        public string UniversityReport(int universityId)
        {
            IUniversity university = universities.FindById(universityId);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            sb.AppendLine($"Students admitted: {this.students.Models.Where(x => x.University == university).Count()}");
            sb.AppendLine($"University vacancy: {university.Capacity - students.Models.Where(x => x.University == university).Count()}");

            return sb.ToString().TrimEnd();
        }
    }
}
