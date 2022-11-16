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
        public List<BSigma> Sigma { get; set; }
        public List<SDelta> Delta { get; set; }
        public q StartState { get; set; }
        public List<q> FinalStates { get; set; }
        public DFA()
        {
            this.Q = new List<q>();
            this.Sigma = new List<BSigma>();
            this.Delta = new List<SDelta>();
            this.StartState = new q();
            this.FinalStates = new List<q>();
        }
        public DFA(List<q> Q,List<BSigma> Sigma,List<SDelta> Delta,q StartState,List<q> FinalStates)
        {
            this.Q = Q;
            this.Sigma = Sigma;
            this.Delta = Delta;
            this.StartState = StartState;
            this.FinalStates = FinalStates;
        }

        public virtual bool Read(string sentence)
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
        public virtual q Read(q startstate,char c)
        {
            q currentState = startstate;
            foreach (SDelta sd in Delta)
            {
                if (sd.OriState.Name == currentState.Name && sd.ReadChar == c)
                {
                    currentState = sd.DesState;
                    break;
                }
            }
            return currentState;
        }
        public virtual List<string> show()
        {
            List<string> list = new List<string>();
            string qs = "{";
            foreach (q qw in Q)
            {
                qs+=qw.Name+",";
            }
            if (Q.Count > 0 && qs != "{")
                qs = qs.Remove(qs.Length - 1);
            list.Add(qs);

            string qs1 = "{";
            foreach (BSigma c in Sigma)
            {
                qs1 += c.ReadChar.ToString() + ",";
            }
            if (Q.Count > 0 && qs1 != "{")
                qs1 = qs1.Remove(qs1.Length - 1);
            list.Add(qs1);

            string qs2 = "{";
            foreach (SDelta s in Delta)
            {
                qs2 += "d(" + s.OriState.Name + "," + s.ReadChar.ToString() + ")= " + s.DesState.Name + " , ";
            }
            if (Q.Count > 0 && qs2 != "{")
                qs2 = qs2.Remove(qs2.Length - 1);
            list.Add(qs2);

            list.Add(StartState.Name);

            string qs3 = "{";
            foreach (q qf in FinalStates)
            {
                qs3 += qf.Name + ",";
            }
            if (Q.Count > 0 && qs3 != "{")
                qs3 = qs.Remove(qs3.Length - 1);
            list.Add(qs);

            return list;
        }
    }
}
