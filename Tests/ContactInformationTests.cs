using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Charity.Objects;

namespace Charity
{
  [Collection("Charity")]
  public class ContactInformationTest : IDisposable
  {
    public ContactInformationTest()
    {
      // Console.WriteLine("Hello");
      DBConfiguration.ConnectionString = "Data Source=DESKTOP-6CVACGR\\SQLEXPRESS;Initial Catalog=charity_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void User_Equals_UserEqualsUser()
    {
      ContactInformation info1 = new ContactInformation("950 W.Burnside, Portland", "useremail@gmail.com", "(123)456-7890");
      ContactInformation info2 = new ContactInformation("950 W.Burnside, Portland", "useremail@gmail.com", "(123)456-7890");

      Assert.Equal(info1, info2);
    }

    public void Dispose()
    {
      // User.DeleteAll();
    }
  }
}
