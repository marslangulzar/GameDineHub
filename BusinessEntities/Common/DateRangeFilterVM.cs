using System;
using BusinessEntities.Enums;

namespace BusinessEntities.Common
{
    public class DateRangeFilterVM
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string DateRangeFilterText { get; set; }
        public DateRangeFilter? CurrentDateRangeFilter { get; set; }
    }
}