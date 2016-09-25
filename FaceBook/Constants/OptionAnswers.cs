using System.ComponentModel;

namespace Constants
{
    public enum OptionAnswers
    {
        [Description("Друзья")]
        Friends,
        [Description("Только я")]
        OnlyI,
        [Description("Никто")]
        Nobody,
        [Description("Включено")]
        Enabled,
        [Description("Отключено")]
        Disabled
    }
}
