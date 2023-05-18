using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExcelDataReader;
using YCCodeChallenge.Excel;
using YCCodeChallenge.Model;

namespace YCCodeChallenge.Repository
{
    public class SuperRepository : ISuperRepository
    {
        private readonly IExcelReader _excelReader;

        public SuperRepository(IExcelReader excelReader)
        {
            _excelReader = excelReader;
        }

        public void GetPayCodes()
        {
            _excelReader.GetItems<PayCode>();
        }
    }
}
