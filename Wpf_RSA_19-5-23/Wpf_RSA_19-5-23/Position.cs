using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_RSA_19_5_23
{
    public enum EmploymentLevel { Student, A, B, C, D, E };
    public class Position
    {
        //These properties have auto-generated getters (accessors <- fancy name) and
        //setters (mutators <- again fancier name); hence, are entirely public
        public EmploymentLevel EmploymentLevel { get; set; }//employment level of position - should be enum?

        public DateTime StartDate { get; set; }//time of commencement with position

        public DateTime EndDate { get; set; }//time of commencement with position
        public string Title()
        {
            return EmploymentLevel.ToString();
        }
        public string ToTitle()//
        {
            return "Current position level: " + (EmploymentLevel).ToString() + '\t';
        }
    }
}