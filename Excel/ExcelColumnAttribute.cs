namespace YCCodeChallenge.Excel
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class ExcelColumnAttribute : Attribute
    {
        public ExcelColumnAttribute(string name)
        {
            ColumnName = name;
        }

        public string ColumnName { get; set; }
    }
}
