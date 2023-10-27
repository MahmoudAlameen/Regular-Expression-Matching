using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode_ProblemSolving_FirstWeek.RegularExpressionMatching.RegularExpressionAutomaticEngine
{
    public class TransferPath
    {
        public  State SourceState { get; set; }
        public  State DestinationState { get; set; }
        public TransferPath(State sourceState)
        {
            SourceState = sourceState;
        }
        public TransferPath(State sourceState, State destinationState)
        {
            SourceState = sourceState;
            DestinationState = destinationState;
        }
    }
}
