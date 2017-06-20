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

    public List<Donation> GetDonations()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("SELECT * FROM donations WHERE campaign_id = @CampaignId;", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@CampaignId", this.Id));
      SqlDataReader rdr = cmd.ExecuteReader();

      int newId = 0;
      int newUserId = 0;
      int newCampaignId = 0;
      int newDonationAmount = 0;
      DateTime newDonationDate = default(DateTime);

      List<Donation> donations = new List<Donation> {};
      while(rdr.Read())
      {
        newId = rdr.GetInt32(0);
        newUserId = rdr.GetInt32(1);
        newCampaignId = rdr.GetInt32(2);
        newDonationAmount = rdr.GetInt32(3);
        newDonationDate = rdr.GetDateTime(4);
      }

      Donation newDonation = new Donation(newUserId, newCampaignId, newDonationAmount, newDonationDate, newId);
      donations.Add(newDonation);

      if(rdr != null)
      {
        rdr.Close();
      }

      DB.CloseConnection();
      return donations;
    }

    public void Update(string name, string description, int goalAmount, int currentAmount, DateTime startDate, DateTime endDate, int categoryId )
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("UPDATE campaigns SET name = @Name, description = @Description, goal_amt = @GoalAmount, current_amt = @CurrentAmount, start_date = @StartDate, end_date = @EndDate, category_id = @CategoryId OUTPUT INSERTED.name, INSERTED.description, INSERTED.goal_amt, INSERTED.current_amt, INSERTED.start_date, INSERTED.end_date, INSERTED.category_id WHERE id = @CampaignId;", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@Name", name));
      cmd.Parameters.Add(new SqlParameter("@Description", description));
      cmd.Parameters.Add(new SqlParameter("@GoalAmount", goalAmount));
      cmd.Parameters.Add(new SqlParameter("@CurrentAmount", currentAmount));
      cmd.Parameters.Add(new SqlParameter("@StartDate", startDate));
      cmd.Parameters.Add(new SqlParameter("@EndDate", endDate));
      cmd.Parameters.Add(new SqlParameter("@CategoryId", categoryId));
      cmd.Parameters.Add(new SqlParameter("@CampaignId", this.Id));

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Name = rdr.GetString(0);
        this.Description = rdr.GetString(1);
        this.Goal = rdr.GetInt32(2);
        this.Balance = rdr.GetInt32(3);
        this.Start = rdr.GetDateTime(4);
        this.End = rdr.GetDateTime(5);
        this.CategoryId = rdr.GetInt32(6);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();
    }

    public void DeleteSingleCampaign()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("DELETE FROM campaigns WHERE id = @CampaignId;", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@CampaignId", this.Id));
      cmd.ExecuteNonQuery();
      DB.CloseConnection();
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
