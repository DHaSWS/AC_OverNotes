using System;
using Unity.VisualScripting;

namespace OverNotes.System {
    public abstract class SettingItem {
        // �ݒ�A�C�e����
        public string Name;
        // ���̐ݒ�̃K�C�h
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