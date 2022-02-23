using System;

namespace FileTracker.Common.Models
{
    public class Report
    {
        public string AddedContent { get; set; }

        public string FileName { get; set; }

        public DateTime Timestamp { get; set; }
    }
}