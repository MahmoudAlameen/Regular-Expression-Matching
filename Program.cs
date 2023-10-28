using LeetCode_ProblemSolving_FirstWeek.RegularExpressionMatching.RegularExpressionAutomaticEngine;

namespace Regular_Expression_Matching
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunRegularExpressionStateMachine();
        }
        public static void RunRegularExpressionStateMachine()
        {
            Console.WriteLine("Regular Expression Engine Using State Machine :-");
            Console.WriteLine("Enter pattern:-");
            string pattern = Console.ReadLine();
            Console.WriteLine("Enter the string :-");
            string s = Console.ReadLine();

            RegularExpressionStateMachine regEx = new RegularExpressionStateMachine(pattern);
            var isMatched = regEx.StartRegExAutomaticEngine(s);
            if (isMatched)
                Console.WriteLine("pattern is matched");
            else
                Console.WriteLine("pattern is not matched");
        }
    }
}