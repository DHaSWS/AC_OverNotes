using System;

namespace OverNotes.System {
    public class SettingItemValue : SettingItem {
        // åªç›ÇÃíl
        public object Value;
        // ílÇÃå^
        private Type _valueType;

        // ëùå∏ó 
        private object _deltaValue;

        // ç≈ëÂíl
        private object _maxValue;
        // ç≈è¨íl
        private object _minValue;

        public SettingItemValue(
            string name,
            string guideName,
            object constantValue,
            object deltaValue,
            object maxValue,
            object minValue
            ) : base(name, guideName) {
            Value = constantValue;
            _valueType = constantValue.GetType();

            // å^Ç™àÍívÇµÇƒÇÈÇ©îªï Ç∑ÇÈ
            if (_valueType != deltaValue.GetType()) {
                throw new ArgumentException();
            }
            if (_valueType != maxValue.GetType()) {
                throw new ArgumentException();
            }
            if (_valueType != minValue.GetType()) {
                throw new ArgumentException();
            }

            _deltaValue = deltaValue;
            _maxValue = maxValue;
            _minValue = minValue;
        }

        private dynamic Calculate(dynamic value, dynamic deltaValue, dynamic maxValue, dynamic minValue) {
            return Math.Clamp(value + deltaValue, minValue, maxValue);
        }

        public override void Minus() {
            dynamic convertedValue = Convert.ChangeType(Value, _valueType);
            dynamic convertedDeltaValue = Convert.ChangeType(_deltaValue, _valueType);
            dynamic convertedMaxValue = Convert.ChangeType(_maxValue, _valueType);
            dynamic convertedMinValue = Convert.ChangeType(_minValue, _valueType);

            convertedValue = Calculate(convertedValue, convertedDeltaValue * -1, convertedMaxValue, convertedMinValue);
        }

        public override void Plus() {
            dynamic convertedValue = Convert.ChangeType(Value, _valueType);
            dynamic convertedDeltaValue = Convert.ChangeType(_deltaValue, _valueType);
            dynamic convertedMaxValue = Convert.ChangeType(_maxValue, _valueType);
            dynamic convertedMinValue = Convert.ChangeType(_minValue, _valueType);

            convertedValue = Calculate(convertedValue, convertedDeltaValue, convertedMaxValue, convertedMinValue);
        }

        public override void Select() {
            switch (SelectContext.selectRoutine) {
                case SelectContext.SelectRoutine.Setting: {
                        SelectContext.selectRoutine = SelectContext.SelectRoutine.Setting_Value;
                        SettingPanelParams.IsTriggered = true;

                        GuideMessage.guideLane1 = "";
                        GuideMessage.guideLane2 = "ï€ë∂";
                        GuideMessage.guideLane3 = "å∏ÇÁÇ∑";
                        GuideMessage.guideLane4 = "ëùÇ‚Ç∑";
                        break;
                    }
                case SelectContext.SelectRoutine.Setting_Value: {
                        SelectContext.selectRoutine = SelectContext.SelectRoutine.Setting;

                        GuideMessage.guideLane1 = "ñﬂÇÈ";
                        GuideMessage.guideLane2 = "åàíË";
                        GuideMessage.guideLane3 = "ëOÇ÷";
                        GuideMessage.guideLane4 = "éüÇ÷";
                        break;
                    }
            }
        }

        public override object GetValue() {
            return Value;
        }
    }
}