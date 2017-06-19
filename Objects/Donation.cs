using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Charity.Objects;

namespace Charity.Objects
{
  public class Donation
  {
    public int Id {get; set;}
    public int UserId {get; set;}
    public int CampaignId {get; set;}
    public int DonationAmount {get; set;}
    public DateTime DonationDate {get; set;}

    public Donation(int userId, int campaignId, int donationAmount, DateTime donationDate, int id = 0)
    {
      UserId = userId;
      CampaignId = campaignId;
      DonationAmount = donationAmount;
      DonationDate = donationDate;
      Id = id;
    }

    public override bool Equals(System.Object otherDonation)
    {
      if (!(otherDonation is Donation))
      {
        return false;
      }
      else
      {
        Donation newDonation = (Donation)otherDonation;
        return (this.Id == newDonation.Id &&
                this.UserId == newDonation.UserId &&
                this.CampaignId == newDonation.CampaignId &&
                this.DonationAmount == newDonation.DonationAmount &&
                this.DonationDate == newDonation.DonationDate);
      }
      return false;
    }

    public static List<Donation> GetAll()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("SELECT * FROM donations;", DB.GetConnection());
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Donation> donations = new List<Donation>{};
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int userId = rdr.GetInt32(1);
        int campaignId = rdr.GetInt32(2);
        int donationAmount = rdr.GetInt32(3);
        DateTime donationDate = rdr.GetDateTime(4);

        Donation newDonation = new Donation(userId, campaignId, donationAmount, donationDate, id);

        donations.Add(newDonation);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();

      return donations;
    }

    public void Save()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("INSERT INTO donations (user_id, campaign_id, donation_amount, donation_date) OUTPUT INSERTED.id VALUES (@UserId, @CampaignId, @DonationAmount, @DonationDate)", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@UserId", this.UserId));
      cmd.Parameters.Add(new SqlParameter("@CampaignId", this.CampaignId));
      cmd.Parameters.Add(new SqlParameter("@DonationAmount", this.DonationAmount));
      cmd.Parameters.Add(new SqlParameter("@DonationDate", this.DonationDate));

      SqlDataReader rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }
      DB.CloseConnection();
    }

    public static void DeleteAll()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("DELETE FROM donations;", DB.GetConnection());

      cmd.ExecuteNonQuery();
      DB.CloseConnection();
    }
  }
}
