using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_RSA_19_5_23
{
    public enum OutputType { Conference, Journal, Other };
    public class Publication
    {
        //These properties have auto-generated getters (accessors <- fancy name) and
        //setters (mutators <- again fancier name); hence, are entirely public
        public string DOI { get; set; }//employment level of position - should be enum?
        public string Title { get; set; }//time of commencement with position
        public int Year { get; set; }//time of commencement with position // not sure how to make year type in c# so using int instead
        public OutputType Type { get; set; }//refer to enum^
        public string CiteAs { get; set; }
        public DateTime Available { get; set; }
        public int Age()
        {
            return (int)(DateTime.Now - Available).TotalDays;
        }
        public override string ToString()
        {
            return Title; //format: surname, firstname, Title ie Macabantad, Antonio Dr. i think
        }
    }
}