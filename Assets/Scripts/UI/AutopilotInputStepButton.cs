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
        label.text = string.Format("{0}", value);
    }

    public void OnButtonClicked() {
        OnClicked(value);
    }
}
