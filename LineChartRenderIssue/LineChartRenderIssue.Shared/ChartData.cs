using System;
using System.Collections.Generic;
using System.Text;

namespace LineChartRenderIssue.Shared
{
    public class ChartData
    {
        public ChartDataType ChartDataType { get; set; }

        public string Name { get; set; }

        public int Score { get; set; }
    }

    public enum ChartDataType
    {
        Value,
        Phase
    }
}
