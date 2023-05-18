using YCCodeChallenge.Excel;

namespace YCCodeChallenge.Model
{
    [ExcelSheet("PayCodes")]
    public class PayCode
    {
        [ExcelColumn("pay_code")]
        public string Code { get; set; }

        [ExcelColumn("ote_treament")]
        public string OTETreatment { get; set; }
    }
}
