using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using PhotoInvasion.Models;

namespace PhotoInvasion
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            OAuthWebSecurity.RegisterMicrosoftClient(
                clientId: "000000004411504B",
                clientSecret: "8FTR0MgbvTNjPePdzl2haYMnVY1fPETj");

            OAuthWebSecurity.RegisterTwitterClient(
               consumerKey: "pMsZJOmnELsmD7du7jBCuKhcf",
               consumerSecret: "lmmlWU2wY64muu89sX9QadO8ODuRqzS17ZLWmoOafnO6hbkxHN");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "1453983538170294",
                appSecret: "a69f9a2560018e685cf11ca4a0c9e694");

            OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
