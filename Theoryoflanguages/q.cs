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
            List<string> Qs = new List<string>();
            foreach (q qn in Q)
            {
                if (qn.Name.StartsWith("{"))
                {
                    string n = qn.Name.Replace("{","").Replace("}","");
                    List<string> Qns = n.Split(',').ToList();
                    foreach (string s in Qns)
                    {
                        if (!Qs.Contains(s))
                            Qs.Add(s);
                    }
                }else
                {
                    if(!Qs.Contains(qn.Name))
                        Qs.Add(qn.Name);
                }
                
            }
            Qs.Sort();
            foreach (string n in Qs)
            {
                if(n!="")
                    Name += n + ",";
            }
            if(Qs.Count> 0 && Name!="{")
                Name =Name.Remove(Name.Length - 1);
            Name+="}";
        }
    }
}
