using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Elokuvatietue
{
    public class YourMovies : DataContext
    {
          public Table<Elokuva> Elokuva;
            public Table<Movie> Movie;

            public YourMovies(string connection) : base(connection) { }
        
    }
}