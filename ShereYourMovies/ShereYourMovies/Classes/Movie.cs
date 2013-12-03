using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShereYourMovies.Classes
{
    #region Yhden Elokuvan Tiedot internetistä
    [Table(Name = "Movie")]
    public class Movie
    {
        private static readonly Dictionary<string, PropertyInfo> _publicProperties;
        private int _movieID;

        [Column(DbType = "BigInt IDENTITY NOT NULL", IsPrimaryKey = true, IsDbGenerated = true)]
        public int MovieID
        {
            set { _movieID = value; }
            get { return _movieID; }
        }


        [Column]
        public string Title { get; set; }
        [Column]
        public string Year { get; set; }
        [Column]
        public string Rated { get; set; }
        [Column]
        public string Released { get; set; }
        [Column]
        public string Runtime { get; set; }
        [Column]
        public string Genre { get; set; }
        [Column]
        public string Director { get; set; }
        [Column]
        public string Writer { get; set; }
        [Column]
        public string Actors { get; set; }
        [Column]
        public string Plot { get; set; }
        [Column]
        private string _poster;

        
        public string Poster
        {
            set { _poster=value; }
            get
            {
                if (_poster == "tietoja ei löytynyt" || _poster == null || _poster == "N/A")
                {
                    return "~/Images/No-Photo-Available.jpg";
                }
                else{
                    return _poster;
                }
            } 
        }
        [Column]
        public string ImdbRating { get; set; }
        [Column]
        public string ImdbVotes { get; set; }
        [Column]
        public string ImdbID { get; set; }
        [Column]
        public string Type { get; set; }

        public Movie()
        {


        }
        static Movie()
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.SetProperty;

            _publicProperties = typeof(Movie).GetProperties(bindingFlags).ToDictionary(propertyInfo => propertyInfo.Name);
        }
        public void Update(string propertyName, string value)
        {
            PropertyInfo propertyInfo;
            _publicProperties.TryGetValue(propertyName, out propertyInfo);

            if (propertyInfo != null)
            {
                int q = 0;
                if (propertyInfo.PropertyType.Equals(q.GetType()))
                {
                    q = Int32.Parse(value);
                    propertyInfo.SetValue(this, q, null);
                }
                else
                    propertyInfo.SetValue(this, value, null);
            }
            else
            {
                throw new ArgumentException("Movie does not contain a property of the name " + propertyName, "propertyName");
            }
        }
    }
    #endregion
}
