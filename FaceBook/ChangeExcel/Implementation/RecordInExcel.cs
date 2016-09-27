using System.IO;
using ChangeExcel.Interfaces;
using Engines.Engines.RegistrationEngine;
using InputData.Constants;
using InputData.Decorators;
using Excel = Microsoft.Office.Interop.Excel;
using Engines.Engines.Models;

namespace ChangeExcel.Implementation
{
    public class RecordInExcel: IRecordInExcel
    {
        private readonly string fileName;

        public RecordInExcel(string fileName)
        {
            this.fileName = fileName;
        }

        public bool RecordRegistratedData(RegistrationModel model, ErrorModel errors)
        {

            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.Combine(currentDirectory, fileName);

            using (var workBook = new ExcelWorkBook(path))
            {
                var usersDataSheet = (Excel.Worksheet)workBook.Worksheets.Item[(int)SheetNumber.UserData];
                var recordingStatus = RecordData(usersDataSheet, model, errors);

                workBook.Save();

                return recordingStatus;
            }
        }

        public bool RecordData(Excel.Worksheet worksheet, RegistrationModel model, ErrorModel errors)
        {
            var range = worksheet.UsedRange;
            var statusRegistration = GetStatus(errors);

            var rowCount = range.Rows.Count;

            for (var rowIndex = (int)RowName.StartData; rowIndex <= rowCount; rowIndex++)
            {
                var id = (worksheet.Cells[rowIndex, (int)ColumnName.Id] as Excel.Range).Value;
                var homePageUrl = (worksheet.Cells[rowIndex, (int) ColumnName.HomePageUrl] as Excel.Range).Value;
                if ((id == null) || (id != model.Id))
                {
                    continue;
                }
                (worksheet.Cells[rowIndex, (int)ColumnName.RegistratedStatus] as Excel.Range).Value = statusRegistration;
                if (homePageUrl == null) (worksheet.Cells[rowIndex, (int)ColumnName.HomePageUrl] as Excel.Range).Value = model.HomepageUrl;
                if (!statusRegistration) (worksheet.Cells[rowIndex, (int)ColumnName.TextError] as Excel.Range).Value = errors.ErrorText;
                break;
            }

            return true;
        }

        private bool GetStatus(ErrorModel errors)
        {
            return errors == null;
        }
    }
}
