namespace YCCodeChallenge.Tests
{
    public class DateTest
    {
        [Theory]
        [InlineData(1, 2023, "2023-01-01", "2023-03-31")]
        [InlineData(2, 2023, "2023-04-01", "2023-06-30")]
        [InlineData(3, 2023, "2023-07-01", "2023-09-30")]
        [InlineData(4, 2023, "2023-10-01", "2023-12-31")]
        public void Should_Correctly_Determine_Quarter_Start_End_Date(int quarter, int year, DateTime expectedStartDate, DateTime expectedEndDate)
        {
            (DateTime startPeriod, DateTime endPeriod) = DateHelper.GetQuarterPeriod(quarter, year);

            Assert.Equal(expectedStartDate, startPeriod);
            Assert.Equal(expectedEndDate, endPeriod);
        }

        [Theory]
        [InlineData(-1, 2023)]
        [InlineData(0, 2023)]
        [InlineData(5, 2023)]
        [InlineData(200, 2023)]
        public void Should_Throw_If_Invalid_Quarter_Number(int quarter, int year)
        {
            Action act = () => DateHelper.GetQuarterPeriod(quarter, year);

            Assert.Throws<ArgumentException>(act);
        }
    }
}
