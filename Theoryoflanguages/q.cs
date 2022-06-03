using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theoryoflanguages
{
    public class q
    {
        public string Name { get; set; }
        public void SetNameWithSetOfQs(List<q> Q)
        {
            Name = "{";
            foreach (q qn in Q)
            {
                Name += qn.Name + ",";
            }
            Name=Name.Remove(Name.Length - 1);
            Name+="}";
        }
    }
}
