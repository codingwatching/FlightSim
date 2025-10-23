using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutopilotDropdown : MonoBehaviour {
    [SerializeField]
    Dropdown dropdown;

    public event Action<int> OnValueChanged = delegate { };

    public void OnValueSelected(int value) {
        OnValueChanged(value);
    }

    public void SetValue(int value) {
        dropdown.SetValueWithoutNotify(value);
    }
}
