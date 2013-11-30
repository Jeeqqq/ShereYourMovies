﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShereYourMovies.Classes
{
    
    public class YourMovies : DataContext
    {
          public Table<Elokuva> Elokuva;
          public Table<Movie> Movie;

          public YourMovies(SqlConnection connection) : base(connection) { }
        
    }
}