using System;

namespace TimHanewich.Google.Charts
{
    public class ChartRequest
    {
        public string Title {get; set;}
        public int Width {get; set;}
        public int Height {get; set;}

        public ChartRequest()
        {
            Width = 200;
            Height = 200;
        }
    }
}