using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_HW3
{
    public class MyLabelInfo
    {
        public string Name { get; set; }
        public string Patch { get; set; }

       public MyLabelInfo(string _name , string _patch) { 
        
            Name = _name;
            Patch = _patch;
        }

        public override string ToString()
        {
            return $"{Name}" ;
        }
    }
}
