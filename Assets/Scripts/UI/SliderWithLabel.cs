using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderWithLabel : MonoBehaviour
{
    public float Value => _slider.value;

    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _label;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
        OnSliderValueChanged(Value);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        _label.text = value.ToString();
    }
}
