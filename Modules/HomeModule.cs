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
        model.Add("campaigns", Campaign.GetAll());
        return View["index.cshtml", model];
      };
    }
  }
}
