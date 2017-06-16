using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Charity.Objects
{
  public class ContactInformation()
  {
    public string Address {get; set;}
    public string PhoneNumber {get; set;}
    public string Email {get; set;}

    public ContactInformation(string address, string phoneNumber, string email)
    {
      Address = address;
      PhoneNumber = phoneNumber;
      Email = email;
    }

    
  }


}
