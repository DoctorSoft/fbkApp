using System.Collections.Generic;
using Constants;
using Engines.Engines.Models;
using InputData.Interfaces;

namespace InputData.Implementation
{
    public class GeneralProfileSettings: IGeneralProfileSettings
    {
        public List<GeneralProfileSettingsModel> GetGeneralProfileSettings()
        {
            var listSettings = new List<GeneralProfileSettingsModel>
            {
                new GeneralProfileSettingsModel
                {
                    Name = "ChroniclesAndLabelsPosting",
                    Link = SettingsUrl.ChroniclesAndLabelsPosting,
                    Answer = OptionAnswers.OnlyI
                },
                new GeneralProfileSettingsModel
                {
                    Name = "ChroniclesAndLabelsViewTagging",
                    Link = SettingsUrl.ChroniclesAndLabelsViewTagging,
                    Answer = OptionAnswers.OnlyI
                },
                new GeneralProfileSettingsModel
                {
                    Name = "ChroniclesAndLabelsViewOthers",
                    Link = SettingsUrl.ChroniclesAndLabelsViewOthers,
                    Answer = OptionAnswers.OnlyI
                },
                new GeneralProfileSettingsModel
                {
                    Name = "ChroniclesAndLabelsExpansion",
                    Link = SettingsUrl.ChroniclesAndLabelsExpansion,
                    Answer = OptionAnswers.OnlyI
                },
                new GeneralProfileSettingsModel
                {
                    Name = "ChroniclesAndLabelsSuggestion",
                    Link = SettingsUrl.ChroniclesAndLabelsSuggestion,
                    Answer = OptionAnswers.Nobody
                },
                new GeneralProfileSettingsModel
                {
                    Name = "PrivacyComposer",
                    Link = SettingsUrl.PrivacyComposeryUrl,
                    Answer = OptionAnswers.All
                }
            };

            return listSettings;
        }
    }
}
