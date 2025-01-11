using System;

namespace Examination_system
{
    internal class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }

        public Answer(int answerId, string answerText)
        {
            this.AnswerId = answerId;
            this.AnswerText = answerText;
        }
    }
}
