using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShereYourMovies.Classes
{
    #region Yhden Elokuvan Tiedot internetistä
    [Table(Name = "Movie")]
    public class Movie
    {
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
        public string Poster { get; set; }
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
    }
    #endregion
}
