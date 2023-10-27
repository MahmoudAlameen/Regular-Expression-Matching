using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LeetCode_ProblemSolving_FirstWeek.RegularExpressionMatching.RegularExpressionAutomaticEngine
{
    /// <summary>
    /// 
    /// Problem:- Given an input string s and a pattern p, implement regular expression matching with support for '.' and '*' where:
    /// •	'.' Matches any single character.
    /// •	'*' Matches zero or more of the preceding element.
    /// The matching should cover the entire input string (not partial).
    /// 
    /// 
    ///  steps of solution:-
    ///  1- implement  method for creating State Machine Regular Expression :-  state machine that simulate passed pattern  
    ///  2- implement method  that take State Machine  and string , pass the string through  state machine 
    ///             return true if the string chars passed through state machine to finite state ( string match pattern )
    ///             false other wise
    ///  State Machine Regular Expression  has states and arrows
    ///  there is base states : -
    ///     start state
    ///     finite state
    ///  the goal is that the string should take path from start state and arrive  to finite state to be acepted as matched
    ///  other wise will be considered as not matched
    ///  each arrow belong to state and directed to specefic state 
    /// </summary>

    public class RegularExpressionStateMachine
    {
        private string Pattern { get; set; }
        private State StartState { get; set; }
        private State FiniteState { get; set; }
        public RegularExpressionStateMachine(string pattern)
        {
            Pattern = pattern;
            BuildRegularExpressionStateMachine();
        }

        /// <summary>
        /// output of this method is generated  state machine start from start state and end with finite state 
        /// this generated state machine simulate the passed pattern 
        /// </summary>
        void BuildRegularExpressionStateMachine()
        {
            StartState = new State('S', new List<TransferPath> { new TransferPath(StartState) });
            FiniteState = new State('F');
            List<TransferPath> backPaths = new List<TransferPath>();
            // add to backPaths list transferPaths of StartState and this is startiing point for our RegAutomaticEngine
            backPaths.AddRange(StartState.TransferPaths);
            State newState;
            for (int index = 0; index < Pattern.Length; index++)
            {
                newState = new State(Pattern[index]);
                LinkStateToEngine(backPaths, newState);
                var newStatePath = new TransferPath(newState);
                newState.TransferPaths.Add(newStatePath);
                if (!IsLetterFollowedByAstrec(Pattern, index))
                {
                    backPaths.Clear();
                    backPaths.AddRange(newState.TransferPaths);
                }
                else
                {
                    var newelyCreatedPaths = AddTransferPath(backPaths.Select(p => p.SourceState).ToList(), null);
                    newState.TransferPaths.Insert(0,new TransferPath(newState, newState));
                    backPaths.Clear();
                    backPaths.AddRange(newelyCreatedPaths);
                    backPaths.Add(newStatePath);
                    index++;
                }
            }
            foreach (var path in backPaths)
                path.DestinationState = FiniteState;
        }
        public bool StartRegExAutomaticEngine(string s)
        {
           return IfStateHasAnyValidPaths(this.StartState, s, 0);
        }

        /// <summary>
        /// take the string and pass it through created stateMachine (RegAutomaticEngine) for checking that this string 
        /// is matched with pattern or not 
        /// </summary>
        /// <param name="startState"></param>
        /// <param name="word"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool IfStateHasAnyValidPaths(State startState, string  word, int wordIndex)
        {
            foreach(var path in startState.TransferPaths)
            {
                // if we in last char n word
                if (wordIndex > word.Length - 1)
                {
                    // if destination state of path is FiniteState and all word characters passed then word is matched 
                    if (path.DestinationState == FiniteState)
                        return true;
                    // if destination state of path is not finite state and word characters passed then we  go through all word characters
                    // and word is not matched
                    else
                        continue;
                }
                var destLetter = path.DestinationState.Letter;
                var wordIndexedLetter = word[wordIndex];

                if (destLetter != '.' && destLetter != wordIndexedLetter)
                    continue;

                if(IfStateHasAnyValidPaths(path.DestinationState, word, wordIndex+1))
                    return true;
            }
            return false;
        }
        // check that current letter is folowed by (*) or not 
        bool IsLetterFollowedByAstrec(string pattern, int index)
        {
            return index < pattern.Length - 1 && pattern[index + 1] == '*';
        }
        // linking state to state machine 
        void LinkStateToEngine(List<TransferPath> backPaths, State state)
        {
            foreach(var transferPath in backPaths)
            {
                transferPath.DestinationState = state;
            }
        }
       /// <summary>
       /// linking transition (transfer path) to state machine
       /// </summary>
       /// <param name="sourceStates"></param>
       /// <param name="destinationState"></param>
       /// <returns></returns>
        List<TransferPath> AddTransferPath(List<State> sourceStates, State destinationState)
        {
            List<TransferPath> newelyCreatedPaths = new List<TransferPath>();
            foreach (var sourceState in sourceStates)
            {
                var transferPath = new TransferPath(sourceState);
                transferPath.DestinationState = destinationState;
                sourceState.TransferPaths.Add(transferPath);
                newelyCreatedPaths.Add(transferPath);
            }
            return newelyCreatedPaths;
        }

    }
}
