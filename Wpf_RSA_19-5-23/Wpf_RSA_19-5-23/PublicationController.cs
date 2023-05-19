using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_RSA_19_5_23
{//ID, GivenName, FamilyName, Title, School, Email, Photo
    public class PublicationController//researcher controller
    {
        List<Publication> publications;
        public PublicationController()//
        {
            publications = DataBase.LoadPublications();
        }

        public List<Publication> FilterByTitle(string title)
        {
            var selected = from Publication p in publications
                           where p.Title.Contains(title)
                           select p;
            return new List<Publication>(selected);
        }

        public void Display()
        {
            publications.ForEach(Console.WriteLine);
        }
    }
}