
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_RSA_19_5_23
{
    abstract class DataBase
    {
        //These would not be hard coded in the source file normally, but read from the application's settings (and, ideally, with some amount of basic encryption applied)
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";//myphpadmin link

        private static MySqlConnection conn = null;

        //This method is a gift to you because .NET's approach to converting strings to enums is so horribly broken
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Creates and returns (but does not open) the connection to the database.
        /// </summary>
        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                //Note: This approach is not thread-safe
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }

        //loading researchers from phpMyAdmin to List<Researcher> to be called in ResearcherController
        public static List<Researcher> LoadResearchers()
        {
            List<Researcher> researcherList = new List<Researcher>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader researchdr = null;
            MySqlDataReader positiondr = null;

            try
            {
                conn.Open();
                MySqlCommand selectResearcher = new MySqlCommand("select id, type, given_name, family_name, title, unit, campus, email, photo, degree, supervisor_id, level, utas_start, current_start from researcher", conn);
                researchdr = selectResearcher.ExecuteReader();

                while (researchdr.Read())
                {
                    //could have used enum but enum to string, i feel it is redundant
                    if (researchdr.GetString(1) == "Student")
                    {
                        researcherList.Add(new Student { ID = researchdr.GetInt32(0), GivenName = researchdr.GetString(2), FamilyName = researchdr.GetString(3), Title = researchdr.GetString(4), Unit = researchdr.GetString(5), Campus = researchdr.GetString(6), Email = researchdr.GetString(7), Photo = researchdr.GetString(8), UTASStart = researchdr.GetDateTime(12), CurrentStart = researchdr.GetDateTime(13), Degree = researchdr.GetString(9), Positions = new List<Position>() { new Position { EmploymentLevel = EmploymentLevel.Student, } }, Publications = new List<Publication>() });//need empty list because otherwise filter functions will fail when searching thru uninitialized list
                    }

                    if (researchdr.GetString(1) == "Staff")
                    {
                        researcherList.Add(new Staff { ID = researchdr.GetInt32(0), GivenName = researchdr.GetString(2), FamilyName = researchdr.GetString(3), Title = researchdr.GetString(4), Unit = researchdr.GetString(5), Campus = researchdr.GetString(6), Email = researchdr.GetString(7), Photo = researchdr.GetString(8), UTASStart = researchdr.GetDateTime(12), CurrentStart = researchdr.GetDateTime(13), Positions = new List<Position>(), Publications = new List<Publication>() });
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (researchdr != null)
                {
                    researchdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            try
            {
                conn.Open();
                MySqlCommand selectPosition = new MySqlCommand("select id, level, start, end from position", conn);
                positiondr = selectPosition.ExecuteReader();

                while (positiondr.Read())
                {
                    //i wanted to find a more efficient way to do this but must close ExectureReader() before opening new one
                    foreach (Researcher r in researcherList)
                    {
                        if (positiondr.GetInt32(0) == r.ID)
                        {
                            if (!positiondr.IsDBNull(3))
                            {
                                r.Positions.Add(new Position { EmploymentLevel = ParseEnum<EmploymentLevel>(positiondr.GetString(1)), StartDate = positiondr.GetDateTime(2), EndDate = positiondr.GetDateTime(3) });

                            }
                            else
                            {
                                r.Positions.Add(new Position { EmploymentLevel = ParseEnum<EmploymentLevel>(positiondr.GetString(1)), StartDate = positiondr.GetDateTime(2) });
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (researchdr != null)
                {
                    researchdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return researcherList;
        }
        //loading publications from phpMyAdmin to List<Publication> to be called in PublicationController
        public static List<Publication> LoadPublications()
        {
            List<Publication> publicationList = new List<Publication>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader publicationdr = null;

            try
            {
                conn.Open();
                MySqlCommand selectPublication = new MySqlCommand("select doi, title, ranking, authors, year, type, cite_as, available from publication", conn);
                publicationdr = selectPublication.ExecuteReader();

                while (publicationdr.Read())
                {
                    //adding publication to publication list     
                    //publicationList.Add(new Publication { DOI = publicationdr.GetString(0), Title = publicationdr.GetString(1), Year = publicationdr.GetDateTime(4), Type = ParseEnum<OutputType>(publicationdr.GetString(5)), CiteAs = publicationdr.GetString(6), Available = publicationdr.GetDateTime(7)});
                    publicationList.Add(new Publication { DOI = publicationdr.GetString(0), Title = publicationdr.GetString(1), Year = publicationdr.GetInt32(4), Type = ParseEnum<OutputType>(publicationdr.GetString(5)), CiteAs = publicationdr.GetString(6), Available = publicationdr.GetDateTime(7) });
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (publicationdr != null)
                {
                    publicationdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return publicationList;
        }
    }
}