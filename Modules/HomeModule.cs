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
        return View["login_form.cshtml"];
      };
      Post["/profile/new"] = _ => {
        ContactInformation info = new ContactInformation(Request.Form["address"], Request.Form["phone-number"], Request.Form["email"]);
        User newUser = new User(Request.Form["name"], Request.Form["username"], Request.Form["password"], info);
        newUser.Save();
        User.CurrentUser = newUser;
        return View["login_form.cshtml"];
      };
      Get["/login"] = _ => {
        User.CurrentUser =  User.ValiateUser(Request.Query["username"], Request.Form["password"]);
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
      // Get["/home"] = _ => {
      //   Dictionary<string, object> model = new Dictionary<string, object> {};
      //   model.Add("currentUser", User.CurrentUser);
      //   model.Add("categories", Category.GetAll());
      //   model.Add("all-campaigns", Campaign.GetAll());
      //   return View["index.cshtml", model];
      // };
      Get["/{id}/campaigns"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object> {};
        Category selectedCategory = Category.Find(parameters.id);
        model.Add("selectedCampaigns", selectedCategory.GetCampaigns());
        model.Add("currentUser", User.CurrentUser);
        model.Add("categories", Category.GetAll());
        model.Add("campaigns", Campaign.GetAll());
        return View["index.cshtml", model];
      };
      Get["/campaign/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object> {};
        Campaign selectedCampaign = Campaign.Find(parameters.id);
        model.Add("selectedCampaign", selectedCampaign);
        model.Add("campaignDonations", selectedCampaign.GetDonations());
        return View["campaign.cshtml", model];
      };
      Post["/campaign/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object> {};
        Campaign selectedCampaign = Campaign.Find(parameters.id);
        Donation newDonation = User.CurrentUser.MakeDonation(selectedCampaign, Request.Form["amount"], Request.Form["date"]);

        model.Add("selectedCampaign", selectedCampaign);
        model.Add("campaignDonations", selectedCampaign.GetDonations());
        return View["campaign.cshtml", model];
      };
      Get["/campaign/{id}/"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object> {};
        Campaign selectedCampaign = Campaign.Find(parameters.id);
        List<Donation> allDonations = selectedCampaign.GetDonations();
        List<Dictionary<string, object>> allDonationInfo = new List<Dictionary<string, object>>{};
        foreach(Donation donation in allDonations)
        {
          allDonationInfo.Add(donation.GetDonationsByCampaign(selectedCampaign));
        }
        model.Add("donations", allDonationInfo);
        model.Add("selectedCampaign", selectedCampaign);
        return View["campaign.cshtml", model];
      };
      Get["/campaign/new"] = _ => {
        return View["form.cshtml"];
      };
      Post["/campaign/new"] = _ => {
        Campaign newCampaign = new Campaign(Request.Form["name"], Request.Form["description"], Request.Form["goal"], 0, Request.Form["start_date"],
        Request.Form["end_date"], Request.Form["category"], User.CurrentUser.Id);
        newCampaign.Save();
        return View[""];
      };
    }
  }
}
