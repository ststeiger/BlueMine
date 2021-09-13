
namespace BlueMine
{


    public class Zodiacs
    {


        public static System.DateTime GetIslamicNewYear()
        {
            System.DateTime utcNow = System.DateTime.UtcNow;
            return GetIslamicNewYear(utcNow);
        }


        public static System.DateTime GetIslamicNewYear(int gregorianYear)
        {
            System.DateTime utcYear = new System.DateTime(gregorianYear, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            return GetIslamicNewYear(utcYear);
        }


        // https://en.wikipedia.org/wiki/Islamic_calendar
        // Δ578 for 2008-2041
        private static int CorrelatingDelta(int utcYear)
        {
            int[] civil = new int[] { 1650, 1682, 1715, 1748, 1780, 1813, 1845, 1878, 1911, 1943, 1976, 2008, 2041, 2073, 2106, 2139 };
            int[] diff = new int[] { 590, 589, 588, 587, 586, 585, 584, 583, 582, 581, 580, 579, 578, 577, 576, 575 };

            if(utcYear < civil[0] || utcYear >= civil[civil.Length-1])
                throw new System.ArgumentOutOfRangeException("Don't have the delta.");

            for (int i = 0; i < civil.Length-1; ++i)
            {
                if (utcYear >= civil[i] && utcYear < civil[i + 1])
                    return diff[i+1];
            }

            throw new System.InvalidProgramException("This should never happen.");
        }


        public static System.DateTime GetIslamicNewYear(System.DateTime utcYear)
        {
            System.Globalization.HijriCalendar arab = new System.Globalization.HijriCalendar();
            System.Globalization.GregorianCalendar gregorian = new System.Globalization.GregorianCalendar();
            
            System.DateTime islamicNewYear = arab.ToDateTime(utcYear.Year - CorrelatingDelta(utcYear.Year), 1, 1, 0, 0, 0, 0);

            int year = gregorian.GetYear(islamicNewYear);
            int month = gregorian.GetMonth(islamicNewYear);
            int day = gregorian.GetDayOfMonth(islamicNewYear);

            return new System.DateTime(year, month, day);
        }


        public static System.DateTime GetChineseNewYear()
        {
            System.DateTime utcNow = System.DateTime.UtcNow;
            return GetChineseNewYear(utcNow);
        }


        public static System.DateTime GetChineseNewYear(int gregorianYear)
        {
            System.DateTime utcYear = new System.DateTime(gregorianYear, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            return GetChineseNewYear(utcYear);
        }


        public static System.DateTime GetChineseNewYear(System.DateTime utcYear)
        {
            System.Globalization.ChineseLunisolarCalendar chinese = new System.Globalization.ChineseLunisolarCalendar();
            System.Globalization.GregorianCalendar gregorian = new System.Globalization.GregorianCalendar();

            // Get Chinese New Year of current UTC date/time
            System.DateTime chineseNewYear = chinese.ToDateTime(utcYear.Year, 1, 1, 0, 0, 0, 0);

            // Convert back to Gregorian (you could just query properties of `chineseNewYear` directly, 
            // but I prefer to use `GregorianCalendar` for consistency:
            int year = gregorian.GetYear(chineseNewYear);
            int month = gregorian.GetMonth(chineseNewYear);
            int day = gregorian.GetDayOfMonth(chineseNewYear);

            return new System.DateTime(year, month, day);
        }



        // https://www.yourchineseastrology.com/zodiac/how-to-calculate-animal-sign.htm
        public static string GetChineseZodiac(System.DateTime date)
        {
            System.Globalization.EastAsianLunisolarCalendar cc =
                  new System.Globalization.ChineseLunisolarCalendar();
            int sexagenaryYear = cc.GetSexagenaryYear(date);
            int terrestrialBranch = cc.GetTerrestrialBranch(sexagenaryYear);

            // string[] years = "rat,ox,tiger,hare,dragon,snake,horse,sheep,monkey,fowl,dog,pig".Split(',');
            // string[] years = "Rat,Ox,Tiger,Rabbit,Dragon,Snake,Horse,Goat,Monkey,Rooster,Dog,Pig".Split(',');
            // string[] years = new string[]{ "rat", "ox", "tiger", "hare", "dragon", "snake", "horse", "sheep", "monkey", "fowl", "dog", "pig" };    
            string[] years = new string[] { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };

            return years[terrestrialBranch - 1];
        } // End Function get_ChineseZodiac


        // http://www.w3examples.com/jquery_dates/get_zodiac_sign.php
        public static string GetZodiac(System.DateTime date)
        {
            string[] zod_signs = new string[] {
                "Capricorn", "Aquarius", "Pisces", "Aries",
                "Taurus", "Gemini", "Cancer", "Leo", "Virgo",
                "Libra", "Scorpio", "Sagittarius"
            };

            int day = date.Day;
            int month = date.Month;

            string zodiacSign = "";
            switch (month)
            {
                case 1: //January
                    {
                        if (day < 20)
                            zodiacSign = zod_signs[0];
                        else
                            zodiacSign = zod_signs[1];
                    }
                    break;
                case 2: //February
                    {
                        if (day < 19)
                            zodiacSign = zod_signs[1];
                        else
                            zodiacSign = zod_signs[2];
                    }
                    break;
                case 3: //March
                    {
                        if (day < 21)
                            zodiacSign = zod_signs[2];
                        else
                            zodiacSign = zod_signs[3];
                    }
                    break;
                case 4: //April
                    {
                        if (day < 20)
                            zodiacSign = zod_signs[3];
                        else
                            zodiacSign = zod_signs[4];
                    }
                    break;
                case 5: //May
                    {
                        if (day < 21)
                            zodiacSign = zod_signs[4];
                        else
                            zodiacSign = zod_signs[5];
                    }
                    break;
                case 6: //June
                    {
                        if (day < 21)
                            zodiacSign = zod_signs[5];
                        else
                            zodiacSign = zod_signs[6];
                    }
                    break;
                case 7: //July
                    {
                        if (day < 23)
                            zodiacSign = zod_signs[6];
                        else
                            zodiacSign = zod_signs[7];
                    }
                    break;
                case 8: //August
                    {
                        if (day < 23)
                            zodiacSign = zod_signs[7];
                        else
                            zodiacSign = zod_signs[8];
                    }
                    break;
                case 9: //September
                    {
                        if (day < 23)
                            zodiacSign = zod_signs[8];
                        else
                            zodiacSign = zod_signs[9];
                    }
                    break;
                case 10: //October
                    {
                        if (day < 23)
                            zodiacSign = zod_signs[9];
                        else
                            zodiacSign = zod_signs[10];
                    }
                    break;
                case 11: //November
                    {
                        if (day < 22)
                            zodiacSign = zod_signs[10];
                        else
                            zodiacSign = zod_signs[11];
                    }
                    break;
                case 12: //December
                    {
                        if (day < 22)
                            zodiacSign = zod_signs[11];
                        else
                            zodiacSign = zod_signs[0];
                    }
                    break;
            } // End Switch (month) 

            return zodiacSign;
        } // End Function 


    } // End Class Zodiacs 


} // End Namespace BlueMine.Code 
