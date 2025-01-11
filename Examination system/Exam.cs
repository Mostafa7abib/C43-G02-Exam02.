using System;

namespace Examination_system
{
    internal abstract class Exam
    {
        public int Time { get; set; }
        public int NumberOfQuestions { get; set; }
        public Subject Subject { get; set; }

        public Exam(int time, int numberOfQuestions, Subject subject)
        {
            this.Time = time;
            this.NumberOfQuestions = numberOfQuestions;
            this.Subject = subject;
        }

        public abstract void ExamImplementation();
    }
}
