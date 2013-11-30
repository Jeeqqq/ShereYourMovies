using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Elokuvatietue
{
    
    public static class UserController
    {

        ///*Authentikointi
        // * tarkistetaan hashattu salasana ja käyttäjä
        // */
        //public static bool Authenticate(string uName, string pass, ref YourMovies db)
        //{
           

        //    var users = from User in db.User
        //                select User;

        //    byte[] saltBytes = new byte[] { 12, 254, 62, 6, 7, 42, 2, 96 };
        //    byte[] saltedHashBytesPassword = new HMACMD5(saltBytes).ComputeHash(Encoding.UTF8.GetBytes(pass));


        //    string hashedPass = Convert.ToBase64String(saltedHashBytesPassword);

        //    foreach (User u in users)
        //    {
        //        if (u.UserName == uName && hashedPass == u.Password)
        //        {
                   
        //            return true;
        //        }

        //    }

        //    return false;

        //}
        //public static int getUserIdByName(string username, ref YourMovies db)
        //{
        //    var u = from User in db.User
        //            where User.UserName == username
        //            select User.UserID;

        //    return u.First();
        //}
        //public static User getUserByName(string username, ref YourMovies db)
        //{
        //    var u = from User in db.User
        //            where User.UserName == username
        //            select User;
        //    if (u.Count() == 0)
        //        return null;
        //    else return u.First();
        //}
        //public static string RegisterUser(string uName, string pass, ref YourMovies db)
        //{
           
        //    byte[] saltBytes = new byte[] { 12, 254, 62, 6, 7, 42, 2, 96 };
        //    byte[] saltedHashBytesPassword = new HMACMD5(saltBytes).ComputeHash(Encoding.UTF8.GetBytes(pass));


        //    string Pass = Convert.ToBase64String(saltedHashBytesPassword);
        //    string msg = "";
           
        //    if (UserExists(uName,ref db))
        //    {
        //        msg= "User allready exists";
        //    }
        //    else
        //    {

        //        User u = new User();
        //        u.UserName = uName;
        //        u.Password = Pass;
        //        try
        //        {
        //            db.User.InsertOnSubmit(u);
        //            db.SubmitChanges();
        //        }
        //        catch (Exception e)
        //        {
        //            msg = e.Message;
        //        }
        //    }
        //    return msg;
        //}

        //private static bool UserExists(string uName, ref YourMovies db)
        //{
           

        //    var users = from User in db.User
        //                select User;
        //    if (users.Count() > 0)
        //    {
        //        foreach (User u in users)
        //        {
        //            if (u.UserName == uName)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

 
    }
}