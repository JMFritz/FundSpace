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
    public void Campaign_Update_UpdateCampaignInfo()
    {
      DateTime start = new DateTime(2018,1,1);
      DateTime end = new DateTime(2019,1,1);

      Campaign campaign = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 50, 0, start, end, 1);
      campaign.Save();

      campaign.Update("Jun needs new laptop", "Help to buy new laptop", 50, 0, start, end, 1);

      Campaign controlCampaign = new Campaign("Jun needs new laptop", "Help to buy new laptop", 50, 0, start, end, 1, campaign.Id);

      Assert.Equal(controlCampaign, campaign);
    }

    public void Dispose()
    {
      User.DeleteAll();
      Campaign.DeleteAll();
    }
  }
}
