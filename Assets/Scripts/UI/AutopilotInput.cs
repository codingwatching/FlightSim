using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AutopilotInput : MonoBehaviour {
    [SerializeField]
    float minValue;
    [SerializeField]
    float maxValue;
    [SerializeField]
    float defaultValue;
    [SerializeField]
    bool wrapValue;
    [SerializeField]
    InputField inputField;
    [SerializeField]
    List<float> stepSizes;
    [SerializeField]
    List<AutopilotInputStepButton> stepButtons;
    [SerializeField]
    bool showDefaultButton;
    [SerializeField]
    AutopilotInputStepButton defaultButton;

    public float Value { get; private set; }
    public event Action<float> OnValueChanged = delegate { };

    public void Init() {
        Value = defaultValue;
        AssignStepButtons();

        if (showDefaultButton) {
            defaultButton.IsDefault = true;
            defaultButton.Bind(defaultValue);
            defaultButton.OnClicked += OnDefaultClicked;
        }
    }

    void OnDestroy() {
        for (int i = 0; i < stepButtons.Count; i++) {
            AutopilotInputStepButton button = stepButtons[i];

            button.OnClicked -= OnStepClicked;
        }

        if (showDefaultButton) {
            defaultButton.OnClicked -= OnDefaultClicked;
        }
    }

    void AssignStepButtons() {
        if (stepSizes.Count > stepButtons.Count) {
            Debug.LogError("Cannot assign step buttons");
            return;
        }

        for (int i = 0; i < stepSizes.Count; i++) {
            float stepSize = stepSizes[i];
            AutopilotInputStepButton button = stepButtons[i];

            button.Bind(stepSize);
            button.OnClicked += OnStepClicked;
        }
    }

    void OnStepClicked(float step) {
        UpdateValue(Value + step);
    }

    void OnDefaultClicked(float value) {
        UpdateValue(value);
    }

    public void OnTextInput(string textValue) {
        float value = float.Parse(textValue, System.Globalization.NumberStyles.Number);
        UpdateValue(value);
    }

    public void SetValue(float newValue) {
        SetNewValue(newValue);
    }

    void SetNewValue(float newValue) {
        if (wrapValue) {
            float offset = newValue - minValue;
            float range = maxValue - minValue;
            float mod = ((offset % range) + range) % range;

            Value = minValue + mod;
        } else {
            Value = Mathf.Clamp(newValue, minValue, maxValue);
        }

        inputField.SetTextWithoutNotify(string.Format("{0}", Value));
    }

    void UpdateValue(float newValue) {
        SetNewValue(newValue);
        OnValueChanged(Value);
    }
}
