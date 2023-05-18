namespace YCCodeChallenge.Excel
{
    public interface IExcelReader
    {
        List<T> GetItems<T>();
    }
}
