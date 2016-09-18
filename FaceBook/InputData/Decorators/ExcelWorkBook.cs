using System;
using Excel = Microsoft.Office.Interop.Excel;

namespace InputData.Decorators
{
    public class ExcelWorkBook: IDisposable
    {
        Excel.Application excel;
        Excel.Workbook workBook;

        public ExcelWorkBook(string path)
        {
            excel = new Excel.Application();
            workBook = excel.Workbooks.Open(path);
        }

        public Excel.Sheets Worksheets
        {
            get { return workBook.Worksheets; }
        }

        public void Dispose()
        {
            workBook.Close(false);
            workBook = null;
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            excel = null;
            GC.Collect();
        }
    }
}
