using LigaACLabs.Models.Entities;

namespace LigaACLabs.Services
{
    public class SubjectRepository : ISubjectRepository
    {
        private static Guid StudentId1 = Guid.Parse("7074815E-7E36-442E-AAFA-9241D3FD9F24");
        private static Guid StudentId2 = Guid.Parse("9DB26953-E797-4772-AB44-667B8B3C3C62");
        private static Guid StudentId3 = Guid.Parse("A65E49A9-1E80-468F-BB9E-44997618E8F2");        

        public IEnumerable<Subject> GetSubjectsForUser(Guid userId)
        {
            var student = Students.Where(s => s.Id == userId).FirstOrDefault();

            if (student == null)
            {
                return Enumerable.Empty<Subject>();
            }

            return student.Subjects;
        }

        public IEnumerable<Lab> GetLabOptions(Guid subjectId)
        {
            var subject = Subjects.Where(s => s.Id == subjectId).FirstOrDefault();
            
            if (subject == null)
            {
                return Enumerable.Empty<Lab>();
            }

            return subject.Labs;
        }

        #region data
        private static List<Subject> Subjects = new List<Subject>()
        {
            new Subject()
            {
                Id = Guid.NewGuid(),
                Name = "Physics",
                Labs = new List<Lab>()
                {
                    new Lab()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Option 1",
                        LabDate = DateTime.UtcNow
                    },
                    new Lab()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Option 2",
                        LabDate = DateTime.UtcNow.AddDays(1)
                    },
                    new Lab()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Option 3",
                        LabDate = DateTime.UtcNow.AddDays(2)
                    }
                }
            },
            new Subject()
            {
                Id = Guid.NewGuid(),
                Name = "Mathematics",
                Labs = new List<Lab>()
                {
                    new Lab()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Option 1",
                        LabDate = DateTime.UtcNow
                    },
                    new Lab()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Option 2",
                        LabDate = DateTime.UtcNow.AddDays(1)
                    },
                    new Lab()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Option 3",
                        LabDate = DateTime.UtcNow.AddDays(2)
                    }
                }
            }
        };

        private static List<Student> Students = new List<Student>()
        {
            new Student()
            {
                Id = StudentId1,
                Email = "student1@test.com",
                Name = "Student 1",
                Subjects = new List<Subject>()
                {
                    Subjects[0]
                }
            },
            new Student()
            {
                Id = StudentId2,
                Email = "student2@test.com",
                Name = "Student 2",
                Subjects = new List<Subject>()
                {
                    Subjects[1]
                }
            },
            new Student()
            {
                Id = StudentId3,
                Email = "student3@test.com",
                Name = "Student 3",
                Subjects = new List<Subject>()
                {
                    Subjects[0],
                    Subjects[1]
                }
            }
        };

        #endregion
    }
}
