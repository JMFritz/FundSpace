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
        model.Add("categories", Category.GetAll());
        model.Add("all-campaigns", Campaign.GetAll());
        return View["index.cshtml", model];
      };
      Post["/profile/new"] = _ => {
        ContactInformation info = new ContactInformation(Request.Form["address"], Request.Form["phone-number"], Request.Form["email"]);
        User newUser = new User(Request.Form["name"], Request.Form["username"], Request.Form["password"], info);
        newUser.Save();
        User.CurrentUser = newUser;
        return View["userProfile.cshtml"];
      };
      Get["/login"] = _ =>{
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
      Get["/home"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object> {};
        // model.Add("currentUser", User.CurrentUser);
        model.Add("categories", Category.GetAll());
        model.Add("all-campaigns", Campaign.GetAll());
        return View["index.cshtml", model];
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
        model.Add("donations", allDonationInfo);
        model.Add("selectedCampaign", selectedCampaign);
        return View["campaign.cshtml", model];
        }
      };
      Get["/campaign/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object> {};
        Campaign selectedCampaign = Campaign.Find(parameters.id);
        List<Dictionary<string, object>> allDonationInfo = selectedCampaign.GetDonationsByCampaign();
        model.Add("donations", allDonationInfo);
        model.Add("selectedCampaign", selectedCampaign);
        return View["campaign.cshtml", model];
      };
      Get["/campaign/new"] = _ => {
        // List<Category> allCategories = Category
        // model.Add("categories", Category.GetAll())
        return View["createCampaign.cshtml", Category.GetAll()];
      };
      Post["/campaign/new"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object> {};
        Console.WriteLine(User.CurrentUser.Name);
        Campaign newCampaign = new Campaign(Request.Form["name"], Request.Form["description"], Request.Form["goal"], 0, Request.Form["start-date"],
        Request.Form["end-date"], Request.Form["category"], User.CurrentUser.Id);
        newCampaign.Save();
        List<Dictionary<string, object>> allDonationInfo = newCampaign.GetDonationsByCampaign();
        model.Add("donations", allDonationInfo);
        model.Add("selectedCampaign", newCampaign);
        return View["campaign.cshtml", model];
      };
    }
  }
}
