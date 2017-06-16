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
    public int DonateAmout {get; set;}
    public ContactInformation ContactInfo {get; set;}

    public User(int roleId, string name, string login, string password, int donateAmout, ContactInformation contactInfo, int id = 0)
    {
      int RoleId = roleId;
      string Name = name;
      string Loging = login;
      string Password = password;
      int DonateAmout = donateAmout;
      ContactInformation ContactInfo = contactInfo;
      int Id = id;
    }
  }
}
