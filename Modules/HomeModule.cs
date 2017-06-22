using Nancy;
using System;
using System.Collections.Generic;
using Charity.Objects;

namespace Charity
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object> {};
        model.Add("currentUser", User.CurrentUser);
        model.Add("categories", Category.GetAll());
        model.Add("all-campaigns", Campaign.GetAll());
        return View["index.cshtml", model];
      };
      Post["/profile/new"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object> {};
        ContactInformation info = new ContactInformation(Request.Form["address"], Request.Form["phone-number"], Request.Form["email"]);
        User newUser = new User(Request.Form["name"], Request.Form["username"], Request.Form["password"], info);
        newUser.Save();
        User.CurrentUser = newUser;
        model.Add("currentUser", User.CurrentUser);
        model.Add("categories", Category.GetAll());
        model.Add("all-campaigns", Campaign.GetAll());
        return View["index.cshtml", model];
      };
      Get["/login"] = _ =>{
        User.CurrentUser = null;
        return View["login_form.cshtml"];
      };
      Post["/login"] = _ => {
        User.CurrentUser =  User.ValiateUser(Request.Form["username"], Request.Form["password"]);
        if (User.CurrentUser == null)
        {
          return View["login_form.cshtml"];
        }
        else
        {
          Dictionary<string, object> model = new Dictionary<string, object> {};
          model.Add("currentUser", User.CurrentUser);
          model.Add("categories", Category.GetAll());
          model.Add("all-campaigns", Campaign.GetAll());
          return View["index.cshtml", model];
        }
      };
      Get["/{id}/campaigns"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object> {};
        Category selectedCategory = Category.Find(parameters.id);
        model.Add("selectedCampaigns", selectedCategory.GetCampaigns());
        model.Add("currentUser", User.CurrentUser);
        model.Add("categories", Category.GetAll());
        model.Add("campaigns", Campaign.GetAll());
        return View["index.cshtml", model];
      };
      Post["/campaign/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object> {};
        Campaign selectedCampaign = Campaign.Find(parameters.id);
        if (User.CurrentUser == null)
        {
          return View["login_form.cshtml"];
        }
        else
        {
        Donation newDonation = User.CurrentUser.MakeDonation(selectedCampaign, Request.Form["money"], DateTime.Now);
        List<Dictionary<string, object>> allDonationInfo = selectedCampaign.GetDonationsByCampaign();
        selectedCampaign.UpdateBalance(Request.Form["money"]);
        model.Add("currentUser", User.CurrentUser);
        model.Add("donations", allDonationInfo);
        model.Add("selectedCampaign", selectedCampaign);
        return View["campaign.cshtml", model];
        }
      };
      Get["/campaign/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object> {};
        Campaign selectedCampaign = Campaign.Find(parameters.id);
        List<Dictionary<string, object>> allDonationInfo = selectedCampaign.GetDonationsByCampaign();
        model.Add("currentUser", User.CurrentUser);
        model.Add("donations", allDonationInfo);
        model.Add("selectedCampaign", selectedCampaign);
        return View["campaign.cshtml", model];
      };
      Get["/campaign/new"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("allCategories", Category.GetAll());
        model.Add("currentUser", User.CurrentUser);
        return View["create_campaign.cshtml", model];
      };
      Post["/campaign/new"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object> {};
        Console.WriteLine(User.CurrentUser.Name);
        Campaign newCampaign = new Campaign(Request.Form["name"], Request.Form["description"], Request.Form["goal"], 0, Request.Form["start-date"],
        Request.Form["end-date"], Request.Form["category"], User.CurrentUser.Id);
        newCampaign.Save();
        List<Dictionary<string, object>> allDonationInfo = newCampaign.GetDonationsByCampaign();
        model.Add("currentUser", User.CurrentUser);
        model.Add("donations", allDonationInfo);
        model.Add("selectedCampaign", newCampaign);
        return View["campaign.cshtml", model];
      };
      Get["/profile/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("donations", User.CurrentUser.GetDonationsByUser());
        model.Add("campaigns", User.CurrentUser.GetCampaigns());
        model.Add("currentUser", User.CurrentUser);
        return View["user_profile.cshtml", model];
      };
      Patch["/profile/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        User mainUser = User.CurrentUser;
        mainUser.Update(Request.Form["name"], Request.Form["username"], Request.Form["password"], Request.Form["address"], Request.Form["phone-number"], Request.Form["email"]);
        model.Add("donations", mainUser.GetDonationsByUser());
        model.Add("campaigns", mainUser.GetCampaigns());
        model.Add("currentUser", mainUser);
        return View["user_profile.cshtml", model];
      };
      Get["/campaign/{id}/update"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("selectedCampaign", Campaign.Find(parameters.id));
        model.Add("allCategories", Category.GetAll());
        model.Add("currentUser", User.CurrentUser);
        model.Add("update", null);
        return View["create_campaign.cshtml", model];
      };
      Patch["/campaign/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Campaign selectedCampaign = Campaign.Find(parameters.id);
        selectedCampaign.Update(Request.Form["name"], Request.Form["description"], Request.Form["goal"], Request.Form["end-date"], Request.Form["category"]);
        List<Dictionary<string, object>> allDonationInfo = selectedCampaign.GetDonationsByCampaign();
        model.Add("donations", allDonationInfo);
        model.Add("currentUser", User.CurrentUser);
        model.Add("selectedCampaign", selectedCampaign);
        return View["campaign.cshtml", model];
      };
      Delete["campaign/{id}/delete"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Campaign selectedCampaign = Campaign.Find(parameters.id);
        selectedCampaign.DeleteSingleCampaign();
        model.Add("donations", User.CurrentUser.GetDonationsByUser());
        model.Add("campaigns", User.CurrentUser.GetCampaigns());
        model.Add("currentUser", User.CurrentUser);
        return View["user_profile.cshtml", model];
      };
      Get["categories/{id}/trending"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        List<Category> allCategories = Category.GetAll();
        Category selectedCategory = Category.Find(parameters.id);
        Console.WriteLine(selectedCategory.Name);
        if (selectedCategory.GetTrendingCampaigns() != null)
        {
          model.Add("campaigns", selectedCategory.GetTrendingCampaigns());
        }
        else
        {
          model.Add("campaigns", selectedCategory.GetCampaigns());
        }
        model.Add("search-results", null);
        model.Add("currentUser", User.CurrentUser);
        model.Add("categories", allCategories);
        model.Add("selectedCategory", selectedCategory);
        return View["campaigns.cshtml", model];
      };

      Get["campaigns/search"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        List<Category> allCategories = Category.GetAll();
        model.Add("currentUser", User.CurrentUser);
        model.Add("campaigns", null);
        model.Add("search-results", Campaign.SearchByName(Request.Query["name"]));
        model.Add("categories", allCategories);
        return View["campaigns.cshtml", model];
      };
      Get["campaigns/all"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        List<Campaign> allCampaigns = Campaign.GetAll();
        model.Add("currentUser", User.CurrentUser);
        model.Add("campaigns", null);
        model.Add("search-results", null);
        model.Add("categories", Category.GetAll());
        model.Add("allCampaigns", allCampaigns);
        return View["campaigns.cshtml", model];
      };
    }
  }
}
