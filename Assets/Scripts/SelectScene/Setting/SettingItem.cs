using System;
using Unity.VisualScripting;

namespace OverNotes.System {
    public abstract class SettingItem {
        // 設定アイテム名
        public string Name;
        // その設定のガイド
        public string GuideName;

        public SettingItem(string name, string guideName) {
            Name = name;
            GuideName = guideName;
        }

        public abstract void Select();
        public abstract void Plus();
        public abstract void Minus();

        public abstract object GetValue();
    }
}