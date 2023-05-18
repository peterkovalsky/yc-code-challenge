using System.Data;
using System.Reflection;
using ExcelDataReader;

namespace YCCodeChallenge.Excel
{
    public class ExcelReader : IExcelReader
    {
        private readonly string _filepath;

        public ExcelReader(string filepath)
        {
            _filepath = filepath;
        }

        public List<T> GetItems<T>()
        {
            var result = new List<T>();

            var excelSheet = (ExcelSheetAttribute?)Attribute.GetCustomAttribute(typeof(T), typeof(ExcelSheetAttribute));
            if (excelSheet == null)
            {
                throw new ArgumentException("T does not have attribute ExcelSheetAttribute");
            }

            using (var stream = File.Open(_filepath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration { }))
                {
                    var table = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    }).Tables[excelSheet.SheetName];

                    if (table == null)
                    {
                        throw new ArgumentException($"Provided excel file doesn't have a sheet with name {excelSheet.SheetName}");
                    }

                    PropertyInfo[] properties = typeof(T).GetProperties();

                    foreach (DataRow row in table.Rows)
                    {
                        T instance = Activator.CreateInstance<T>();

                        foreach (PropertyInfo property in properties)
                        {
                            var excelColumnAttribute = property.GetCustomAttributes<ExcelColumnAttribute>()?.FirstOrDefault();
                            if (excelColumnAttribute != null)
                            {
                                property.SetValue(instance, row[excelColumnAttribute.ColumnName]);
                            }
                        }

                        result.Add(instance);
                    }
                }
            }

            return result;
        }
    }
}
