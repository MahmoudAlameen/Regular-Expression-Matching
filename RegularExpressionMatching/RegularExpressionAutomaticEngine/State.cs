using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode_ProblemSolving_FirstWeek.RegularExpressionMatching.RegularExpressionAutomaticEngine
{
    public class State
    {
        public char Letter { get; set; }
        public  List<TransferPath> TransferPaths { get; set;  }
        public State(char letter)
        {
            Letter = letter;
            TransferPaths = new List<TransferPath>();    
        }
        public State(char letter, List<TransferPath> transferPaths)
        {
            Letter = letter;
            foreach (var transferPath in transferPaths)
            {
                transferPath.SourceState = this;
            }
            TransferPaths = transferPaths;
        }
    }
}
