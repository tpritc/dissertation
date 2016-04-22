using UnityEngine;
using System.Collections;

public class VRModeManager : MonoBehaviour {

    public bool virtualRealityMode = true;

    public GameObject VR;
    public GameObject nonVR;

	void Start () {
        if (virtualRealityMode) {
            VR.SetActive(true);
            nonVR.SetActive(false);
        } else {
            VR.SetActive(false);
            nonVR.SetActive(true);
        }
	}
}
