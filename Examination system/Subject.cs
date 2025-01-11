using System;

namespace Examination_system
{
    internal class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public Subject(int subjectId, string subjectName)
        {
            this.SubjectId = subjectId;
            this.SubjectName = subjectName;
        }

        public override string ToString()
        {
            return $"Subject ID: {SubjectId}, Subject Name: {SubjectName}";
        }
    }
}
