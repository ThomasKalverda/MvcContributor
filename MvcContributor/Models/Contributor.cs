using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using MvcContributor.Class;


namespace MvcContributor.Models
{
    public class Contributor
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Birth Place")]
        [StringLength(60, MinimumLength = 3)]
        public string BirthPlace { get; set; }

        [StringLength(250, MinimumLength = 3)]
        public string Biography { get; set; }

        [Display(Name = "Birth Name")]
        [StringLength(60, MinimumLength = 3)]
        public string BirthName { get; set; }

        public string ImagePath { get; set; }

                
    }

    public class TmdbPerson
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Birth Name")]
        [StringLength(60, MinimumLength = 3)]
        public string BirthName { get; set; }
        public int TmdbID { get; set; }
        
        public string Biography { get; set; }
        public string ImdbID { get; set; }

        [Display(Name = "Birth Place")]
        [StringLength(60, MinimumLength = 3)]
        public string PlaceOfBirth { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public double Popularity { get; set; }
        public string ProfilePath { get; set; }
        public List<ProfileImage> ImageList { get; set; }
        public DateTime ImportDate { get; set; }
    }
    public class ContributorDBContext : DbContext
    {
        public DbSet<Contributor> Contributors { get; set; }
        public DbSet<TmdbPerson> TmdbPersons { get; set; }
    }
}
