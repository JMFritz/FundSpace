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
    public ContactInformation ContactInfo {get; set;}

    public User()
    {
      Name = null;
      Login = null;
      Password = null;
      ContactInfo = null;
      RoleId = 0;
      Id = 0;
    }

    public User(string name, string login, string password, ContactInformation contactInfo, int roleId = 1, int id = 0)
    {
      Name = name;
      Login = login;
      Password = password;
      ContactInfo = contactInfo;
      RoleId = roleId;
      Id = id;
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
                this.Password == newUser.Password &&
                this.ContactInfo.Equals(newUser.ContactInfo));
      }
    }

    public static List<User> GetAll()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("SELECT * FROM users;", DB.GetConnection());
      SqlDataReader rdr = cmd.ExecuteReader();

      List<User> users = new List<User>{};
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int roleId = rdr.GetInt32(1);
        string name = rdr.GetString(2);
        string login = rdr.GetString(3);
        string password = rdr.GetString(4);
        string address = rdr.GetString(5);
        string phoneNumber = rdr.GetString(6);
        string email = rdr.GetString(7);

        ContactInformation info = new ContactInformation(address, phoneNumber, email);
        User user = new User(name, login, password, info, roleId, id);
        users.Add(user);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();

      return users;
    }

    public void Save()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("INSERT INTO users (role_id, name, login, password, address, phone_number, email) OUTPUT INSERTED.id VALUES (@RoleId, @Name, @Login, @Password, @Address, @PhoneNumber, @Email)", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@RoleId", this.RoleId));
      cmd.Parameters.Add(new SqlParameter("@Name", this.Name));
      cmd.Parameters.Add(new SqlParameter("@Login", this.Login));
      cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
      cmd.Parameters.Add(new SqlParameter("@Address", this.ContactInfo.Address));
      cmd.Parameters.Add(new SqlParameter("@PhoneNumber", this.ContactInfo.PhoneNumber));
      cmd.Parameters.Add(new SqlParameter("@Email", this.ContactInfo.Email));

      SqlDataReader rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }
      DB.CloseConnection();
    }

    public static User ValiateUser(string inputLogin, string inputPassword)
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE login = @Login AND password = @Password", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@Login", inputLogin));
      cmd.Parameters.Add(new SqlParameter("@Password", inputPassword));

      SqlDataReader rdr = cmd.ExecuteReader();
      User result = null;

      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int roleId = rdr.GetInt32(1);
        string name = rdr.GetString(2);
        string login = rdr.GetString(3);
        string password = rdr.GetString(4);
        string address = rdr.GetString(5);
        string phoneNumber = rdr.GetString(6);
        string email = rdr.GetString(7);

        ContactInformation info = new ContactInformation(address, phoneNumber, email);
        result = new User(name, login, password, info, roleId, id);
      }
      return result;
    }

    public static User Find(int searchId)
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE id = @UserId;", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@UserId", searchId));

      SqlDataReader rdr = cmd.ExecuteReader();

      User foundUser = new User();
      while (rdr.Read())
      {
        ContactInformation info = new ContactInformation();
        foundUser.Id = rdr.GetInt32(0);
        foundUser.RoleId = rdr.GetInt32(1);
        foundUser.Name = rdr.GetString(2);
        foundUser.Login = rdr.GetString(3);
        foundUser.Password = rdr.GetString(4);

        info.Address = rdr.GetString(5);
        info.PhoneNumber = rdr.GetString(6);
        info.Email = rdr.GetString(7);
        foundUser.ContactInfo = info;
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();

      return foundUser;
    }

    public List<Donation> GetDonations()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("SELECT * FROM donations WHERE @UserId = user_id;", DB.GetConnection());
      cmd.Parameters.Add(new SqlParameter("@UserId", this.Id));

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Donation> donations = new List<Donation>{};
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int userId = rdr.GetInt32(1);
        int campaignId = rdr.GetInt32(2);
        int donationAmount = rdr.GetInt32(3);
        DateTime donationDate = rdr.GetDateTime(4);

        Donation newDonation = new Donation(userId, campaignId, donationAmount, donationDate, id);

        donations.Add(newDonation);

        if (rdr != null)
        {
          rdr.Close();
        }
        DB.CloseConnection();
      }
      return donations;
    }

    public static void DeleteAll()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("DELETE FROM users;", DB.GetConnection());

      cmd.ExecuteNonQuery();
      DB.CloseConnection();
    }
  }
}
