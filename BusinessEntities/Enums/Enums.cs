namespace BusinessEntities.Enums
{
    public enum AppSettings
    {
        HangUpMessage = 1,
        DaysSupply = 2,
        CallIdelTime = 3,
    }
    public enum DateRangeFilter
    {
        /// <summary>
        /// The today
        /// </summary>
        Today = 1,
        /// <summary>
        /// The yesterday
        /// </summary>
        Yesterday = 2,
        /// <summary>
        /// The last 7 days
        /// </summary>
        /// <summary>
        /// The last 2 days
        /// </summary>
        Last2Days = 15,
        /// <summary>
        /// The last 3 days
        /// </summary>
        Last7Days = 3,
        /// <summary>
        /// The last 14 days
        /// </summary>
        Last14Days = 16,
        /// <summary>
        /// The last 30 days
        /// </summary>
        Last30Days = 4,
        /// <summary>
        /// The last 60 days
        /// </summary>
        Last60Days = 5,
        /// <summary>
        /// The last 60 days
        /// </summary>
        Last90Days = 6,
        /// <summary>
        /// The Month To Date
        /// </summary>
        MonthToDate = 7,
        /// <summary>
        /// The Previous Month
        /// </summary>
        PreviousMonth = 8,
        /// <summary>
        /// The Current Quarter
        /// </summary>
        CurrentQuarter = 9,
        /// <summary>
        /// The Previous Quarter
        /// </summary>
        PreviousQuarter = 10,
        /// <summary>
        /// The Year To Date
        /// </summary>
        YearToDate = 11,
        /// <summary>
        /// The Previous Year
        /// </summary>
        PreviousYear = 12,
        /// <summary>
        /// Custom Date Range
        /// </summary>
        CustomDateRange = 13,
        /// <summary>
        /// All
        /// </summary>
        All = 14,
    }
    /// <summary>
    /// The Year Quarter
    /// </summary>
    public enum Quarter
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4
    }
    /// <summary>
    /// The Year Month
    /// </summary>
    public enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }
    public enum GenderEnum
    {
        Male = 1,
        FeMale = 2
    }
    public enum UserRoleEnum
    {
        SuperAdmin = 1,
        Physician = 2
    }
    public enum StatusCodes
    {
        Success = 200,
        Error = 404
    }
    
    public abstract class SystemSettingsVariables
    {
        public const string FromEmail = "FromMail";
        public const string SmtpClient = "SmtpClient";
        public const string SmtpPort = "SmtpPort";
        public const string SmtpUser = "SmtpUser";
        public const string SmtpPassword = "SmtpPassword";
    }
}