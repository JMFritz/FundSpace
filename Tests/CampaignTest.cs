using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Charity.Objects;

namespace Charity
{
  [Collection("Charity")]
  public class CampaignTest : IDisposable
  {
    public CampaignTest()
    {
      // Console.WriteLine("Hello");
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=charity_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void CampaignEquals_CompareCampaigns_ReturnCampaignsAreEqual()
    {
      DateTime start = DateTime.Now;
      DateTime end = new DateTime(2018,1,1);

      Campaign testCampaign = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 50, 0, start, end, 1);
      Campaign testCampaign2 = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 50, 0, start, end, 1);

      Assert.Equal(testCampaign, testCampaign2);
    }

    [Fact]
    public void CampaignGetAll_RetrieveAllCampaigns_ReturnEmptyList()
    {
      List<Campaign> campaignList = Campaign.GetAll();
      List<Campaign> campaignList2 = new List<Campaign>{};

      Assert.Equal(campaignList, campaignList2);
    }

    [Fact]
    public void CampaignSave_SavesToDatabase_ReturnCampaign()
    {
      DateTime start = new DateTime(2017,1,1);
      DateTime end = new DateTime(2018,1,1);

      Campaign testCampaign = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 50, 0, start, end, 1);
      testCampaign.Save();
      Campaign testCampaign2 = Campaign.GetAll()[0];

      Assert.Equal(testCampaign, testCampaign2);
    }

    [Fact]
    public void CampaignFind_FindSingleCampaign_ReturnFoundCampaign()
    {
      DateTime start = new DateTime(2017,1,1);
      DateTime end = new DateTime(2018,1,1);

      Campaign testCampaign = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 50, 0, start, end, 1);
      testCampaign.Save();

      Campaign foundCampaign = Campaign.Find(testCampaign.Id);
      Assert.Equal(testCampaign, foundCampaign);
    }

    [Fact]
    public void CampaignAddDonation_AddDonationToCampaign_ReturnUser()
    {
      DateTime start = new DateTime(2017,1,1);
      DateTime end = new DateTime(2018,1,1);

      Campaign testCampaign = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 50, 0, start, end, 1);
      testCampaign.Save();

      ContactInformation info = new ContactInformation("950 W.Burnside, Portland", "useremail@gmail.com", "(123)456-7890");
      User testUser = new User(2, "Anna", "anna123", "123",  info);
      testUser.Save();

      testCampaign.AddDonation(testUser);

      List<User> result = testCampaign.GetDonations();
      List<User> testList = new List<User>{testUser};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void CampaignGetDonations_RetrieveAllDonations_ReturnMatchingUser()
    {
      DateTime start = new DateTime(2017,1,1);
      DateTime end = new DateTime(2018,1,1);

      Campaign testCampaign = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 50, 0, start, end, 1);
      testCampaign.Save();

      ContactInformation info = new ContactInformation("950 W.Burnside, Portland", "useremail@gmail.com", "(123)456-7890");
      User testUser = new User(2, "Anna", "anna123", "123",  info);
      testUser.Save();

      ContactInformation info2 = new ContactInformation("1900 SW Gilligan, Portland", "useremail@gmail.com", "(123)456-7890");
      User testUser2 = new User(2, "John", "john123", "123",  info2);
      testUser2.Save();

      testCampaign.AddDonation(testUser);
      List<User> result = testCampaign.GetDonations();
      List<User> testList = new List<User> {testUser};

      Assert.Equal(testList, result);
    }

    public void Dispose()
    {
      User.DeleteAll();
      Campaign.DeleteAll();
    }
  }
}
