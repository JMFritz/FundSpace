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
