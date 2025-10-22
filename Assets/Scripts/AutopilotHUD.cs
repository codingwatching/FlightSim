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

    public void OnSwitchTakeoff() {
        autopilot.EnterTakeoffMode();
    }

    public void OnSwitchNavigate() {
        autopilot.EnterNavigateMode();
    }

    public void OnSwitchLanding() {
        autopilot.EnterLandingMode();
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
