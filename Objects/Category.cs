using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Charity.Objects
{
  public class Category
  {
    public int Id {get; set;}
    public string Name {get; set;}

    public Category(string name, int id = 0)
    {
      Name = name;
      Id = id;
    }

    public override bool Equals(System.Object otherCategory)
    {
      if (!(otherCategory is Category))
      {
        return false;
      }
      else
      {
        Category newCategory = (Category)otherCategory;
        return (this.Id == newCategory.Id &&
                this.Name == newCategory.Name &&);
      }
    }

    public static List<Category> GetAll()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("SELECT * FROM categories;", DB.GetConnection());
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Category> categories = new List<Category>{};
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);

        Category newCategory = new Category(name, id);
        categories.Add(newCategory);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();

      return categories;
    }
  }
}
