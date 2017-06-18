using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Charity.Objects;

namespace Charity
{
  [Collection("Charity")]
  public class CharityTest : IDisposable
  {
    public CharityTest()
    {
      // Console.WriteLine("Hello");
      DBConfiguration.ConnectionString = "Data Source=DESKTOP-6CVACGR\\SQLEXPRESS;Initial Catalog=charity_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void User_Equals_UserEqualsUser()
    {
      ContactInformation info = new ContactInformation("950 W.Burnside, Portland", "useremail@gmail.com", "(123)456-7890");
      User testUser = new User(2, "Anna", "anna123", "123",  info, 1);
      User controlUser = new User(2, "Anna", "anna123", "123",  info, 1);

      Assert.Equal(controlUser, testUser);
    }

    public void Dispose()
    {
    }
  }
}
