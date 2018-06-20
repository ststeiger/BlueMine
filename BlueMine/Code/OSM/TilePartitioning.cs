namespace OSM.API.v0_6
{
    
    
    public class TilePartitioning
    {
        
        
        class BoundingBox 
        {
            public double North;
            public double South;
            public double East;
            public double West;   
        }
        
        
        BoundingBox tile2boundingBox( int x, int y, int zoom) 
        {
            BoundingBox bb = new BoundingBox();
            double zoompow = System.Math.Pow(2.0, zoom);
            
            bb.North = tile2lat(y, zoompow);
            bb.South = tile2lat(y + 1, zoompow);
            bb.West = tile2lon(x, zoompow);
            bb.East = tile2lon(x + 1, zoompow);
            return bb;
        }
        
        static double tile2lon(int x, double zoompow) 
        { 
            return x / zoompow * 360.0 - 180;
        }
        
        static double tile2lat(int y, double zoompow) 
        {
            double n = System.Math.PI - (2.0 * System.Math.PI * y) / zoompow;
            return (System.Math.Atan(System.Math.Sinh(n))) * 180.0/System.Math.PI;
        }
        
        
        public static decimal DegreesToRadians(decimal angle)
        {
            const decimal pi = 3.14159265358979M;
            return (pi / 180.0M) * angle;
        }
        
        private static decimal RadianToDegree(decimal angle)
        {
            const decimal pi = 3.14159265358979M;
            return angle * (180.0M / pi);
        }
        
        
        // https://wiki.openstreetmap.org/wiki/Slippy_map_tilenames#Lon..2Flat._to_tile_numbers_2
        public static (int, int) DegreesToTile(decimal lat_deg, decimal lon_deg, int zoom)
        {
            decimal lat_rad = DegreesToRadians(lat_deg);
            int n = (int) System.Math.Pow( 2.0 , zoom);
            int xtile = (int)((lon_deg + 180.0M) / 360.0M * n);
            int ytile = (int)((1.0 - System.Math.Log(System.Math.Tan((double)lat_rad) + (1 / System.Math.Cos((double)lat_rad))) / System.Math.PI) / 2.0 * n);
            return (xtile, ytile);
        }
        
        
        public static (decimal, decimal) TileToDegrees(int xtile, int ytile, int zoom)
        {
            const decimal pi = 3.14159265358979M;
            
            int n = (int) System.Math.Pow( 2.0 , zoom);
            decimal lon_deg = xtile / n * 360.0M - 180.0M;
            decimal lat_rad = (decimal) System.Math.Atan((System.Math.Sinh(System.Math.PI * (1 - 2 * ytile / n))));
            decimal lat_deg = RadianToDegree(lat_rad);
            return (lat_deg, lon_deg);
        }
        
        
    }
}