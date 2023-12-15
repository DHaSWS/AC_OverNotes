using UnityEngine;
using System;

namespace OverNotes.System {
    public class SettingItemToggle : SettingItem {
        public bool Value;
        public bool ConstantValue { get; private set; }
        public SettingItemToggle(string name, string guideName, bool constantValue) : base(name, guideName) {
            Value = constantValue;
            ConstantValue = constantValue;
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

        public override void Back() {
            throw new Exception("Toggle‚ª‚±‚±‚É—ˆ‚é‚Ì‚Í‚ ‚è‚¦‚È‚¢‚æ");
        }
    }
}
