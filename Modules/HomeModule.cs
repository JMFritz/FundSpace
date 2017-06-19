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
        return View["login_form.cshtml"];
      };
      Get["/home"] = _ => {
        return View["index.cshtml"];
      };
    }
  }
}
