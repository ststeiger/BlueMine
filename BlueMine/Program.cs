
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
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
    
    
    public class GeoPoint
    {
        public decimal Latitude;
        public decimal Longitude;
        
        
        public GeoPoint()
        { } // End Constructor 
        
        
        public GeoPoint(decimal latitude, decimal longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        } // End Constructor 
        
    }
    
    
    public class GeoBoundingBox
    {
        public GeoPoint TopLeft;
        public GeoPoint BottomRight;


        public GeoBoundingBox()
        { } // End Constructor GeoBoundingBox
        
        
        public GeoBoundingBox(GeoPoint topLeft, GeoPoint bottomRight)
        {
            this.TopLeft = topLeft;
            this.BottomRight = bottomRight;
        } // End Constructor GeoBoundingBox
        
        
        public GeoBoundingBox(decimal lat1,decimal lon1,decimal lat2,decimal lon2)
        {
            this.TopLeft = new GeoPoint(lat1, lon1);
            this.BottomRight = new GeoPoint(lat2, lon2);
        } // End Constructor GeoBoundingBox
        
        
        // left is the longitude of the left (westernmost) side of the bounding box.
        public decimal Left
        {
            get { return this.TopLeft.Longitude; }
        } // End Property Left 


        // bottom is the latitude of the bottom (southernmost) side of the bounding box.
        public decimal Bottom
        {
            get { return this.BottomRight.Latitude; }
        } // End Property Bottom 
        
        
        // right is the longitude of the right (easternmost) side of the bounding box.
        public decimal Right
        {
            get { return this.BottomRight.Longitude; }
        } // End Property Right 
        
        // top is the latitude of the top (northernmost) side of the bounding box.
        public decimal Top
        {
            get { return this.TopLeft.Latitude; }
        } // End Property Top 
        
        
        public string Url
        {
            get
            {
                return $"https://www.openstreetmap.org/api/0.6/map?bbox={Left},{Bottom},{Right},{Top}";
            }
        } // End Property Url 
        
    }
    
    
    public class Program
    {
        
        
        // https://wiki.openstreetmap.org/wiki/API_v0.6#Retrieving_map_data_by_bounding_box:_GET_.2Fapi.2F0.6.2Fmap
        // https://stackoverflow.com/questions/1689096/calculating-bounding-box-a-certain-distance-away-from-a-lat-long-coordinate-in-j
        
                
        // lon1 = {decimal} 4.4453171172367054957394797540
        // lat1 = {decimal} 44.168727660802959365719190662
        // p1 = 44.168727660802959365719190662, 4.4453171172367054957394797540
        // lat1, lon1
        
        
        // lon2 = {decimal} 4.4465708827632945042605202460
        // lat2 = {decimal} 44.167828339197040634280809338
        // p2 = 44.167828339197040634280809338, 4.4465708827632945042605202460
        // lat2, lon2
        public static GeoBoundingBox GetBoundingBox(decimal lat, decimal lon, decimal distanceInMeters)
        {
            
            double ToRadians(decimal val)
            {
                return (double)( val/180.0M * (decimal) Math.PI);
            }

            decimal ToDegrees(decimal val)
            {
                return val / (decimal)Math.PI * 180.0M;
            }

            // https://en.wikipedia.org/wiki/Earth_radius
            // For Earth, the mean radius is 6,371.0088 km
            decimal R = 6371.0088M;  // earth radius in km
            decimal radius = distanceInMeters * 0.001M; // km
            
            decimal lon1 = lon - ToDegrees(radius/R/(decimal)System.Math.Cos(ToRadians(lat)));
            decimal lon2 = lon + ToDegrees(radius/R/(decimal)System.Math.Cos(ToRadians(lat)));
            
            decimal lat1 = lat + ToDegrees(radius/R);
            decimal lat2 = lat - ToDegrees(radius/R);
            System.Console.WriteLine($"{lat1},{lon1}");
            System.Console.WriteLine($"{lat2},{lon2}");

            return new GeoBoundingBox(lat1, lon1, lat2, lon2);
        }
        
        
        public static void Main(string[] args)
        {
            GeoBoundingBox bb = GetBoundingBox(44.168278M,4.445944M, 50);
            System.Console.WriteLine(bb.Url);
            OSM.API.v0_6.Polygon.Test();
            
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

            RestrictProgramToSingleInstance();
            
            BuildWebHost(args).Run();
        } // End Sub Main 


        public static void RestrictProgramToSingleInstance()
        {            
            // string cb = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            // string loc = System.Reflection.Assembly.GetExecutingAssembly().Location;
            // System.Console.WriteLine(loc);
            // System.Console.WriteLine(cb);

            // string fn = System.Reflection.Assembly.GetExecutingAssembly().FullName;
            string asmName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            
            // A Mutex is like a C# lock, but it can work across multiple processes. 
            // In other words, Mutex can be computer-wide as well as application-wide.
            
            // Be sure that your mutex name begins with "Global\".
            // On a server that is running Terminal Services, 
            // a named system mutex can have two levels of visibility. 
            // If its name begins with the prefix "Global\", " +
            // "the mutex is visible in all terminal server sessions. " +
            // "If its name begins with the prefix "Local\", " +
            // "the mutex is visible only in the terminal server session where it was created. 
            string mutexName = $@"Global\{asmName}";
            System.Threading.Mutex mutex = 
                new System.Threading.Mutex(true, mutexName, out bool createdNew);
            
            if (!createdNew)
            {
                System.Console.WriteLine(mutexName + " is already running! Exiting the application.");
                return;
            }
        }



        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        } // End Sub Main(string[] args) 
        
        
    } // End Class Program 
    
    
} // End Namespace BlueMine 
