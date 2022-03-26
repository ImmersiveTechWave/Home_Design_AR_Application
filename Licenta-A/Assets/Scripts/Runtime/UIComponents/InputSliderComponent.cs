using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Slider;

namespace AF.UI
{
    public class InputSliderComponent : MonoBehaviour
    {
        public SliderEvent onValueChanged { get; set; } = new SliderEvent();

        private Slider slider;
        private TMP_InputField inputField;

        public float Value
        {
            get => slider.value;
            set
            {
                if (slider == null)
                {
                    Awake();
                }

                slider.value = value;
            }
        }

        public bool WholeNumbers
        {
            get => slider.wholeNumbers;
            set
            {
                slider.wholeNumbers = value;
                inputField.contentType = value ?
                    TMP_InputField.ContentType.IntegerNumber : TMP_InputField.ContentType.DecimalNumber;
            }
        }

        public float MinValue { get => slider.minValue; set => slider.minValue = value; }
        public float MaxValue { get => slider.maxValue; set => slider.maxValue = value; }

        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
            inputField = GetComponentInChildren<TMP_InputField>();
            AddListeners();
        }

        private void AddListeners()
        {
            slider.onValueChanged.AddListener(UpdateComponent);
            inputField.onValueChanged.AddListener(UpdateComponent);
            UpdateComponent(slider.value);
        }

        private void UpdateComponent(float value)
        {
            var finalValue = Mathf.Clamp(value, slider.minValue, slider.maxValue);
            slider.value = finalValue;
            inputField.SetTextWithoutNotify(finalValue.ToString());
            this.Value = value;
            if (onValueChanged != null)
            {
                onValueChanged.Invoke(value);
            }
        }

        private void UpdateComponent(string value)
        {
            UpdateComponent(float.Parse(value.ToString()));
        }

        public void Destroy()
        {
            slider.onValueChanged.RemoveListener(UpdateComponent);
            inputField.onValueChanged.RemoveListener(UpdateComponent);
        }

        private void OnDestroy()
        {
            Destroy();
        }
    }
}
