namespace YCCodeChallenge.Excel
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ExcelSheetAttribute : Attribute
    {
        public ExcelSheetAttribute(string name)
        {
            SheetName = name;
        }

        public string SheetName { get; set; }
    }
}
