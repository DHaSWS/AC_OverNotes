using UnityEngine;

namespace OverNotes.System {
    public class SettingItemToggle : SettingItem {
        public bool Value;
        public SettingItemToggle(string name, string guideName, bool value) : base(name, guideName) {
            Value = value;
        }

        public override void Minus() {
            throw new global::System.NotImplementedException();
        }

        public override void Plus() {
            throw new global::System.NotImplementedException();
        }

        public override void Select() {
            Value = !Value;
        }

        public override object GetValue() {
            return Value;
        }
    }
}
