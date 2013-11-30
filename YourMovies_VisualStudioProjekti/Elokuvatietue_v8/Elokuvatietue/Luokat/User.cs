
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Elokuvatietue
{
    [Table(Name="User")]
    public class User
    {
        
        private int _UserID;
        [Column(Storage = "_UserID", Name = "UserID", DbType = "BigInt IDENTITY NOT NULL", IsPrimaryKey = true, IsDbGenerated = true, UpdateCheck = UpdateCheck.Never)]
        public int UserID { get { return _UserID; } }
        [Column]
        public string UserName;

        [Column]
        public string Password;
   

        private EntitySet<Elokuva> _elokuvat;
        [Association(Storage = "_elokuvat", OtherKey = "UserID", DeleteRule = "CASCADE")]
        public EntitySet<Elokuva> Elokuvat
        {
            get { return this._elokuvat; }
            set { this._elokuvat.Assign(value); }
        }


        public User()
        {
            this._elokuvat = new EntitySet<Elokuva>();
        }
        
    }
}