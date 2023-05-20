namespace YCCodeChallenge
{
    public static class DateHelper
    {
        public static (DateTime periodFrom, DateTime periodTo) GetQuarterPeriod(int quarter, int year)
        {
            if (quarter < 1 || quarter > 4)
            {
                throw new ArgumentException("Valid values for a quarter are 1, 2, 3 and 4.");
            }

            var quarterEnd = new DateTime(year, quarter * 3, DateTime.DaysInMonth(2001, quarter * 3));
            var quarterStart = new DateTime(year, quarter * 3 - 2, 1);

            return (quarterStart, quarterEnd);
        }
    }
}
