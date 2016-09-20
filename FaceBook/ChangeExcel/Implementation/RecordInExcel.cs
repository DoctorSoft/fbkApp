using System;
using System.Collections.Generic;
using System.IO;
using ChangeExcel.Interfaces;
using Engines.Engines.RegistrationEngine;
using InputData.Constants;
using InputData.Decorators;
using InputData.Implementation;
using InputData.InputModels;
using Excel = Microsoft.Office.Interop.Excel;

namespace ChangeExcel.Implementation
{
    public class RecordInExcel: IRecordInExcel
    {
        private readonly string fileName;

        public RecordInExcel(string fileName)
        {
            this.fileName = fileName;
        }

        public bool RecordRegistratedStatus(RegistrationModel model, bool status)
        {

            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.Combine(currentDirectory, fileName);

            using (var workBook = new ExcelWorkBook(path))
            {
                var usersDataSheet = (Excel.Worksheet)workBook.Worksheets.Item[(int)SheetNumber.UserData];
                var recordingStatus = RecordStatus(usersDataSheet, model.Id, status);

                workBook.Save();

                return recordingStatus;
            }
        }

        public bool RecordUserUserHomeLink(RegistrationModel model, string link)
        {
            throw new System.NotImplementedException();
        }

        public bool RecordStatus(Excel.Worksheet worksheet, int userId, bool status)
        {
            var range = worksheet.UsedRange;

            var rowCount = range.Rows.Count;

            for (var rowIndex = (int)RowName.StartData; rowIndex <= rowCount; rowIndex++)
            {
                var id = (worksheet.Cells[rowIndex, (int)ColumnName.Id] as Excel.Range).Value;
                if ((id == null) || (id != userId))
                {
                    continue;
                }
                (worksheet.Cells[rowIndex, (int)ColumnName.Registrated] as Excel.Range).Value = status;
                break;
            }

            return true;
        }
    }
}
