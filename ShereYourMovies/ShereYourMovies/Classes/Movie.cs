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
    
    public class Root
    {
        public Root()
        {
            leffa = new Movie();
        }
        
        public Movie leffa { get; set; }
    }
   [Table(Name="Movie")]
    public class Movie
    {
       [Column(Storage = "MovieID", Name = "MovieID", DbType = "BigInt IDENTITY NOT NULL", IsPrimaryKey = true, IsDbGenerated = true, UpdateCheck = UpdateCheck.Never)]
       private int MovieID;
       [Column]
       public int ElokuvaID;
       private EntityRef<Elokuva> _Elokuva;
       [Association(Storage = "_Elokuva", ThisKey = "ElokuvaID")]
       public Elokuva Elokuva
       {
           get { return this._Elokuva.Entity; }
           set { this._Elokuva.Entity = value; }
       }
         [Column]
        public string Title{get;set;}
          [Column]
        public string Year{get;set;}
         [Column]
        public string Rated{get;set;}
          [Column]
        public string Released{get;set;}
         [Column]
        public string Runtime{get;set;}
          [Column]
        public string Genre{get;set;}
         [Column]
        public string Director{get;set;}
         [Column]
        public string Writer{get;set;}
          [Column]
        public string Actors{get;set;}
        [Column]
        public string Plot{get;set;}
         [Column] 
        public string Poster{get;set;}
          [Column]
        public string ImdbRating{get;set;}
         [Column]
        public string ImdbVotes{get;set;}
          [Column]
        public string ImdbID{get;set;}
         [Column] 
        public string Type{get;set;}
         
        public Movie()
        {
            
            this._Elokuva = default(EntityRef<Elokuva>);
        }
    }
}
