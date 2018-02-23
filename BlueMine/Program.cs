
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace BlueMine
{

    public class Country
    {
        public int Id;
        public string Culture;


        static int idCounter = 1;

        public Country(string culture)
        {
            this.Id = idCounter++;
            this.Culture = culture;
        }

    }


    public class IpRange
    {
        public uint Lower;
        public uint Upper;


        public IpRange(uint lowerBoundary, uint upperBoundary)
        {
            this.Lower = lowerBoundary;
            this.Upper = upperBoundary;
        }

    }


    public class Program
    {


        public static void Main(string[] args)
        {
            CoreyM.Collections.AATree<IpRange, Country> mytree = new CoreyM.Collections.AATree<IpRange, Country>();



            mytree.Add(new IpRange(0, 5), new Country("Switzerland"));
            mytree.Add(new IpRange(6, 10), new Country("France"));
            mytree.Add(new IpRange(11, 15), new Country("Germany"));
            mytree.Add(new IpRange(16, 20), new Country("Italy"));
            mytree.Add(new IpRange(21, 255), new Country("USA"));


            mytree.fin




            // SchemaGenerator.GenerateSchema();
            BuildWebHost(args).Run();
        }


        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }


    }


}
