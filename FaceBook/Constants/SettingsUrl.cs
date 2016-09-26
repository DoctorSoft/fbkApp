﻿using System.ComponentModel;

namespace Constants
{
    public enum SettingsUrl
    {
        [Description("https://www.facebook.com/settings?tab=privacy")]
        PrivacyUrl,

        [Description("https://www.facebook.com/settings?tab=privacy&section=composer&view")]
        PrivacyComposeryUrl,

        [Description("https://www.facebook.com/settings?tab=timeline")]
        ChroniclesAndLabels,

        [Description("https://www.facebook.com/settings?tab=timeline&section=posting&view")]
        ChroniclesAndLabelsPosting,

        [Description("https://www.facebook.com/settings?tab=timeline&section=tagging&view")]
        ChroniclesAndLabelsViewTagging,

        [Description("https://www.facebook.com/settings?tab=timeline&section=others&view")]
        ChroniclesAndLabelsViewOthers,

        [Description("https://www.facebook.com/settings?tab=timeline&section=expansion&view")]
        ChroniclesAndLabelsExpansion,

        [Description("https://www.facebook.com/settings?tab=timeline&section=suggestions&view")]
        ChroniclesAndLabelsSuggestion


    }
}
