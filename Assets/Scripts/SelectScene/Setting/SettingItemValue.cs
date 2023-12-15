using System;
using UnityEngine;

namespace OverNotes.System {
    public class SettingItemValue : SettingItem {
        // 現在の値
        public object Value;
        // 値 管理用
        private object _oldValue;
        // 値の型
        private Type _valueType;

        // 増減量
        private object _deltaValue;

        // 最大値
        private object _maxValue;
        // 最小値
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

            // 型が一致してるか判別する
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
            if(maxValue < minValue) {
                throw new ArgumentException("minValue must be less than or equal to maxValue");
            }
            return Math.Clamp(value + deltaValue, minValue, maxValue);
        }

        public override void Minus() {
            dynamic convertedValue = Convert.ChangeType(Value, _valueType);
            dynamic convertedDeltaValue = Convert.ChangeType(_deltaValue, _valueType);
            dynamic convertedMaxValue = Convert.ChangeType(_maxValue, _valueType);
            dynamic convertedMinValue = Convert.ChangeType(_minValue, _valueType);

            Value = Calculate(convertedValue, convertedDeltaValue * -1, convertedMaxValue, convertedMinValue);
        }

        public override void Plus() {
            dynamic convertedValue = Convert.ChangeType(Value, _valueType);
            dynamic convertedDeltaValue = Convert.ChangeType(_deltaValue, _valueType);
            dynamic convertedMaxValue = Convert.ChangeType(_maxValue, _valueType);
            dynamic convertedMinValue = Convert.ChangeType(_minValue, _valueType);

            Value = Calculate(convertedValue, convertedDeltaValue, convertedMaxValue, convertedMinValue);
        }

        public override void Select() {
            switch (SelectContext.selectRoutine) {
                case SelectContext.SelectRoutine.Setting: {
                        SelectContext.selectRoutine = SelectContext.SelectRoutine.Setting_Value;
                        SettingPanelParams.IsTriggered = true;

                        // Save old value
                        _oldValue = Value;

                        GuideMessage.guideLane1 = "破棄";
                        GuideMessage.guideLane2 = "保存";
                        GuideMessage.guideLane3 = "減らす";
                        GuideMessage.guideLane4 = "増やす";
                        break;
                    }
                case SelectContext.SelectRoutine.Setting_Value: {
                        SelectContext.selectRoutine = SelectContext.SelectRoutine.Setting;

                        GuideMessage.guideLane1 = "戻る";
                        GuideMessage.guideLane2 = "決定";
                        GuideMessage.guideLane3 = "前へ";
                        GuideMessage.guideLane4 = "次へ";
                        break;
                    }
            }
        }

        public override void Back() {
            Value = _oldValue;
            GuideMessage.guideLane1 = "戻る";
            GuideMessage.guideLane2 = "決定";
            GuideMessage.guideLane3 = "前へ";
            GuideMessage.guideLane4 = "次へ";
            SelectContext.selectRoutine = SelectContext.SelectRoutine.Setting;
        }

        public override object GetValue() {
            return Value;
        }
    }
}