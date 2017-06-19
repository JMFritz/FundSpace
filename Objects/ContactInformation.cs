using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Charity.Objects
{
  public class ContactInformation
  {
    public string Address {get; set;}
    public string PhoneNumber {get; set;}
    public string Email {get; set;}

    public ContactInformation()
    {
      Address = null;
      PhoneNumber = null;
      Email = null;
    }
    public ContactInformation(string address, string phoneNumber, string email)
    {
      Address = address;
      PhoneNumber = phoneNumber;
      Email = email;
    }

    public override bool Equals(System.Object otherContact)
    {
      if (!(otherContact is ContactInformation))
      {
        return false;
      }
      else
      {
        ContactInformation newContact = (ContactInformation)otherContact;
        return (this.Address == newContact.Address &&
                this.PhoneNumber == newContact.PhoneNumber &&
                this.Email == newContact.Email);
      }
    }
  }
}
