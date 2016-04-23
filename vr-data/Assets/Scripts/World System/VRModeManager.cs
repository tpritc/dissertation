using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

[assembly: AssemblyVersion("1.0.*.*")]
public class VRModeManager : MonoBehaviour {

    public bool isSteamBuild = false;

    public GameObject VR;
    public GameObject nonVR;

    private bool vrMode;
    private GameObject currentCameraSystem;

    public bool VirtualRealityMode {
        get { return vrMode; }
        set {
            SetVRMode(vrMode);
        }
    }

    void SetVRMode(bool vrModeEnable) {
        if (vrModeEnable && !vrMode) {
            Destroy(currentCameraSystem);
            currentCameraSystem = Instantiate(VR);
        } else if (!vrModeEnable && vrMode) {
            Destroy(currentCameraSystem);
            currentCameraSystem = Instantiate(nonVR);
        }
        vrMode = vrModeEnable;
    }

    void Start() {
        currentCameraSystem = Instantiate(nonVR);
        VirtualRealityMode = false;
    }

    void OnGUI() {
        if (GUI.Button( new Rect( new Vector2(20f, 20f), new Vector2(100f, 40f)), "VR Mode" )) {
            SetVRMode(true);
        }
        if (GUI.Button(new Rect(new Vector2(20f, 70f), new Vector2(100f, 40f)), "Screen Mode")) {
            SetVRMode(false);
        }
        string label = "Thomas Pritchard\nPlymouth University\n\n" + GetBuildTime().ToString("R") + "\n";
        if (isSteamBuild) { label += "Steam Build"; } else { label += "Non-Steam Build"; }
        GUI.Label(new Rect(130f, 20f, 400f, 90f), label);
    }

    private DateTime GetBuildTime() {
        System.Version version = Assembly.GetExecutingAssembly().GetName().Version;
        System.DateTime startDate = new System.DateTime(2000, 1, 1, 0, 0, 0);
        System.TimeSpan span = new System.TimeSpan(version.Build, 0, 0, version.Revision * 2);
        System.DateTime buildDate = startDate.Add(span);
        return buildDate;
    }
}
