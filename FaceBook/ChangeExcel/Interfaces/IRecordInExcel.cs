using Engines.Engines.Models;
using Engines.Engines.RegistrationEngine;

namespace ChangeExcel.Interfaces
{
    public interface IRecordInExcel
    {
        bool RecordRegistratedStatus(RegistrationModel model, ErrorModel errors);

        bool RecordUserUserHomeLink(RegistrationModel model, string link);
    }
}
