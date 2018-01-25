using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppPizza.Models;

namespace WebAppPizza.Controllers
{
    public class MentionController : Controller
    {
        public IActionResult Index()
        {

            var mention = new Mention
            {
                Title = "Mentions légales",
                Paragraphe1 = "Do you see any Teletubbies in here? Do you see a slender plastic tag clipped to my shirt with my name printed on it? Do you see a little Asian child with a blank expression on his face sitting outside on a mechanical helicopter that shakes when you put quarters in it? No? Well, that's what you see at a toy store. And you must think you're in a toy store, because you're here shopping for an infant named Jeb.",
                Paragraphe2 = "Now that there is the Tec-9, a crappy spray gun from South Miami. This gun is advertised as the most popular gun in American crime. Do you believe that shit? It actually says that in the little book that comes with it: the most popular gun in American crime. Like they're actually proud of that shit.",
                Paragraphe3 = "My money's in that office, right? If she start giving me some bullshit about it ain't there, and we got to go someplace else and get it, I'm gonna shoot you in the head then and there. Then I'm gonna shoot that bitch in the kneecaps, find out where my goddamn money is. She gonna tell me too. Hey, look at me when I'm talking to you, motherfucker. You listen: we go in there, and that nigga Winston or anybody else is in there, you the first motherfucker to get shot. You understand?"

            };

            /*
            dynamic a = new ExpandoObject();
            a.maProp = "";
            */

            /*Utilisent des objets dynamiques, des Expando*/
            ViewBag.MonTitle = "MothaFucka !";
            ViewData["MonTitle"] = mention.Title;

            return View(mention);
        }
    }
}