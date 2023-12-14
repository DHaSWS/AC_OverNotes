using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverNotes.System {
    public class SettingPanelParamsConstant {
        static public readonly int SettingIndex = 0;
        static public readonly bool IsFadeIn = true;
    }

    public class SettingPanelParams {
        static public int SettingIndex = SettingPanelParamsConstant.SettingIndex;
        static public bool IsFadeIn = SettingPanelParamsConstant.IsFadeIn;
        static public bool IsTriggered = false;
    }
}
