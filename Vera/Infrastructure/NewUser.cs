using System;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Vera.Models;


namespace Vera.Infrastructure{
class NewUser
{
    ILogger<NewUser> logger;
    
    public void Domain(){
         try{
                string domain = "localhost"; // замените example.com на ваш домен
                string userName = "s_ant";
                string password = "sergy7";
                string ldapConnection = "LDAP://" + domain;
                PrincipalContext context = new PrincipalContext(ContextType.Domain, domain, userName, password);
    
                if (!context.ValidateCredentials(userName, password))
                {
                    Console.WriteLine("Incorrect login or password.");                    
                }
    
                Console.WriteLine("Authentication successful.");
            }            
            catch(Exception ex){
                Console.WriteLine(ex.Message);
            }

            // using (var context = new PrincipalContext(ContextType.Domain, "yourdomain.com"))
            // {
            //     using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
            //     {
            //         foreach (var result in searcher.FindAll())
            //         {
            //             DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
            //             Console.WriteLine("First Name: " + de.Properties["givenName"].Value);
            //             Console.WriteLine("Last Name : " + de.Properties["sn"].Value);
            //             Console.WriteLine("SAM account name   : " + de.Properties["samAccountName"].Value);
            //             Console.WriteLine("User principal name: " + de.Properties["userPrincipalName"].Value);
            //             Console.WriteLine();
            //         }
            //     }
            // }
            // Console.ReadLine();
    }

}

}
