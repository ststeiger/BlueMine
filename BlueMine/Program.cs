﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlueMine.Controllers;
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
        } // End Constructor 


    } // End Country 


    public class IpRange
    {
        public uint Lower;
        public uint Upper;


        public IpRange(uint lowerBoundary, uint upperBoundary)
        {
            this.Lower = lowerBoundary;
            this.Upper = upperBoundary;
        } // End Constructor 

    } // End Constructor 


    public class Program
    {


        public static Guid ToGuid(ulong a, ulong b)
        {
            byte[] bytes = new byte[16];
            byte[] aa = System.BitConverter.GetBytes(a);
            byte[] bb = System.BitConverter.GetBytes(b);

            System.Array.Copy(aa, 0, bytes, 0, 8);
            System.Array.Copy(bb, 0, bytes, 8, 8);

            return new System.Guid(bytes);
        }

        public static Guid ToGuid(ulong a)
        {
            return ToGuid(0, a);
        }
        

        public static void Main(string[] args)
        {
            // string aaa = ToGuid(1).ToString();
            string aaa = ToGuid(1).ToString();
            
            System.Console.WriteLine(aaa);

            System.Guid g = new iCaramba.UInt128(111999).ToGuid();
            System.Console.WriteLine(g);
            
            
            // CoreyM.Collections.AATree<IpRange, Country> mytree = new CoreyM.Collections.AATree<IpRange, Country>();


            //CoreyM.Collections.AATree<Country, IpRange> mytree = new CoreyM.Collections.AATree<Country, IpRange>();

            //// mytree.Add(new IpRange(0, 5), new Country("Switzerland"));
            //// mytree.Add(new IpRange(6, 10), new Country("France"));
            //// mytree.Add(new IpRange(11, 15), new Country("Germany"));
            //// mytree.Add(new IpRange(16, 20), new Country("Italy"));
            //// mytree.Add(new IpRange(21, 255), new Country("USA"));



            //mytree.Add(new Country("Switzerland"), new IpRange(0, 5));
            //mytree.Add(new Country("France"), new IpRange(6, 10));
            //mytree.Add(new Country("Germany"), new IpRange(11, 15));
            //mytree.Add(new Country("Italy"), new IpRange(16, 20));
            //mytree.Add(new Country("USA"), new IpRange(21, 255));


            // var res = mytree[new IpRange(21, 255)];

            // var res = mytree[123];
            // 255 => 0 => 0 => 1
            // 255 * 255 * 255 * 255 => range // 15+ gb

            // 0-255 ==> all that have this
            // all that have start=>cidr
            // cidr ranges


            // 227 => 0 => 0 => 1

            // OfficeOpenXml.OpenDocumentSpreadsheet.OdsReaderWriter.Test();

            BuildWebHost(args).Run();
        } // End Sub Main 


        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        } // End Sub Main(string[] args) 


    } // End Class Program 


} // End Namespace BlueMine 
