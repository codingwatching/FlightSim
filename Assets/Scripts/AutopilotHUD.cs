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

    StringBuilder builder;

    void Start() {
        builder = new StringBuilder();

        InitInputs();
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

    public void OnSwitchTakeoff() {
        ShowPanel(takeoffModeInfo, true);
        ShowPanel(navigateModeInfo, false);
        ShowPanel(landingModeInfo, false);
        autopilot.EnterTakeoffMode();
    }

    public void OnSwitchNavigate() {
        ShowPanel(takeoffModeInfo, false);
        ShowPanel(navigateModeInfo, true);
        ShowPanel(landingModeInfo, false);
        autopilot.EnterNavigateMode();
    }

    public void OnSwitchLanding() {
        ShowPanel(takeoffModeInfo, false);
        ShowPanel(navigateModeInfo, false);
        ShowPanel(landingModeInfo, true);
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
    }

    void UpdateInputs() {

    }
}
