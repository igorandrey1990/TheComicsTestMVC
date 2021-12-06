using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using TheComicsTestMVC.Helpers;
using TheComicsTestMVC.Models;

namespace TheComicsTestMVC.Controllers
{
    public class MarvelController : Controller
    {
        public IActionResult Index([FromServices] IConfiguration config)
        {
            MarvelAPIHelper apiHelper = new MarvelAPIHelper();
            Story objStory = apiHelper.LoadStory(config, 18158);
            return View(objStory);
        }
    }
}
