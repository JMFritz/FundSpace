using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Charity.Objects;

namespace Charity
{
  [Collection("Charity")]
  public class CategoryTest : IDisposable
  {
    public CategoryTest()
    {
      DBConfiguration.ConnectionString = "Data Source=DESKTOP-6CVACGR\\SQLEXPRESS;Initial Catalog=charity_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void CategoryEquals_CompareCategorys_ReturnCategorysAreEqual()
    {
      Category testCategory = new Category("Medical");
      Category testCategory2 = new Category("Medical");

      Assert.Equal(testCategory, testCategory2);
    }

    [Fact]
    public void CategoryGetAll_RetrieveAllCategorys_ReturnEmptyList()
    {
      List<Category> categoryList = Category.GetAll();
      List<Category> categoryList2 = new List<Category>{};

      Assert.Equal(categoryList, categoryList2);
    }

    [Fact]
    public void CategorySave_SavesToDatabase_ReturnCategory()
    {
      Category testCategory = new Category("Medical");
      testCategory.Save();
      Category testCategory2 = Category.GetAll()[0];

      Assert.Equal(testCategory, testCategory2);
    }

    [Fact]
    public void Category_Update_UpdateCategory()
    {
      Category testCategory = new Category("Medical");
      testCategory.Save();

      testCategory.Update("Animals");
      Category controlCategory = new Category("Animals", testCategory.Id);

      Assert.Equal(controlCategory, testCategory);
    }


    [Fact]
    public void CategoryFind_FindSingleCategory_ReturnFoundCategory()
    {
      Category testCategory = new Category("Medical");
      testCategory.Save();

      Category foundCategory = Category.Find(testCategory.Id);
      Assert.Equal(testCategory, foundCategory);
    }
    public void Dispose()
    {
      Category.DeleteAll();
    }
  }
}
