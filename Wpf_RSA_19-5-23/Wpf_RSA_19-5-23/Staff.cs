using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_RSA_19_5_23
{
    class Staff : Researcher
    {
        public List<Student> Students { get; set; }//list of students being supervised
        public float ThreeYearAverage()
        {
            return 0;
        }
        public float Performance()
        {
            return 0;
        }
    }
}