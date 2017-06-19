using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Charity.Objects
{
  public class Campaign
  {
    public int Id {get; set;}
    public string Name {get; set;}
    public string Description {get; set;}
    public int Goal {get; set;}
    public int Balance {get; set;}
    public DateTime Start {get; set;}
    public DateTime End {get; set;}
    public int CategoryId {get; set;}

    public Campaign(string name, string description, int goal, int balance, DateTime start, DateTime end, int categoryId, int id = 0)
    {
      Name = name;
      Description = description;
      Goal = goal;
      Balance = balance;
      Start = start;
      End = end;
      CategoryId = categoryId;
      Id = id;
    }

    public override bool Equals(System.Object otherCampaign)
    {
      if (!(otherCampaign is Campaign))
      {
        return false;
      }
      else
      {
        Campaign newCampaign = (Campaign)otherCampaign;
        return (this.Id == newCampaign.Id &&
                this.Name == newCampaign.Name &&
                this.Description == newCampaign.Description &&
                this.Goal == newCampaign.Goal &&
                this.Balance == newCampaign.Balance &&
                this.Start == newCampaign.Start &&
                this.End == newCampaign.End &&
                this.CategoryId == newCampaign.CategoryId);
      }
    }

    public static List<Campaign> GetAll()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("SELECT * FROM campaigns;", DB.GetConnection());
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Campaign> campaigns = new List<Campaign>{};
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string description = rdr.GetString(2);
        int goal = rdr.GetInt32(3);
        int balance = rdr.GetInt32(4);
        DateTime start = rdr.GetDateTime(5);
        DateTime end = rdr.GetDateTime(6);
        int categoryId = rdr.GetInt32(7);

        Campaign newCampaign = new Campaign(name, description, goal, balance, start, end, categoryId, id);
        campaigns.Add(newCampaign);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();

      return campaigns;
    }

    public void Save()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("INSERT INTO campaigns (name, description, goal_amt, current_amt, start_date, end_date, category_id) OUTPUT INSERTED.id VALUES (@Name, @Description, @Goal, @Balance, @Start, @End, @CategoryId)", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@Name", this.Name));
      cmd.Parameters.Add(new SqlParameter("@Description", this.Description));
      cmd.Parameters.Add(new SqlParameter("@Goal", this.Goal));
      cmd.Parameters.Add(new SqlParameter("@Balance", this.Balance));
      cmd.Parameters.Add(new SqlParameter("@Start", this.Start));
      cmd.Parameters.Add(new SqlParameter("@End", this.End));
      cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));

      SqlDataReader rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();
    }

    public static Campaign Find(int id)
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("SELECT * FROM campaigns WHERE id = @CampaignId;", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@CampaignId", id));
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundCampaignId = 0;
      string foundCampaignName = null;
      string foundCampaignDescription = null;
      int foundCampaignGoal = 0;
      int foundCampaignBalance = 0;
      DateTime foundCampaignStart = default(DateTime);
      DateTime foundCampaignEnd = default(DateTime);
      int foundCampaignCategoryId = 0;

      while(rdr.Read())
      {
        foundCampaignId = rdr.GetInt32(0);
        foundCampaignName = rdr.GetString(1);
        foundCampaignDescription = rdr.GetString(2);
        foundCampaignGoal = rdr.GetInt32(3);
        foundCampaignBalance = rdr.GetInt32(4);
        foundCampaignStart = rdr.GetDateTime(5);
        foundCampaignEnd = rdr.GetDateTime(6);
        foundCampaignCategoryId = rdr.GetInt32(7);
      }

      Campaign foundCampaign = new Campaign(foundCampaignName, foundCampaignDescription, foundCampaignGoal, foundCampaignBalance, foundCampaignStart, foundCampaignEnd, foundCampaignCategoryId, foundCampaignId);

      if (rdr != null)
      {
        rdr.Close();
      }

      DB.CloseConnection();
      return foundCampaign;
    }

    public void AddDonation(User newUser)
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("INSERT INTO donations (campaign_id, user_id) VALUES (@CampaignId, @UserId);", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@CampiagnId", this.id));
      cmd.Parameters.Add(new SqlParameter("UserId", newUser.Id));

      cmd.ExecuteNonQuery();
      DB.CloseConnection();
    }
    public List<User> GetDonations()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("SELECT users.* FROM campaigns JOIN donations ON (campaigns.id = donations.campaign_id) JOIN users ON (donations.user_id = users.id) WHERE campaigns.id = @CampaignId ORDER BY name;", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@CampaignId", this.Id));

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Users> users = new List<users> {};

      while(rdr.Read())
      {
        int thisUserId = rdr.GetInt32(0);
        int roleId = rdr.GetInt32(1);
        string name = rdr.GetString(2);
        string login = rdr.GetString(3);
        string password = rdr.GetString(4);
        string address = rdr.GetString(5);
        string phoneNumber = rdr.GetString(6);
        string email = rdr.GetString(7);

        ContactInformation info = new ContactInformation(address, phoneNumber, email);
        result = new User(roleId, name, login, password, info, thisUserId);
      }
      if (rdr != null)
      {
        rdr.Close();
      }

      DB.CloseConnection();
      return users;
    }

    public static void DeleteAll()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("DELETE FROM campaigns;", DB.GetConnection());

      cmd.ExecuteNonQuery();
      DB.CloseConnection();
    }
  }
}