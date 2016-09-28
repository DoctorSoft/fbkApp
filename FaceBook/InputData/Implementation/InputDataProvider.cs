using System;
using System.Collections.Generic;
using System.IO;
using Constants;
using Engines.Engines.FillingGeneralInformationEngine;
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
                var usersInfoSheet = (Excel.Worksheet)workBook.Worksheets.Item[(int)SheetNumber.UserInfo];

                var usersData = ParseUserDataSheet(usersDataSheet, usersInfoSheet);

                workBook.Save();

                return new InputDataModel
                {
                    UsersData = usersData
                };

            }
        }

        public List<RegistrationModel> ParseUserDataSheet(Excel.Worksheet userDataSheet, Excel.Worksheet userInfoSheet)
        {
            var users = new List<RegistrationModel>();
            var passGenerator = new PasswordGenerator();

            if (userDataSheet == null)
            {
                return users;
            }

            var range = userDataSheet.UsedRange;

            if (range == null)
            {
                return users;
            }

            var rowCount = range.Rows.Count;

            for (var rowIndex = (int)RowName.StartData; rowIndex <= rowCount; rowIndex++)
            {
                var id = (userDataSheet.Cells[rowIndex, (int)ColumnName.Id] as Excel.Range).Value;
                if (id == null)
                {
                    continue;
                }
                var lastName = (userDataSheet.Cells[rowIndex, (int)ColumnName.LastName] as Excel.Range).Value;
                var firstName = (userDataSheet.Cells[rowIndex, (int)ColumnName.FirstName] as Excel.Range).Value;
                var email = (userDataSheet.Cells[rowIndex, (int)ColumnName.Email] as Excel.Range).Value;
                var birthday = (userDataSheet.Cells[rowIndex, (int)ColumnName.Birthday] as Excel.Range).Value;
                var gender = (userDataSheet.Cells[rowIndex, (int)ColumnName.Gender] as Excel.Range).Value;
                var facebookPassword = (userDataSheet.Cells[rowIndex, (int)ColumnName.FacebookPassword] as Excel.Range).Value;
                var emailPassword = (userDataSheet.Cells[rowIndex, (int)ColumnName.EmailPassword] as Excel.Range).Value;
                var homePageUrl = (userDataSheet.Cells[rowIndex, (int)ColumnName.HomePageUrl] as Excel.Range).Value;
                
                var userInfo = ParseUserInfoSheet(userInfoSheet, (int)id);
                userInfo.UserHomePageUrl = homePageUrl;

                if (facebookPassword == null)
                {
                    facebookPassword = passGenerator.Generate(8);
                    (userDataSheet.Cells[rowIndex, (int)ColumnName.FacebookPassword] as Excel.Range).Value = facebookPassword;
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
                    Gender = gender == 1 ? Gender.Female : Gender.Male,
                    UserInfo = userInfo,
                    HomepageUrl = homePageUrl
                });
            }

            return users;
        }

        public FillingGeneralInformationModel ParseUserInfoSheet(Excel.Worksheet userInfoSheet, int userId)
        {
            FillingGeneralInformationModel userInfo = null;

            var range = userInfoSheet.UsedRange;

            if (range == null)
            {
                return null;
            }

            var rowCount = range.Rows.Count;

            for (var rowIndex = (int)RowName.StartData; rowIndex <= rowCount; rowIndex++)
            {
                var id = (userInfoSheet.Cells[rowIndex, (int)ColumnName.Id] as Excel.Range).Value;
                if ((id != null) && (id == userId))
                {
                    var company = (userInfoSheet.Cells[rowIndex, (int)ColumnName.Company] as Excel.Range).Value;
                    var cityWork = (userInfoSheet.Cells[rowIndex, (int)ColumnName.CityWork] as Excel.Range).Value;
                    var descriptionWork = (userInfoSheet.Cells[rowIndex, (int)ColumnName.DescriptionWork] as Excel.Range).Value;
                    var post = (userInfoSheet.Cells[rowIndex, (int)ColumnName.Post] as Excel.Range).Value;
                    var univercity = (userInfoSheet.Cells[rowIndex, (int)ColumnName.Univercity] as Excel.Range).Value;
                    var univercityCity = (userInfoSheet.Cells[rowIndex, (int)ColumnName.CityUnivercity] as Excel.Range).Value;
                    var descriptionUnivercity = (userInfoSheet.Cells[rowIndex, (int)ColumnName.DescriptionUnivercity] as Excel.Range).Value;
                    var skills = (userInfoSheet.Cells[rowIndex, (int)ColumnName.Skills] as Excel.Range).Value;
                    var specializations = (userInfoSheet.Cells[rowIndex, (int)ColumnName.Specializations] as Excel.Range).Value;
                    var school = (userInfoSheet.Cells[rowIndex, (int)ColumnName.School] as Excel.Range).Value;
                    var schoolCity = (userInfoSheet.Cells[rowIndex, (int)ColumnName.CitySchool] as Excel.Range).Value;
                    var descriptionSchool = (userInfoSheet.Cells[rowIndex, (int)ColumnName.DescriptionSchool] as Excel.Range).Value;
                    var currentCity = (userInfoSheet.Cells[rowIndex, (int)ColumnName.CurrentCity] as Excel.Range).Value;
                    var nativeCity = (userInfoSheet.Cells[rowIndex, (int)ColumnName.NativeCity] as Excel.Range).Value;
                    var familyStatus = (userInfoSheet.Cells[rowIndex, (int)ColumnName.FamilyStatus] as Excel.Range).Value;

                    userInfo = new FillingGeneralInformationModel
                    {
                        Id = (int)id,
                        Company = company,
                        CityWork = cityWork,
                        DescriptionWork = descriptionWork,
                        Post = post,
                        Univercity = univercity,
                        UnivercityCity = univercityCity,
                        DescriptionUnivercity = descriptionUnivercity,
                        Skills = skills,
                        Specializations = specializations,
                        School = school,
                        SchoolCity = schoolCity,
                        DescriptionSchool = descriptionSchool,
                        CurrentCity = currentCity,
                        NativeCity = nativeCity,
                        FamilyStatus = (int)familyStatus,

                    };

                    break;
                }
            }

            return userInfo;
        }
    }
}
