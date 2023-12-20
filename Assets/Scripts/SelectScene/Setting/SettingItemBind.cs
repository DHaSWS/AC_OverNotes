using UnityEngine;
using System;

namespace OverNotes.System {
    public class SettingItemBind : SettingItem {
        public SettingItemBind(string name, string guideName) : base(name, guideName) {
        }

        public override void Minus() {
            throw new Exception("Bind‚ª‚±‚±‚É—ˆ‚é‚Ì‚Í‚ ‚è‚¦‚È‚¢‚æ");
        }

        public override void Plus() {
            throw new Exception("Bind‚ª‚±‚±‚É—ˆ‚é‚Ì‚Í‚ ‚è‚¦‚È‚¢‚æ");
        }

        public override void Select() {
            KeyManager.Instance.StartRebinding();
        }

        public override object GetValue() {
            throw new Exception("Bind‚ª‚±‚±‚É—ˆ‚é‚Ì‚Í‚ ‚è‚¦‚È‚¢‚æ");
        }

        public override void Back() {
            throw new Exception("Bind‚ª‚±‚±‚É—ˆ‚é‚Ì‚Í‚ ‚è‚¦‚È‚¢‚æ");
        }
    }
}
