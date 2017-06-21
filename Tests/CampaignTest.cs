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
      DBConfiguration.ConnectionString = "Data Source=DESKTOP-6CVACGR\\SQLEXPRESS;Initial Catalog=charity_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void CampaignEquals_CompareCampaigns_ReturnCampaignsAreEqual()
    {
      DateTime start = DateTime.Now;
      DateTime end = new DateTime(2018,1,1);

      Campaign testCampaign = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 50, 0, start, end, 1, 1);
      Campaign testCampaign2 = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 50, 0, start, end, 1, 1);

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

      Campaign testCampaign = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 50, 0, start, end, 1, 1);
      testCampaign.Save();
      Campaign testCampaign2 = Campaign.GetAll()[0];

      Assert.Equal(testCampaign, testCampaign2);
    }

    [Fact]
    public void CampaignFind_FindSingleCampaign_ReturnFoundCampaign()
    {
      DateTime start = new DateTime(2017,1,1);
      DateTime end = new DateTime(2018,1,1);

      Campaign testCampaign = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 50, 0, start, end, 1, 1);
      testCampaign.Save();

      Campaign foundCampaign = Campaign.Find(testCampaign.Id);
      Assert.Equal(testCampaign, foundCampaign);
    }

    [Fact]
    public void Campaign_Update_UpdateCampaignInfo()
    {
      DateTime start = new DateTime(2018,1,1);
      DateTime end = new DateTime(2019,1,1);

      Campaign campaign = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 50, 0, start, end, 1, 1);
      campaign.Save();

      campaign.Update("Jun needs new laptop", "Help to buy new laptop", 50, 0, start, end, 1, 1);

      Campaign controlCampaign = new Campaign("Jun needs new laptop", "Help to buy new laptop", 50, 0, start, end, 1, 1, campaign.Id);

      Assert.Equal(controlCampaign, campaign);
    }

    [Fact]
    public void Campaign_Update_UpdateCampaignBalance()
    {
      DateTime start = new DateTime(2018,1,1);
      DateTime end = new DateTime(2019,1,1);

      Campaign campaign = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 500, 0, start, end, 1, 1);
      campaign.Save();

      campaign.UpdateBalance(30);

      Campaign controlCampaign = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 500, 30, start, end, 1, 1, campaign.Id);

      Assert.Equal(controlCampaign, campaign);
    }

    [Fact]
    public void Campaign_DeleteSingleCampaign_DeletesCampaign()
    {
      DateTime start = new DateTime(2018,1,1);
      DateTime end = new DateTime(2019,1,1);

      Campaign campaign1 = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 50, 0, start, end, 1, 1);
      campaign1.Save();
      Campaign campaign2 = new Campaign("Jun needs new laptop", "Help to buy new laptop", 50, 0, start, end, 1, 1);
      campaign2.Save();

      campaign1.DeleteSingleCampaign();
      List<Campaign> testList = Campaign.GetAll();
      List<Campaign> controlList = new List<Campaign>{campaign2};
      Assert.Equal(controlList, testList);
    }
    [Fact]
    public void CampaignGetDonations_RetrieveCampaignDonations_ReturnListOfDonations()
    {
      ContactInformation info = new ContactInformation("950 W.Burnside, Portland", "useremail@gmail.com", "(123)456-7890");
      User newUser = new User("Anna", "anna123", "123",  info);
      newUser.Save();

      DateTime start = new DateTime(2017,1,1);
      DateTime end = new DateTime(2018,1,1);

      Campaign testCampaign = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 50, 0, start, end, 1, 1);
      testCampaign.Save();

      Donation testDonation = newUser.MakeDonation(testCampaign, 10, new DateTime (2017,3,3));
      Donation controlDonation = new Donation(newUser.Id, testCampaign.Id, 10, new DateTime(2017,3,3), testDonation.Id);

      List<Donation> testDonations = testCampaign.GetDonations();
      Assert.Equal(controlDonation, testDonations[0]);
    }

    [Fact]
    public void Campaign_SearchByName_ReturnsAllMatches()
    {
      DateTime start = new DateTime(2018,1,1);
      DateTime end = new DateTime(2019,1,1);

      Campaign campaign1 = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 100, 0, start, end, 1, 1);
      campaign1.Save();
      Campaign campaign2 = new Campaign("Jun needs new laptop", "Help to buy new laptop", 500, 0, start, end, 1, 1);
      campaign2.Save();
      Campaign campaign3 = new Campaign("Jun's Sunburn", "Help Lina's sunburn", 50, 0, start, end, 1, 1);
      campaign3.Save();
      Campaign campaign4 = new Campaign("Lina needs new laptop", "Help to buy new laptop", 55, 0, start, end, 1, 1);
      campaign4.Save();

      List<Campaign> testList = Campaign.SearchByName("Sunburn");
      List<Campaign> controlList = new List<Campaign>{campaign1, campaign3};

      Assert.Equal(controlList, testList);
    }



    // [Fact]
    // public void CampaignGetGivers_RetrieveCampaignDonators_ReturnListOfUsers()
    // {
    //   ContactInformation info = new ContactInformation("950 W.Burnside, Portland", "useremail@gmail.com", "(123)456-7890");
    //   User newUser = new User("Anna", "anna123", "123",  info);
    //   newUser.Save();
    //
    //   DateTime start = new DateTime(2017,1,1);
    //   DateTime end = new DateTime(2018,1,1);
    //
    //   Campaign testCampaign = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 50, 0, start, end, 1, 1);
    //   testCampaign.Save();
    //
    //   Donation testDonation = newUser.MakeDonation(testCampaign, 10, new DateTime (2017,3,3));
    //   Donation controlDonation = new Donation(newUser.Id, testCampaign.Id, 10, new DateTime(2017,3,3), testDonation.Id);
    //
    //   List<User> testUsers = testCampaign.GetGivers();
    //   Assert.Equal(newUser, testUsers[0]);
    // }


    public void Dispose()
    {
      User.DeleteAll();
      Campaign.DeleteAll();
    }
  }
}
