using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using MvcContributor.Class;
using MvcContributor.Models;
using System.Data.Entity;

namespace MvcContributor.Controllers
{
    

    public class ApiController : Controller
    {
        private ContributorDBContext db = new ContributorDBContext();
        

        // GET: Api
        public ActionResult Index()
        {
            var tmdbPersons = from c in db.TmdbPersons
                              select c;
            return View(tmdbPersons);
        }
        

        [HttpPost]
        public ActionResult Index(string searchString)
        {
            if (ModelState.IsValid)
            {
                CallAPI(searchString);
            }
            var tmdbPersons = from c in db.TmdbPersons
                              select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                tmdbPersons = tmdbPersons.Where(s => s.Name.Contains(searchString));
            }
            TempData["lastSearch"] = searchString;
            return View("Results",tmdbPersons.ToList());
        }

        public void CallAPI(string searchString)
        {
            /*Calling API https://developers.themoviedb.org/3/search/search-people */
            string apiKey = "af6477dbc56ecee64bd59fcba62e4d7e";
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/search/person?api_key=" + apiKey + "&language=en-US&query=" + searchString + "&page=" + 1 + "&include_adult=false") as HttpWebRequest;

            string apiResponse = "";
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }
            /*End*/

            /*http://json2csharp.com*/
            ResponseSearchPeople rootObject = JsonConvert.DeserializeObject<ResponseSearchPeople>(apiResponse);
            
           foreach (Result result in rootObject.results)
            {
                TmdbPerson tmdbPerson = new TmdbPerson();
                tmdbPerson.Name = result.name;
                tmdbPerson.TmdbID = result.id;
                tmdbPerson.ProfilePath = result.profile_path;
                tmdbPerson.Popularity = result.popularity;
                tmdbPerson.BirthDate = DateTime.Today;
                var SearchData = db.TmdbPersons.Where(x => x.TmdbID == tmdbPerson.TmdbID).FirstOrDefault();
                if (ModelState.IsValid && SearchData == null)
                {
                    db.TmdbPersons.Add(tmdbPerson);
                    db.SaveChanges();
                }

            }
            
        }

        public void GetPerson(TmdbPerson tmdbPerson)
        {
            /*Calling API https://developers.themoviedb.org/3/people */
            string apiKey = "af6477dbc56ecee64bd59fcba62e4d7e";
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/person/" + tmdbPerson.TmdbID + "?api_key=" + apiKey + "&language=en-US") as HttpWebRequest;

            string apiResponse = "";
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }
            /*End*/

            /*http://json2csharp.com*/
            ResponsePerson rootObject = JsonConvert.DeserializeObject<ResponsePerson>(apiResponse);
      
            tmdbPerson.Name = rootObject.name;
            tmdbPerson.Biography = rootObject.biography;
            tmdbPerson.BirthDate = DateTime.Parse(rootObject.birthday);
            tmdbPerson.PlaceOfBirth = rootObject.place_of_birth;
            if (ModelState.IsValid)
            {
                db.Entry(tmdbPerson).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void GetImages(TmdbPerson tmdbPerson)
        {
            /*Calling API "https://api.themoviedb.org/3/person/ */
            string apiKey = "af6477dbc56ecee64bd59fcba62e4d7e";
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/person/" + tmdbPerson.TmdbID + "/images?api_key=" + apiKey + "&language=en-US") as HttpWebRequest;

            string apiResponse = "";
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }
            /*End*/

            /*http://json2csharp.com*/
            ResponseImages rootObject = JsonConvert.DeserializeObject<ResponseImages>(apiResponse);
            List<ProfileImage> ProfileList = new List<ProfileImage>();

            foreach (Profile profile in rootObject.profiles)
            {
                ProfileImage profileImage = new ProfileImage();
                profileImage.Width = profile.width;
                profileImage.Height = profile.height;
                profileImage.VoteAverage = profile.vote_average;
                profileImage.FilePath = profile.file_path;
                profileImage.AspectRatio = profile.aspect_ratio;
                if (profileImage.Width > 747 && profileImage.Height > 802)
                {
                    ProfileList.Add(profileImage);
                }
                
            }
            if (tmdbPerson.ImageList != null)
            {
                foreach (ProfileImage image in tmdbPerson.ImageList)
                {
                    tmdbPerson.ImageList.Remove(image);
                }
            }
            
            tmdbPerson.ImageList = ProfileList;
            
            if (ModelState.IsValid)
            {
                db.Entry(tmdbPerson).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        // GET: Api/Edit/5
        public ActionResult PersonDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TmdbPerson tmdbPerson = db.TmdbPersons.Find(id);
            if (tmdbPerson == null)
            {
                return HttpNotFound();
            }
            //Update tmdbPerson through API TMDB
            GetPerson(tmdbPerson);
            //Get Images from tmdbPerson
            GetImages(tmdbPerson);
            

            return View(tmdbPerson);
        }

        // POST: Api/Edit/5
        [HttpPost]
        public ActionResult PersonDetails(TmdbPerson tmdbPerson)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tmdbPerson).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    return View(tmdbPerson);
                }
                Contributor contributor = new Contributor
                {
                    Name = tmdbPerson.Name,
                    BirthName = tmdbPerson.BirthName,
                    BirthDate = tmdbPerson.BirthDate,
                    BirthPlace = tmdbPerson.PlaceOfBirth,
                    Biography = tmdbPerson.Biography
                };

                SaveImage(tmdbPerson);
                contributor.ImagePath = tmdbPerson.ProfilePath;
                TempData["newContributor"] = contributor;
                return RedirectToAction("CreateFromTMDB", "Contributors");
                    
            }
            catch
            {
                return View(tmdbPerson);
            }
        }
        public void SaveImage(TmdbPerson tmdbPerson)
        {
            string filename = Guid.NewGuid().ToString() + ".jpg";
            string path = Path.Combine(Server.MapPath("~/Content/Images"), filename);
            string image = "https://image.tmdb.org/t/p/w500" + tmdbPerson.ProfilePath;
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile(image, path);
            }
            tmdbPerson.ProfilePath = "~/Content/Images/" + filename;
        }
        
    }
}
