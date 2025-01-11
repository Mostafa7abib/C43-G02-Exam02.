using System;

namespace Examination_system
{
    internal class PracticalExam : Exam
    {
        public PracticalExam(int time, int numberOfQuestions, Subject subject)
            : base(time, numberOfQuestions, subject) { }

        public override void ExamImplementation()
        {
            Console.WriteLine("Practical Exam:");
        }
    }
}
