using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GoFundMe.Objects
{
  public class Contact
  {
    private int _id;
    private string _email;
    private string _address;
    private string _phoneNumber;


  public Contact()
  {
    _id = 0;
    _email = null;
    _address = null;
    _phoneNumber = null;
  }

  public Contact(string Email, string Address, string PhoneNumber, int Id = 0)
  {
    _id = Id;
    _email = Email;
    _address = Address;
    _phoneNumber = PhoneNumber;
  }

  public int GetId()
  {
    return _id;
  }

  public string GetEmail()
  {
    return _email;
  }

  public string GetAddress()
  {
    return _address;
  }

  public string GetPhoneNumber()
  {
    return _phoneNumber;
  }

  public override bool Equals(System.Object otherContact)
  {
    if (!(otherContact is Contact))
    {
      return false;
    }
    else
    {
      Contact newContact = (Contact) otherContact;
      bool idEquality = (this.GetId() == newContact.GetId());
      bool emailEquality = (this.GetEmail() == newContact.GetEmail());
      bool addressEquality = (this.GetAddress() == newContact.GetAddress());
      bool phoneNumberEquality = (this.GetPhoneNumber() == newContact.GetPhoneNumber());
      return (idEquality && emailEquality && addressEquality && phoneNumberEquality);
    }
  }

  public static void DeleteAll()
  {
    SqlConnection conn = DB.Connection();
    conn.Open();
    SqlCommand cmd = new SqlCommand("DELETE FROM contacts;", conn);
    cmd.ExecuteNonQuery();
    conn.Close();
  }

  public static List<Contact> GetAll()
  {
    List<Contact> allContacts = new List<Contact>{};

    SqlConnection conn = DB.Connection();
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM contacts;", conn);
    SqlDataReader rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      int contactId = rdr.GetInt32(0);
      string contactEmail = rdr.GetString(1);
      string contactAddress = rdr.GetString(2);
      string contactPhoneNumber = rdr.GetString(3);
      Contact newContact = new Contact(contactEmail, contactAddress, contactPhoneNumber, contactId);
      allContact.Add(newContact);
    }
    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
    return allContact;
  }

  public void Save()
  {
    SqlConnection conn = DB.Connection();
    conn.Open();

    SqlCommand cmd = new SqlCommand("INSERT INTO contacts (email, address, phoneNumber) OUTPUT INSERTED.id VALUES (@ContactEmail, @ContactAddress, @ContactPhoneNumber)", conn);

    SqlParameter emailParameter = new SqlParameter();
    emailParameter.ParameterName = "@ContactEmail";
    emailParameter.Value = this.GetEmail();

    SqlParameter addressParameter = new SqlParameter();
    addressParameter.ParameterName = "@ContactAddress";
    addressParameter.Value = this.GetAddress();

    SqlParameter phoneNumberParameter = new SqlParameter();
    phoneNumberParameter.ParameterName = "@ContactPhoneNumber";
    phoneNumberParameter.Value = this.GetPhoneNumber();

    cmd.Parameters.Add(emailParameter);
    cmd.Parameters.Add(addressParameter);
    cmd.Parameters.Add(phoneNumberParameter);
    SqlDataReader rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      this._id = rdr.GetInt32(0);
    }
    if (rdr != null)
    {
      rdr.Close();
    }
    if(conn != null)
    {
      conn.Close();
    }
  }

  public static Contact Find(int id)
  {
    SqlConnection conn = DB.Connection();
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM contacts WHERE id = @ContactId", conn);
    SqlParameter contactIdParameter = new SqlParameter();
    contactIdParameter.ParameterName = "@ContactId";
    contactIdParameter.Value = id.ToString();
    cmd.Parameters.Add(contactIdParameter);
    SqlDataReader rdr = cmd.ExecuteReader();

    int foundContactId = 0;
    string foundContactEmail = null;
    string foundContactAddress = null;
    string foundContactPhoneNumber = null;

    while(rdr.Read())
    {
      foundContactId = rdr.GetInt32(0);
      foundContactEmail = rdr.GetString(1);
      foundContactAddress = rdr.GetString(2);
      foundContactPhoneNumber = rdr.GetString(3);
    }
    Contact foundContact = new Contact(foundContactEmail, foundContactAddress, foundContactPhoneNumber, foundContactId);

    if (rdr != null)
   {
     rdr.Close();
   }
   if (conn != null)
   {
     conn.Close();
   }

   return foundContact;
  }
}
