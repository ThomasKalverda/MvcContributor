using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;


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

        [Display(Name = "Headshot")]
        public string ImageURL { get; set; }
                
    }
    public class ContributorDBContext : DbContext
    {
        public DbSet<Contributor> Contributors { get; set; }
    }
}
