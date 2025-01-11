using System;

namespace Examination_system
{
    internal class McqQuestion : Question
    {
        private Answer[] AnswerList { get; set; }
        public int CorrectIndex { get; set; }

        public McqQuestion(string header, string body, int mark)
            : base(header, body, mark)
        {
            AnswerList = new Answer[3];
        }

        public McqQuestion(string header, string body, int mark, Answer[] answers, int correctIndex)
            : this(header, body, mark)
        {
            this.AnswerList = answers;
            this.CorrectIndex = correctIndex;
        }

        public override void TheQuestion()
        {
            base.TheQuestion();
            if (AnswerList != null)
            {
                foreach (Answer answer in AnswerList)
                {
                    if (answer != null)
                    {
                        Console.WriteLine($"{answer.AnswerId}. {answer.AnswerText}");
                    }
                }
            }
        }
    }
}
