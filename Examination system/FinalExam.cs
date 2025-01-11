using System;

namespace Examination_system
{
    internal class FinalExam : Exam
    {
        public FinalExam(int time, int numberOfQuestions, Subject subject)
            : base(time, numberOfQuestions, subject) { }

        public override void ExamImplementation()
        {
            Console.WriteLine("Final Exam:");
        }
    }
}
