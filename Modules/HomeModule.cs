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
      Post["/"] = _ => {
        ContactInformation info = new ContactInformation(Request.Form["address"], Request.Form["phone-number"], Request.Form["email"]);
        User newUser = new User(Request.Form["name"], Request.Form["username"], Request.Form["password"], info);
        newUser.Save();
        User.CurrentUser = newUser;
        return View["login_form.cshtml"];
      };
      Get["/home"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object> {};
        model.Add("currentUser", User.CurrentUser);
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
      Get["/campaign/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object> {};
        Campaign selectedCampaign = Campaign.Find(parameters.id);
        model.Add("selectedCampaign", selectedCampaign);
        model.Add("campaignDonations", selectedCampaign.GetDonations());
        return View["campaign.cshtml", model];
      };
      // Post["/campaign/{id}"] = parameters => {
      //   Dictionary<string, object> model = new Dictionary<string, object> {};
      //   Campaign selectedCampaign = Campaign.Find(parameters.id);
      //   Donation newDonation = User.CurrentUser.MakeDonation(selectedCampaign, Request.Form["amount"], Request.Form["date"]);
      //
      //   model.Add("selectedCampaign", selectedCampaign);
      //   model.Add("campaignDonations", selectedCampaign.GetDonations());
      //   return View["campaign.cshtml", model];
      // };
      // Get["/campaign/{id}/"] = parameters => {
      //   Dictionary<string, object> model = new Dictionary<string, object> {};
      //   Campaign selectedCampaign = Campaign.Find(parameters.id);
      //   model.Add("selectedCampaign", selectedCampaign);
      //   return View["campaign.cshtml", model];
      //
      // };
    }
  }
}
