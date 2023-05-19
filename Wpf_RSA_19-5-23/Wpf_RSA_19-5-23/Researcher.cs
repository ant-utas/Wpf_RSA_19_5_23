using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_RSA_19_5_23
{
    public class Researcher
    {
        //fields : int ID, Type Type, string GivenName, string FamilyName, Title Title, string Unit, string School, string Email, string Photo 
        //These properties have auto-generated getters (accessors <- fancy name) and
        //setters (mutators <- again fancier name); hence, are entirely public
        //fields are set to public - an access modifier which allows other classes to access that field
        public int ID { get; set; }//unique id for identifying researcher //PK
        public string GivenName { get; set; }//first name
        public string FamilyName { get; set; }//surname
        public string Title { get; set; }//Ms, Mrs, Mr, Dr, Xir, Sir, etc. could this also be enum?
        public string Unit { get; set; }
        public string Campus { get; set; }//if only UTAS, can use enum perhaps?
        public string Email { get; set; }//is there an email object in c#
        public string Photo { get; set; }//url string
        public DateTime UTASStart { get; set; }
        public DateTime CurrentStart { get; set; }
        public List<Position> Positions { get; set; }//Researcher occupie(s/d) a single or multiple position
        public List<Publication> Publications { get; set; }//List of publications Researcher has (co-)authored
        public Position GetCurrentJob()
        {
            return Positions.ElementAt(Positions.Count - 1);
        }

        public string CurrentJobTitle()
        {
            return Positions.ElementAt(Positions.Count - 1).ToTitle();
        }
        public DateTime CurrentJobStart()
        {
            return Positions.ElementAt(Positions.Count - 1).StartDate;//most recent job
        }
        public Position GetEarliestJob()
        {
            return Positions.ElementAt(0);
        }
        public DateTime EarliestStart()
        {
            return Positions.ElementAt(0).StartDate;
        }
        public float Tenure()
        {
            return (float)(DateTime.Now - Positions[0].StartDate).TotalDays;//find way to return float of days working
        }
        public int PublictionsCount()
        {
            return Publications.Count;
        }
        public override string ToString()
        {
            return FamilyName + ", " + GivenName + " (" + Title+")"; //format: surname, firstname, Title ie Macabantad, Antonio Dr. i think
        }
    }
}