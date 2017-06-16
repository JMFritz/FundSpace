using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Hospital.Objects;

namespace Charity
{
  [Collection("Charity")]
  public class CharityTest
  {
    public CharityTest()
    {
      DBConfiguration.ConnectionString = "Data Source=DESKTOP-6CVACGR\\SQLEXPRESS;Initial Catalog=charity_test;Integrated Security=SSPI;";
    }

    [Fact]
    publpic void User_Equals_UserEqualsUser()
    {
      // User newUser = new User();
    }
  }
}
