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
        public List<BSigma> Sigma { get; set; }
        public List<SDelta> Delta { get; set; }
        public q StartState { get; set; }
        public List<q> FinalStates { get; set; }
        public NFA()
        {
            this.Q = new List<q>();
            this.Sigma = new List<BSigma>();
            this.Delta = new List<SDelta>();
            this.StartState = new q();
            this.FinalStates = new List<q>();
        }
        public NFA(List<q> Q, List<BSigma> Sigma, List<SDelta> Delta, q StartState, List<q> FinalStates)
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
                State.AddRange(_deltaStar(cState, "λ"));
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
                    }else if(d.ReadChar == 'λ' && c!= 'λ')
                    {
                        State2.Add(d.DesState);
                    }
                }    
            }
            foreach( q s in State1)
            {
                State1.AddRange(_deltaS(s, 'λ'));
                break;
            }
            foreach (q s in State2)
            {
                State1.AddRange(_deltaS(s, c));
            }

            return State1;
        }

        public List<string> show()
        {
            List<string> list = new List<string>();
            string qs = "{";
            foreach (q qw in Q)
            {
                qs += qw.Name + ",";
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
        public DFA ToDFA()
        {
            DFA dfa ;
            List<q> Qd = new List<q>();
            List<SDelta> Deltad = new List<SDelta>();
            List<q> FinalStatesd = new List<q>();

            List<List<q>> Nodes = new List<List<q>>();
            StartState.Name="{"+StartState.Name+"}";
            
            Qd.Add(StartState);

            List<q> StartstateL = new List<q>();
            StartstateL.Add(StartState);
            Nodes.Add(StartstateL);
            for (int i = 0; i < Nodes.Count; i++)
            {
                foreach (BSigma c in Sigma)
                {

                    List<SDelta> Deltad2 = new List<SDelta>();

                    for (int k = 0; k < Nodes[i].Count; k++)
                    {
                        q qi = new q();
                        List<q> newNodes = _deltaStar(Nodes[i][k], c.ReadChar.ToString());
                        qi.SetNameWithSetOfQs(newNodes);

                        bool rep = false;
                        foreach (q qd in Qd)
                        {
                            if (qd.Name == qi.Name)
                            {
                                rep = true;
                                break;
                            }
                        }
                        if (!rep)
                        {
                            Nodes.Add(newNodes);
                        }


                        SDelta sd = new SDelta();
                        sd.OriState = Nodes[i][k];
                        sd.DesState = qi;
                        sd.ReadChar = c.ReadChar;
                        Deltad2.Add(sd);
                    }
                    q nq = new q();
                    q dq = new q();
                    SDelta sd2 = new SDelta();
                    sd2.OriState = nq;
                    sd2.DesState = dq;
                    sd2.ReadChar = c.ReadChar;
                    List<q> desNodes = new List<q>();
                    nq.SetNameWithSetOfQs(Nodes[i]);

                    foreach (SDelta d2 in Deltad2)
                    {
                        desNodes.Add(d2.DesState);
                    }
                    dq.SetNameWithSetOfQs(desNodes);

                    bool repd = false;
                    foreach (SDelta del in Deltad)
                    { 
                        if (del.OriState.Name == sd2.OriState.Name && del.DesState.Name == sd2.DesState.Name && del.ReadChar==sd2.ReadChar)
                        {
                            repd = true;
                            break;
                        }
                    }
                    bool repq = false;
                    foreach (q qd in Qd)
                    {
                        if (qd.Name == dq.Name)
                        {
                            repq = true;
                            break;
                        }
                    }
                    if (!repd)
                        Deltad.Add(sd2);
                    if (!repq)
                        Qd.Add(dq);
                }
            }



            

            foreach(q fq in Qd)
            {
                bool ff=false;
                List<string> n = fq.Name.Split(',','}','{').ToList();
                foreach (string s in n)
                {
                    bool had = false;
                    foreach (q fs in FinalStates)
                    {
                        if(fs.Name == s)
                        {
                            foreach (q fsd in FinalStatesd)
                            {
                                if (fsd.Name == fq.Name)
                                {
                                    had = true;
                                    break;
                                }
                            }
                            if (!had)
                            {
                                FinalStatesd.Add(fq);
                                ff = true;
                                break;
                            }
                        }
                        if (had)
                            break;
                    }
                    if (ff || had)
                        break;
                }
            }




            dfa = new DFA(Qd, Sigma, Deltad, StartState, FinalStatesd);
            return dfa;
        }
        
    }
}
