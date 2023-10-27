using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode_ProblemSolving_FirstWeek.RegularExpressionMatching.RegularExpressionAutomaticEngine;

namespace LeetCode_ProblemSolving_FirstWeek.RegularExpressionMatching
{


    public static class RegularExpression
    {
        public  static bool IsMatch(string s, string p)
        {
            RegularExpressionStateMachine regExEngine = new RegularExpressionStateMachine(p);
            return regExEngine.StartRegExAutomaticEngine(s);
        }
    }
   
}
