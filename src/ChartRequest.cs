using System;
using System.Collections.Generic;

namespace TimHanewich.Google.Charts
{
    public class ChartRequest : IChartRequestComponent
    {
        //The static API request URL
        public static string RequestEndpoint = "https://chart.googleapis.com/chart?";

        //Vars
        public string Title {get; set;}
        public int Width {get; set;}
        public int Height {get; set;}

        public ChartRequest()
        {
            Width = 200;
            Height = 200;
        }
    
        public KeyValuePair<string, string>[] GenerateFormContent()
        {
            //The components that we need to do are...
            //chs - The size of the chart
            //chtt - The chart title

            List<KeyValuePair<string, string>> Vals = new List<KeyValuePair<string, string>>();

            //Size (chs)
            Vals.Add(new KeyValuePair<string, string>("chs", Width.ToString() + "x" + Height.ToString()));

            //Title (Chtt)
            //Only add it if it isn't null and it isn't blank.
            if (Title != null)
            {
                if (Title != "")
                {
                    Vals.Add(new KeyValuePair<string, string>("chtt", Title));
                }
            }
            
            return Vals.ToArray();
        }
    }
}