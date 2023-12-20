using System;
using UnityEngine;

namespace OverNotes.System {
    public class SettingItemValue : SettingItem {
        // ���݂̒l
        public object Value;
        // �����l
        public object ConstantValue { get; private set; }
        // �l �Ǘ��p
        private object _oldValue;
        // �l�̌^
        private Type _valueType;

        // ������
        private object _deltaValue;

        // �ő�l
        private object _maxValue;
        // �ŏ��l
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
            ConstantValue = constantValue;
            _valueType = constantValue.GetType();

            // �^����v���Ă邩���ʂ���
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

                        GuideMessage.GuideLanes[0] = "�j��";
                        GuideMessage.GuideLanes[1] = "�ۑ�";
                        GuideMessage.GuideLanes[2] = "���炷";
                        GuideMessage.GuideLanes[3] = "���₷";
                        break;
                    }
                case SelectContext.SelectRoutine.Setting_Value: {
                        SelectContext.selectRoutine = SelectContext.SelectRoutine.Setting;

                        GuideMessage.GuideLanes[0] = "�߂�";
                        GuideMessage.GuideLanes[1] = "����";
                        GuideMessage.GuideLanes[2] = "�O��";
                        GuideMessage.GuideLanes[3] = "����";
                        break;
                    }
            }
        }

        public override void Back() {
            Value = _oldValue;
            GuideMessage.GuideLanes[0] = "�߂�";
            GuideMessage.GuideLanes[1] = "����";
            GuideMessage.GuideLanes[2] = "�O��";
            GuideMessage.GuideLanes[3] = "����";
            SelectContext.selectRoutine = SelectContext.SelectRoutine.Setting;
        }

        public override object GetValue() {
            return Value;
        }
    }
}