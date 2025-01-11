using System;

namespace Examination_system
{
    internal abstract class Question
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public int Mark { get; set; }

        public Question(string header, string body, int mark)
        {
            this.Header = header;
            this.Body = body;
            this.Mark = mark;
        }

        public virtual void TheQuestion()
        {
            Console.WriteLine($"Q: {Header} - {Body} (Mark: {Mark})");
        }
    }
}
