using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutopilotInputStepButton : MonoBehaviour {
    [SerializeField]
    Text label;

    float value;

    public event Action<float> OnClicked = delegate { };

    public void Bind(float value) {
        this.value = value;
        label.text = value.ToString("+#;-#;+0");
    }

    public void OnButtonClicked() {
        OnClicked(value);
    }
}
