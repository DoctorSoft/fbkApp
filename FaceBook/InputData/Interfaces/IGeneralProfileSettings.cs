using System.Collections.Generic;
using Engines.Engines.Models;

namespace InputData.Interfaces
{
    public interface IGeneralProfileSettings
    {
        List<GeneralProfileSettingsModel> GetGeneralProfileSettings();
    }
}
