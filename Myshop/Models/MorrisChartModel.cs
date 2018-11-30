using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myshop.Models
{
    public class MorrisChartModel
    {
        public class LineChart
        {
            public string[] Labels { get; set; }
            public string Data { get; set; }
        }

        public class DonutChart
        {
            public string label { get; set; }
            public string labelColor { get; set; }
            public decimal value { get; set; }
        }
    }
}