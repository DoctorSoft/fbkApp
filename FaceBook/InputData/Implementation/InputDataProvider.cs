using System;
using System.Collections.Generic;
using System.IO;
using Constants;
using Engines.Engines.RegistrationEngine;
using InputData.Constants;
using InputData.Decorators;
using InputData.InputModels;
using InputData.Interfaces;
using Excel = Microsoft.Office.Interop.Excel;

namespace InputData.Implementation
{
    public class InputDataProvider: IInputDataProvider
    {
        private readonly string fileName;

        public InputDataProvider(string fileName)
        {
            this.fileName = fileName;
        }
        public InputDataModel GetInputData()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = Path.Combine(currentDirectory, fileName);

            using (var workBook = new ExcelWorkBook(path))
            {
                var usersDataSheet = (Excel.Worksheet)workBook.Worksheets.Item[(int)SheetNumber.UserData];
                var usersData = ParseUserDataSheet(usersDataSheet);
                workBook.Save();

                return new InputDataModel
                {
                    usersData = usersData
                };

            }
        }

        public List<RegistrationModel> ParseUserDataSheet(Excel.Worksheet worksheet)
        {
            var users = new List<RegistrationModel>();
            var passGenerator = new PasswordGenerator();

            if (worksheet == null)
            {
                return users;
            }

            var range = worksheet.UsedRange;

            if (range == null)
            {
                return users;
            }

            var rowCount = range.Rows.Count;

            for (var rowIndex = (int)RowName.StartData; rowIndex <= rowCount; rowIndex++)
            {
                var id = (worksheet.Cells[rowIndex, (int)ColumnName.Id] as Excel.Range).Value;
                if (id == null)
                {
                    continue;
                }
                var lastName = (worksheet.Cells[rowIndex, (int)ColumnName.LastName] as Excel.Range).Value;
                var firstName = (worksheet.Cells[rowIndex, (int)ColumnName.FirstName] as Excel.Range).Value;
                var email = (worksheet.Cells[rowIndex, (int)ColumnName.Email] as Excel.Range).Value;
                var birthday = (worksheet.Cells[rowIndex, (int)ColumnName.Birthday] as Excel.Range).Value;
                var gender = (worksheet.Cells[rowIndex, (int)ColumnName.Gender] as Excel.Range).Value;
                var facebookPassword = (worksheet.Cells[rowIndex, (int)ColumnName.FacebookPassword] as Excel.Range).Value;
                var emailPassword = (worksheet.Cells[rowIndex, (int)ColumnName.EmailPassword] as Excel.Range).Value;

                if (facebookPassword == null)
                {
                    facebookPassword = passGenerator.Generate(8);
                    (worksheet.Cells[rowIndex, (int)ColumnName.FacebookPassword] as Excel.Range).Value = facebookPassword;
                }

                if (id == null || lastName == null || firstName == null || email == null || birthday == null || gender == null)
                {
                    continue;
                }

                users.Add(new RegistrationModel
                {
                    Id = Convert.ToInt16(id),
                    LastName = lastName,
                    FirstName = firstName,
                    Email = email,
                    FacebookPassword = facebookPassword,
                    EmailPassword = emailPassword,
                    Birthday = Convert.ToDateTime(birthday),
                    Gender = gender == 1 ? Gender.Female : Gender.Male
                });
            }

            return users;
        }
    }
}
