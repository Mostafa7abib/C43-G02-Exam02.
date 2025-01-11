using System;

namespace Examination_system
{
    internal class TrueOrFalseQuestion : Question
    {
        public bool CorrectAnswer { get; set; }

        public TrueOrFalseQuestion(string header, string body, int mark) : base(header, body, mark) { }

        public TrueOrFalseQuestion(string header, string body, int mark, bool correctAnswer)
            : base(header, body, mark)
        {
            this.CorrectAnswer = correctAnswer;
        }

        public override void TheQuestion()
        {
            base.TheQuestion();
            Console.WriteLine("1. True");
            Console.WriteLine("2. False");
        }
    }
}
