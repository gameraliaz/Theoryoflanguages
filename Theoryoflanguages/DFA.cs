using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theoryoflanguages
{
    public class DFA : FiniteMachine
    {
        public List<q> Q { get; set; }
        public List<char> Sigma { get; set; }
        public List<SDelta> Delta { get; set; }
        public q StartState { get; set; }
        public List<q> FinalStates { get; set; }
        public DFA()
        {
            this.Q = new List<q>();
            this.Sigma = new List<char>();
            this.Delta = new List<SDelta>();
            this.StartState = new q();
            this.FinalStates = new List<q>();
        }
        public DFA(List<q> Q,List<char> Sigma,List<SDelta> Delta,q StartState,List<q> FinalStates)
        {
            this.Q = Q;
            this.Sigma = Sigma;
            this.Delta = Delta;
            this.StartState = StartState;
            this.FinalStates = FinalStates;
        }

        public bool Read(string sentence)
        {
            q currentState = StartState;
            foreach (char c in sentence)
            {
                bool cinSigmas=false;
                foreach (SDelta sd in Delta)
                {
                    if (sd.OriState.Name == currentState.Name && sd.ReadChar == c)
                    {
                        currentState = sd.DesState;
                        cinSigmas=true;
                        break;
                    }
                }
                if (!cinSigmas)
                    return false;
            }
            if (FinalStates.Contains(currentState))
                return true;
            return false;
        }
    }
}
