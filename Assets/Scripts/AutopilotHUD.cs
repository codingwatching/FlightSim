using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class AutopilotHUD : MonoBehaviour {
    [SerializeField]
    Plane plane;
    [SerializeField]
    AutopilotController autopilot;
    [SerializeField]
    Text infoText;
    [SerializeField]
    float updateInterval;

    [SerializeField]
    GameObject takeoffModeInfo;
    [SerializeField]
    GameObject navigateModeInfo;
    [SerializeField]
    GameObject landingModeInfo;

    [Header("Takeoff")]
    [SerializeField]
    AutopilotInput takeoffRotationSpeedInput;
    [SerializeField]
    AutopilotInput takeoffAngleInput;
    [SerializeField]
    AutopilotInput takeoffTargetSpeedInput;
    [SerializeField]
    AutopilotInput takeoffTargetAltitudeInput;
    [SerializeField]
    AutopilotInput takeoffClimbRateInput;

    [Header("Navigate")]
    [SerializeField]
    AutopilotDropdown navigatePitchMode;
    [SerializeField]
    AutopilotDropdown navigateAltitudeMode;
    [SerializeField]
    AutopilotInput navigateTargetHeadingInput;
    [SerializeField]
    AutopilotInput navigateTargetPitchInput;
    [SerializeField]
    AutopilotInput navigateTargetSpeedInput;
    [SerializeField]
    AutopilotInput navigateTargetAltitudeInput;
    [SerializeField]
    AutopilotInput navigateClimbRateInput;

    StringBuilder builder;

    void Start() {
        builder = new StringBuilder();
        autopilot.OnModeChanged += OnAutopilotModeChanged;

        InitInputs();
    }

    void OnDestroy() {
        autopilot.OnModeChanged -= OnAutopilotModeChanged;
    }

    void Update() {
        builder.Clear();
        autopilot.WriteDebugString(builder);
        infoText.text = builder.ToString();

        UpdateInputs();
    }

    void ShowPanel(GameObject panel, bool value) {
        if (panel == null) return;
        panel.SetActive(value);
    }

    void OnAutopilotModeChanged(AutopilotController.AutopilotMode mode) {
        ShowPanel(takeoffModeInfo, false);
        ShowPanel(navigateModeInfo, false);
        ShowPanel(landingModeInfo, false);

        switch (mode) {
            case AutopilotController.AutopilotMode.Idle:
                break;
            case AutopilotController.AutopilotMode.Takeoff:
                ShowPanel(takeoffModeInfo, true);
                break;
            case AutopilotController.AutopilotMode.Navigate:
                ShowPanel(navigateModeInfo, true);
                break;
            case AutopilotController.AutopilotMode.Landing:
                ShowPanel(landingModeInfo, true);
                break;
        }
    }

    public void OnSwitchTakeoff() {
        autopilot.EnterTakeoffMode();
    }

    public void OnSwitchNavigate() {
        autopilot.EnterNavigateMode();
    }

    public void OnSwitchLanding() {
        autopilot.EnterLandingMode();
    }

    public void StartTakeoff() {
        autopilot.StartTakeoff();
    }

    void InitInputs() {
        takeoffRotationSpeedInput.SetValue(autopilot.takeoffMode.rotationSpeedKts);
        takeoffAngleInput.SetValue(autopilot.takeoffMode.rotationAngle);
        takeoffTargetSpeedInput.SetValue(autopilot.takeoffMode.takeoffTargetSpeedKts);
        takeoffTargetAltitudeInput.SetValue(autopilot.takeoffMode.finishTakeoffMinFtAGL);
        takeoffClimbRateInput.SetValue(autopilot.takeoffMode.finishTakeoffClimbRateFtPerMin);

        takeoffRotationSpeedInput.OnValueChanged += (float value) => {
            autopilot.takeoffMode.rotationSpeedKts = value;
        };

        takeoffAngleInput.OnValueChanged += (float value) => {
            autopilot.takeoffMode.rotationAngle = value;
        };

        takeoffTargetSpeedInput.OnValueChanged += (float value) => {
            autopilot.takeoffMode.takeoffTargetSpeedKts = value;
        };

        takeoffTargetAltitudeInput.OnValueChanged += (float value) => {
            autopilot.takeoffMode.finishTakeoffMinFtAGL = value;
        };

        takeoffClimbRateInput.OnValueChanged += (float value) => {
            autopilot.takeoffMode.finishTakeoffClimbRateFtPerMin = value;
        };

        navigatePitchMode.SetValue((int)autopilot.navigateMode.pitchControlMode);
        navigateAltitudeMode.SetValue((int)autopilot.navigateMode.altitudeControlMode);
        navigateTargetHeadingInput.SetValue(autopilot.navigateMode.targetHeading);
        navigateTargetPitchInput.SetValue(autopilot.navigateMode.targetPitch);
        navigateTargetSpeedInput.SetValue(autopilot.navigateMode.targetSpeedKts);
        navigateTargetAltitudeInput.SetValue(autopilot.navigateMode.targetAltitudeFt);
        navigateClimbRateInput.SetValue(autopilot.navigateMode.targetClimbRateFtPerMin);

        navigatePitchMode.OnValueChanged += (int value) => {
            autopilot.navigateMode.pitchControlMode = (AutopilotController.NavigateModeState.PitchControlMode)value;
        };

        navigateAltitudeMode.OnValueChanged += (int value) => {
            autopilot.navigateMode.altitudeControlMode = (AutopilotController.NavigateModeState.AltitudeControlMode)value;
        };

        navigateTargetHeadingInput.OnValueChanged += (float value) => {
            autopilot.navigateMode.targetHeading = value;
        };

        navigateTargetPitchInput.OnValueChanged += (float value) => {
            autopilot.navigateMode.targetPitch = value;
        };

        navigateTargetSpeedInput.OnValueChanged += (float value) => {
            autopilot.navigateMode.targetSpeedKts = value;
        };

        navigateTargetAltitudeInput.OnValueChanged += (float value) => {
            autopilot.navigateMode.targetAltitudeFt = value;
        };

        navigateClimbRateInput.OnValueChanged += (float value) => {
            autopilot.navigateMode.targetClimbRateFtPerMin = value;
        };
    }

    void UpdateInputs() {

    }
}
