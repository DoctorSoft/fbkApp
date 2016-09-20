using Engines.Engines.RegistrationEngine;

namespace ChangeExcel.Interfaces
{
    public interface IRecordInExcel
    {
        bool RecordRegistratedStatus(RegistrationModel model, bool status);

        bool RecordUserUserHomeLink(RegistrationModel model, string link);
    }
}
