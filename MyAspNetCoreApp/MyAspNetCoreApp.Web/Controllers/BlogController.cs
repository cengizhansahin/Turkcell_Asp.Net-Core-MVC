﻿using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class BlogController : Controller
    {
        //blog/article/makale-ismi/id
        public IActionResult Article(string name, int id)
        {

            //var routes = Request.RouteValues["article"];
            return View();
        }
    }
}
