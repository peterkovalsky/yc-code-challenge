namespace YCCodeChallenge
{
    public static class DateHelper
    {
        public static (DateTime periodFrom, DateTime periodTo) GetQuarterPeriod(int quarter, int year)
        {
            var quarterEnd = new DateTime(year, quarter * 3, 1);
            var quarterStart = quarterEnd.AddMonths(-3);

            return (quarterStart, quarterEnd);
        }
    }
}
