using Engines.Engines.Models;
using Engines.Engines.RegistrationEngine;

namespace ChangeExcel.Interfaces
{
    public interface IRecordInExcel
    {
        bool RecordRegistratedData(RegistrationModel model, ErrorModel errors);
    }
}
