using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcContributor.Class
{
    public class TMDBSearchPerson
    {
    }
    public class KnownFor
    {
        public string poster_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
        public string original_title { get; set; }
        public List<object> genre_ids { get; set; }
        public int id { get; set; }
        public string media_type { get; set; }
        public string original_language { get; set; }
        public string title { get; set; }
        public string backdrop_path { get; set; }
        public double popularity { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public string first_air_date { get; set; }
        public List<string> origin_country { get; set; }
        public string name { get; set; }
        public string original_name { get; set; }
    }

    public class Result
    {
        public string profile_path { get; set; }
        public bool adult { get; set; }
        public int id { get; set; }
        public List<KnownFor> known_for { get; set; }
        public string name { get; set; }
        public double popularity { get; set; }
    }

    public class ResponseSearchPeople
    {
        public int page { get; set; }
        public List<Result> results { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
    }
    public class ResponsePerson
    {
        public bool adult { get; set; }
        public List<string> also_known_as { get; set; }
        public string biography { get; set; }
        public string birthday { get; set; }
        public string deathday { get; set; }
        public int gender { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string name { get; set; }
        public string place_of_birth { get; set; }
        public double popularity { get; set; }
        public string profile_path { get; set; }
    }
    public class ResponseImages
    {
        public List<Profile> profiles { get; set; }
        public int id { get; set; }
    }
    public class Profile
    {
        public string iso_639_1 { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int vote_count { get; set; }
        public double vote_average { get; set; }
        public string file_path { get; set; }
        public double aspect_ratio { get; set; }


    }
    public class ProfileImage
    {
        public int ID { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double VoteAverage { get; set; }
        public string FilePath { get; set; }
        public double AspectRatio { get; set; }


    }
}