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
                this.Name == newCategory.Name);
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

    public void Save()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("INSERT INTO categories (name) OUTPUT INSERTED.id VALUES (@Name)", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@Name", this.Name));

      SqlDataReader rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();
    }

    public static Category Find(int id)
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("SELECT * FROM categories WHERE id = @CategoryId;", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@CategoryId", id));
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundCategoryId = 0;
      string foundCategoryName = null;

      while(rdr.Read())
      {
        foundCategoryId = rdr.GetInt32(0);
        foundCategoryName = rdr.GetString(1);
      }

      Category foundCategory = new Category(foundCategoryName, foundCategoryId);

      if (rdr != null)
      {
        rdr.Close();
      }

      DB.CloseConnection();
      return foundCategory;
    }

    public List<Campaign> GetCampaigns()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("SELECT * FROM campaigns WHERE category_id = @CategoryId;", DB.GetConnection());
      cmd.Parameters.Add(new SqlParameter("@CategoryId", this.Id));
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Campaign> campaigns = new List<Campaign>{};

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string description = rdr.GetString(2);
        int goal = rdr.GetInt32(3);
        int balance = rdr.GetInt32(4);
        DateTime start = rdr.GetDateTime(5);
        DateTime end = rdr.GetDateTime(6);
        int categoryId = rdr.GetInt32(7);

        Campaign newCampaign = new Campaign(name, description, goal, balance, start, end, categoryId, id);
        campaigns.Add(newCampaign);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();
      return campaigns;
    }

    public void Update(string name)
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("UPDATE categories SET name = @Name OUTPUT INSERTED.name WHERE id = @CategoryId;", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@Name", name));
      cmd.Parameters.Add(new SqlParameter("@CategoryId", this.Id));

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Name = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();
    }

    public void DeleteSingleCategory()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("DELETE FROM categories WHERE id = @CategoryId;", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@CategoryId", this.Id));
      cmd.ExecuteNonQuery();
      DB.CloseConnection();
    }

    public static void DeleteAll()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("DELETE FROM categories;", DB.GetConnection());

      cmd.ExecuteNonQuery();
      DB.CloseConnection();
    }
  }
}
