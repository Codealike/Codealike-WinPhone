using System;
using System.Collections.Generic;

namespace Codealike.WP8.Models
{
    public class MonthHeader
    {
        public string Header { get; set; }

        public bool IsHeaderVisible { get; set; }

        public List<string> Days { get; set; }
        public DateTime Date { get; set; }

        public int Count { get; set; }
    }
}
