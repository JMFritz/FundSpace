using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Charity.Objects;

namespace Charity
{
  [Collection("Charity")]
  public class DonationTest : IDisposable
  {
    public DonationTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=charity_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Donation_Equals_DonationEqualsDonation()
    {
      ContactInformation info = new ContactInformation("950 W.Burnside, Portland", "useremail@gmail.com", "(123)456-7890");
      User newUser = new User("Anna", "anna123", "123",  info, 1);
      newUser.Save();
      Donation testDonation = new Donation(newUser.Id, 1, 25, new DateTime(2017, 05, 21), 2);

      Donation controlDonation = new Donation (newUser.Id, 1, 25, new DateTime(2017, 05, 21), 2);
      Assert.Equal(controlDonation, testDonation);
    }

    // [Fact]
    // public void Donation_SaveDonation_SaveToDB()
    // {
    //   ContactInformation info = new ContactInformation("950 W.Burnside, Portland", "useremail@gmail.com", "(123)456-7890");
    //   User newUser = new User("Anna", "anna123", "123",  info);
    //   newUser.Save();
    //   Donation controlDonation = new Donation(newUser.Id, 1, 25, new DateTime(2017, 05, 21), 2);
    //   controlDonation.Save();
    //
    //   Donation testDonation  = Donation.GetAll()[0];
    //   Assert.Equal(controlDonation, testDonation);
    // }

    public void Dispose()
    {
      Category.DeleteAll();
      User.DeleteAll();
      Donation.DeleteAll();
      Campaign.DeleteAll();
    }
  }
}
