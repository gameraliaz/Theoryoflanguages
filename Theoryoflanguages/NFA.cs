using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Theoryoflanguages
{
    public class NFA : FiniteMachine
    {
        public List<q> Q { get; set; }
        public List<char> Sigma { get; set; }
        public List<SDelta> Delta { get; set; }
        public q StartState { get; set; }
        public List<q> FinalStates { get; set; }
        public NFA()
        {
            this.Q = new List<q>();
            this.Sigma = new List<char>();
            this.Delta = new List<SDelta>();
            this.StartState = new q();
            this.FinalStates = new List<q>();
        }
        public NFA(List<q> Q, List<char> Sigma, List<SDelta> Delta, q StartState, List<q> FinalStates)
        {
            this.Q = Q;
            this.Sigma = Sigma;
            this.Delta = Delta;
            this.StartState = StartState;
            this.FinalStates = FinalStates;
        }

        public bool Read(string sentence)
        {
            List<q> states= _deltaStar(StartState, sentence);
            foreach(q s in states)
            { 
                MessageBox.Show(s.Name);
            }
            foreach (q fq in FinalStates)
            {
                if (states.Contains(fq))
                    return true;
            }
            return false;
            
        }

        private List<q> _deltaStar(q cState,string sentence)
        {
            List<q> FState = new List<q>();
            List<q> State = new List<q>();
            State.Add(cState);
            if(sentence=="")
            {
                State.AddRange(_deltaStar(cState, "L"));
                return State;
            }
                
            State=_deltaS(cState, sentence[0]);
            foreach(q cs in State)
            {
                List<q> _fState = _deltaStar(cs, sentence.Substring(1));
                foreach(q fq in _fState)
                {
                    if(!FState.Contains(fq))
                        FState.Add(fq);
                }
            }
            return FState;
        }
        private List<q> _deltaS(q cState,char c)
        {
            List<q> State1 = new List<q>();
            List<q> State2 = new List<q>();
            foreach (SDelta d in Delta)
            {
                if(d.OriState == cState )
                {
                    if(d.ReadChar == c)
                    {
                        State1.Add(d.DesState);
                    }else if(d.ReadChar == 'L' && c!='L')
                    {
                        State2.Add(d.DesState);
                    }
                }    
            }
            foreach( q s in State1)
            {
                State1.AddRange(_deltaS(s, 'L'));
                break;
            }
            foreach (q s in State2)
            {
                State1.AddRange(_deltaS(s, c));
            }

            return State1;
        }


        public DFA ToDFA()
        {
            DFA dfa ;
            List<q> Qd = new List<q>();
            List<SDelta> Deltad = new List<SDelta>();
            List<q> FinalStatesd = new List<q>();






            




            dfa = new DFA(Qd, Sigma, Deltad, StartState, FinalStatesd);
            return dfa;
        }
        
    }
}
