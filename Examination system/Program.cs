using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace Examination_system
{
    internal class Program
    {
        private static string GetValidStringInput() 
        { 
            string input; 
            do 
            { 
                input = Console.ReadLine(); 
                if (string.IsNullOrEmpty(input) || input.All(char.IsDigit)) 
                { 
                    Console.WriteLine("Please enter {String}"); 
                } 
            } 
            while (string.IsNullOrEmpty(input) || input.All(char.IsDigit)); 
            return input; 
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Subject Id:");
            int subjectId;
            while (!int.TryParse(Console.ReadLine(), out subjectId) || subjectId <= 0)
            {
                Console.WriteLine("Please Enter a number");
            }

            Console.WriteLine("Enter Subject Name {String}");
            string subjectName = GetValidStringInput();

            Subject subject = new Subject(subjectId, subjectName);
            Console.Clear();
            Console.WriteLine("Select Exam Type: 1 for Practical, 2 for Final");
            int examType;
            while (!int.TryParse(Console.ReadLine(), out examType) || (examType != 1 && examType != 2))
            {
                Console.WriteLine("Please Enter Number Of Exam {1|2}");
            }

            Console.WriteLine("Enter Time of Exam (60 minutes : 180 minutes):");
            int timeOfExam;
            while (!int.TryParse(Console.ReadLine(), out timeOfExam) || timeOfExam < 60 || timeOfExam > 180)
            {
                Console.WriteLine("Please Enter Correct Number");
            }

            Console.WriteLine("Enter Number of Questions:");
            int numberOfQuestions;
            while (!int.TryParse(Console.ReadLine(), out numberOfQuestions) || numberOfQuestions <= 0)
            {
                Console.WriteLine("Please Enter number");
            }

            Console.Clear();

            Exam exam;

            Question[] questions = new Question[numberOfQuestions];
            int[] userAnswers = new int[numberOfQuestions];
            int totalMarks = 0;
            if (examType == 1) //Practical 
            {
                exam = new PracticalExam(timeOfExam, numberOfQuestions, subject);
                Console.WriteLine("MCQ Exam");
                for (int i = 0; i < numberOfQuestions; i++)
                {
                    Answer[] answers = new Answer[3];
                    Console.WriteLine($"Enter Header for Question {i + 1}:");
                    string header = GetValidStringInput();

                    Console.WriteLine($"Enter Body for Question {i + 1}:");
                    string body = GetValidStringInput();

                    Console.WriteLine($"Enter Mark for Question {i + 1}:");
                    int mark;
                    while (!int.TryParse(Console.ReadLine(), out mark) || mark <= 0)
                    {
                        Console.WriteLine("Please Enter number");
                    }
                    totalMarks += mark;
                    for (int j = 0; j < 3; j++)
                    {
                        Console.WriteLine($"Enter Answer {j + 1} Text:");
                        string answerText = GetValidStringInput();
                        answers[j] = new Answer(j + 1, answerText);
                    }

                    Console.WriteLine("Enter the number of the correct answer:");
                    int correctIndex;
                    while (!int.TryParse(Console.ReadLine(), out correctIndex) || correctIndex < 1 || correctIndex > 3)
                    {
                        Console.WriteLine("Please Enter Correct Number");
                    }

                    questions[i] = new McqQuestion(header, body, mark, answers, correctIndex);
                }
            }
            else
            {
                exam = new FinalExam(timeOfExam, numberOfQuestions, subject);
                
                for (int i = 0; i < numberOfQuestions; i++)
                {
                    Console.WriteLine("Select Question Type: 1=> True/False, 2 => MCQ");
                    int questionType;
                    while (!int.TryParse(Console.ReadLine(), out questionType) || (questionType != 1 && questionType != 2))
                    {
                        Console.WriteLine("Please Enter 1 or 2");
                    }

                    Console.WriteLine($"Enter Header for Question {i + 1}:");
                    string header = GetValidStringInput();

                    Console.WriteLine($"Enter Body for Question {i + 1}:");
                    string body = GetValidStringInput();

                    Console.WriteLine($"Enter Mark for Question {i + 1}:");
                    int mark;
                    while (!int.TryParse(Console.ReadLine(), out mark) || mark <= 0)
                    {
                        Console.WriteLine("Please Enter number");
                    }
                    totalMarks += mark;

                    if (questionType == 1)
                    {
                        questions[i] = new TrueOrFalseQuestion(header, body, mark);

                        Console.WriteLine("Enter the number of the correct answer  [1=> true , 2=> False]");
                        int correctIndex;
                        while (!int.TryParse(Console.ReadLine(), out correctIndex) || correctIndex < 1 || correctIndex > 2)
                        {
                            Console.WriteLine("Please Enter Correct Number");
                        }

                        bool correctAnswer = correctIndex == 1;
                        ((TrueOrFalseQuestion)questions[i]).CorrectAnswer = correctAnswer;
                    }
                    else
                    {
                        Answer[] answers = new Answer[3];
                        Console.WriteLine("MCQ Exam");
                        for (int j = 0; j < 3; j++)
                        {
                            Console.WriteLine($"Enter Answer {j + 1} Text:");
                            string answerText = GetValidStringInput();
                            answers[j] = new Answer(j + 1, answerText);
                        }

                        Console.WriteLine("Enter the number of the correct answer:");
                        int correctIndex;
                        while (!int.TryParse(Console.ReadLine(), out correctIndex) || correctIndex < 1 || correctIndex > 3)
                        {
                            Console.WriteLine("Please Enter Correct Number");
                        }

                        questions[i] = new McqQuestion(header, body, mark, answers, correctIndex);
                    }
                }
            }

            Console.Clear();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("Start The Exam");
            Console.WriteLine("Subject Details:");
            Console.WriteLine(subject.ToString());
            Console.WriteLine();
            Console.WriteLine("Exam Details:");
            exam.ExamImplementation();
            Console.WriteLine();
            
            Console.WriteLine("Questions and Answers:");
            int totalScore = 0;
            for (int i = 0; i < questions.Length; i++)
            {
                questions[i].TheQuestion();
                Console.WriteLine("Enter your answer:");
                int userAnswer;
                while (!int.TryParse(Console.ReadLine(), out userAnswer))
                {
                    Console.WriteLine("Please enter a valid number:");
                }
                userAnswers[i] = userAnswer;

                if (questions[i] is TrueOrFalseQuestion trueFalseQuestion)
                {
                    if ((trueFalseQuestion.CorrectAnswer && userAnswer == 1) || (!trueFalseQuestion.CorrectAnswer && userAnswer == 2))
                    {
                        totalScore += trueFalseQuestion.Mark;
                    }
                }
                else if (questions[i] is McqQuestion mcqQuestion)
                {
                    if (userAnswer == mcqQuestion.CorrectIndex)
                    {
                        totalScore += mcqQuestion.Mark;
                    }
                }
            }
            stopwatch.Stop(); 
            TimeSpan timeTaken = stopwatch.Elapsed;
            Console.Beep(); 
            Console.Clear();

            Console.WriteLine("Exam Results:");
            for (int i = 0; i < questions.Length; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Question {i + 1}:");
                questions[i].TheQuestion();
                Console.WriteLine($"Your Answer: {userAnswers[i]}");

                if (questions[i] is TrueOrFalseQuestion trueFalseQuestion)
                {
                    Console.WriteLine($"Correct Answer: {(trueFalseQuestion.CorrectAnswer ? 1 : 2)}");
                }
                else if (questions[i] is McqQuestion mcqQuestion)
                {
                    Console.WriteLine($"Correct Answer: {mcqQuestion.CorrectIndex}");
                }
            }
            Console.WriteLine();
            Console.WriteLine($"Time taken: {timeTaken.Hours}h {timeTaken.Minutes}m {timeTaken.Seconds}s");
            Console.WriteLine($"You scored {totalScore} out of {totalMarks}");

            // Determine the grade as a percentage based on the score
            double percentage = (double)totalScore / totalMarks * 100;

            Console.WriteLine($"Your grade: {percentage:F2}%");
        }
    }
}
