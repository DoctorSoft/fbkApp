using Constants;

namespace Engines.Engines.Models
{
    public class GeneralProfileSettingsModel
    {
        public string Name { get; set; }

        public SettingsUrl Link { get; set; }

        public OptionAnswers Answer { get; set; }
    }
}
