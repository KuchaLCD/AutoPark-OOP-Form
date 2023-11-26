﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class UserDB
    {
        public string Login { get; }
        public string Password { get; }
        public string FirstName { get; }
        public string SureName { get; }
        public string IDPos { get; }
        public string AvatarPicture { get; }
        public override string ToString()
        {
            string st = string.Format("{0} {1}", FirstName, SureName);
            return st;
        }
        public UserDB(string login, string password, string firstName, string sureName, string idPos)
        {
            this.Login = login;
            this.Password = password;
            this.FirstName = firstName;
            this.SureName = sureName;
            this.IDPos = idPos;
        }
        public UserDB(string login, string password, string firstName, string sureName, string idPos, string avatarPicture)
        {
            this.Login = login;
            this.Password = password;
            this.FirstName = firstName;
            this.SureName = sureName;
            this.IDPos = idPos;
            this.AvatarPicture = avatarPicture;
        }
        //public UserDB(string login, string firstName, string avatarPicture)
        //{
        //    this.Login = login;
        //    this.FirstName = firstName;
        //    this.AvatarPicture = avatarPicture;
        //}
    }
}
