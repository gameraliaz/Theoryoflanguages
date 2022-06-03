using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theoryoflanguages
{
    interface FiniteMachine
    {
        public List<q> Q { get; set; }
        public List<char> Sigma { get; set; }
        public List<SDelta> Delta { get; set; }
        public q StartState { get; set; }
        public List<q> FinalStates { get; set; }

        public bool Read(string sentence);
    }
}
