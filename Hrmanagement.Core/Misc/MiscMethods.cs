using Hrmanagement.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.Misc
{
    public class MiscMethods
    {


        //IFormFile
        public static string uploadFileToLocal(IFormFile file, string dirpath)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filepath = Path.Combine(dirpath, uniqueFileName);
            var fileStream = new FileStream(filepath, FileMode.Create);
            file.CopyTo(fileStream);
            fileStream.Dispose();
            return uniqueFileName;
            //return filepath;
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }


        public static bool VerifyPassword(string userInputPassword, string storedHashedPassword)
        {
            string userInputHash = MD5Hash(userInputPassword);

            return string.Equals(userInputHash, storedHashedPassword, StringComparison.OrdinalIgnoreCase);
        }


        public static LoggedInUserModel getLoginDetailByToken(HttpContext context)
        {
            var claimsDictionary = new Dictionary<string, string>();
            foreach (var claim in context.User.Claims)
            {
                if (claim.Type == ClaimTypes.Role)
                {
                    // For role claims, just store the value directly without concatenating
                    claimsDictionary[claim.Type] = claim.Value;
                }
                else
                {
                    // For other claims, concatenate values if the key already exists
                    if (claimsDictionary.ContainsKey(claim.Type))
                    {
                        claimsDictionary[claim.Type] += ";" + claim.Value;
                    }
                    else
                    {
                        claimsDictionary.Add(claim.Type, claim.Value);
                    }
                }
               
            }

            LoggedInUserModel lud = new LoggedInUserModel();

            foreach (var a in claimsDictionary)
            {
                if (a.Key == "id") { 
                    lud.Id = Int16.Parse(a.Value);
                }

                if (a.Key == "name") {
                    lud.FirstName = a.Value; 
                }

                if (a.Key == ClaimTypes.Email) {
                    lud.Email = a.Value; 
                }

                if (a.Key == "mobile") { 
                    lud.Mobile = a.Value; 
                }

                if (a.Key == ClaimTypes.Role) { 
                    lud.Role = a.Value;
                }

            }
            return lud;

        }
    }
}
