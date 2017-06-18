using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Charity.Objects
{
  public class User
  {
    public int Id {get; set;}
    public int RoleId {get; set;}
    public string Name {get; set;}
    public string Login {get; set;}
    public string Password {get; set;}
    // public int DonateAmout {get; set;}
    public ContactInformation ContactInfo {get; set;}

    public User(int roleId, string name, string login, string password, ContactInformation contactInfo, int id = 0)
    {
      int RoleId = roleId;
      string Name = name;
      string Loging = login;
      string Password = password;
      // int DonateAmout = donateAmout;
      ContactInformation ContactInfo = contactInfo;
      int Id = id;
    }

    public override bool Equals(System.Object otherUser)
    {
      if (!(otherUser is User))
      {
        return false;
      }
      else
      {
        User newUser = (User)otherUser;
        return (this.Id == newUser.Id &&
                this.RoleId == newUser.RoleId &&
                this.Name == newUser.Name &&
                this.Login == newUser.Login &&
                this.Password == newUser.Login &&
                // this.DonateAmout == newUser.DonateAmout &&
                this.ContactInfo == newUser.ContactInfo &&
                this.Id == newUser.Id);
      }
    }
  }
}
