namespace MvcContributor.Migrations
{
    using MvcContributor.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcContributor.Models.ContributorDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "MvcContributor.Models.ContributorDBContext";
        }

        protected override void Seed(MvcContributor.Models.ContributorDBContext context)
        {
            context.Contributors.AddOrUpdate(i => i.Name,
            new Contributor
            {
                Name = "Brad Pitt",
                BirthDate = DateTime.Parse("1963-12-18"),
                BirthPlace = "Shawnee, Oklahoma, USA",
                Biography = "William Bradley 'Brad' Pitt (born December 18, 1963) is an American actor and film producer. ",
                BirthName = "William Bradley Pitt",
                ImagePath = "~/Content/Images/preview.jpg"
            },

            new Contributor
            {
                Name = "Angelina Jolie",
                BirthDate = DateTime.Parse("1975-06-04"),
                BirthPlace = "Los Angeles, California, USA",
                Biography = "Angelina Jolie is an American actress. She has received an Academy Award, two Screen Actors Guild Awards, and three Golden Globe Awards.",
                BirthName = "Angelina Jolie",
                ImagePath = "~/Content/Images/preview.jpg"
            },

            new Contributor
            {
                Name = "Margot Robbie",
                BirthDate = DateTime.Parse("1990-07-02"),
                BirthPlace = "Gold Coast, Queensland, Australia",
                Biography = "Margot Elise Robbie (born 2 July 1990) is an Australian actress. ",
                BirthName = "Margot Elise Robbie",
                ImagePath = "~/Content/Images/preview.jpg"
            }


            );
        }

    }
}
