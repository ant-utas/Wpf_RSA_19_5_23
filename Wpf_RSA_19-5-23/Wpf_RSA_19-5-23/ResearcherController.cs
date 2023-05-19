using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Wpf_RSA_19_5_23
{//ID, GivenName, FamilyName, Title, School, Email, Photo
    public class ResearcherController//researcher controller
    {
        List<Researcher> researchers;
        public List<Researcher> Workers { get { return researchers; } set { } }

        private ObservableCollection<Researcher> viewableResearcher;
        public ObservableCollection<Researcher> VisibleResearchers { get { return viewableResearcher; } set { } }



        public static T ParseEnum<T>(string value)//a gift
        {
            return (T)Enum.Parse(typeof(T), value);
        }
        public ResearcherController()//
        {
            researchers = DataBase.LoadResearchers();
            viewableResearcher = new ObservableCollection<Researcher>(researchers);
            /*foreach (Researcher r in researchers)
            {
                e.Skills = Agency.LoadTrainingSessions(e.ID);
            }*///TODO:load positions into researchers here!@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        }

        public ObservableCollection<Researcher> GetViewableList()
        {
            return VisibleResearchers;
        }

        public List<Researcher> FilterByLevel(string employmentLevel)
        {
            EmploymentLevel eL = ParseEnum<EmploymentLevel>(employmentLevel);
            var selected = from Researcher e in researchers
                           where e.GetCurrentJob().EmploymentLevel == eL
                           select e;
            return new List<Researcher>(selected);
        }

        public List<Researcher> FilterByName(string name)
        {
            var selected = from Researcher e in researchers
                           where (e.GivenName.ToUpper()).Contains(name.ToUpper()) || (e.FamilyName.ToUpper()).Contains(name.ToUpper())
                           select e;
            return new List<Researcher>(selected);
        }

        public Researcher LoadResearcherDetails()//need to figure out what this method does/ implement it
        {
            return new Researcher();
        }

        public void Display()
        {
            researchers.ForEach(Console.WriteLine);
        }
    }
}