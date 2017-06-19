using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Charity.Objects;

namespace Charity
{
  [Collection("Charity")]
  public class UserTest : IDisposable
  {
    public UserTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=charity_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void User_Equals_UserEqualsUser()
    {
      ContactInformation info = new ContactInformation("950 W.Burnside, Portland", "useremail@gmail.com", "(123)456-7890");
      User testUser = new User("Anna", "anna123", "123",  info, 1);
      User controlUser = new User("Anna", "anna123", "123",  info, 1);

      Assert.Equal(controlUser, testUser);
    }

    [Fact]
    public void User_GetAll_DatabaseEmptyOnLoad()
    {
      List<User> testList = User.GetAll();
      List<User> controlList = new List<User>{};

      Assert.Equal(controlList, testList);
    }

    [Fact]
    public void User_SaveUser_SaveToDB()
    {
      ContactInformation info = new ContactInformation("950 W.Burnside, Portland", "useremail@gmail.com", "(123)456-7890");
      User controlUser = new User("Anna", "anna123", "123",  info);
      controlUser.Save();
      User testUser = User.GetAll()[0];

      Assert.Equal(controlUser, testUser);
    }

    [Fact]
    public void User_Find_FindsUserInDB()
    {
      ContactInformation info = new ContactInformation("950 W.Burnside, Portland", "useremail@gmail.com", "(123)456-7890");
      User controlUser = new User("Anna", "anna123", "123",  info);
      controlUser.Save();

      User testUser = User.Find(controlUser.Id);

      Assert.Equal(controlUser, testUser);
    }


    [Fact]
    public void User_ValidUser_ReturnsUser()
    {
        ContactInformation info = new ContactInformation("950 W.Burnside, Portland", "useremail@gmail.com", "(123)456-7890");
        User testUser = new User("Anna", "anna123", "123",  info);
        testUser.Save();

        Assert.Equal(testUser, User.ValiateUser("anna123", "123"));
    }

    [Fact]
    public void User_MakeDonation_ConnectsUserAndCampaign()
    {
        ContactInformation info = new ContactInformation("950 W.Burnside, Portland", "useremail@gmail.com", "(123)456-7890");
        User newUser = new User("Anna", "anna123", "123",  info);
        newUser.Save();

        DateTime start = new DateTime(2017,1,1);
        DateTime end = new DateTime(2018,1,1);

        Campaign newCampaign = new Campaign("Lina's Sunburn", "Help Lina's sunburn", 50, 0, start, end, 1);

        Donation testDonation = newUser.MakeDonation(newCampaign, 25, new DateTime (2017, 03, 3));
        Donation controlDonation = new Donation(newUser.Id, newCampaign.Id, 25, new DateTime (2017, 03, 3), testDonation.Id);

        Assert.Equal(controlDonation, testDonation);
    }


    public void Dispose()
    {
      Category.DeleteAll();
      User.DeleteAll();
      Donation.DeleteAll();
      Campaign.DeleteAll();
    }
  }
}
