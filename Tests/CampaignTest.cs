using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Charity.Objects;

namespace Charity
{
  [Collection("Charity")]
  public class CampaignTest
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
  }
}
