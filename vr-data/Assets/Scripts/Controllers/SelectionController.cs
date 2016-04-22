using UnityEngine;
using System.Collections;

public class SelectionController : MonoBehaviour {

    public float standardDistance = 0.05f;  // 5 cm
    public float extendedDistance = 0.8f;  // Extension

    public GameObject vrController;
    public bool leftController;
    public bool rightController;

    public GameObject ringPrefab;
    public Material standardMaterial;
    public Material hoverMaterial;

    private ControllerRing ring;
    private GameObject ringGO;
    private Vector3 ringRotation = new Vector3(-3f, 0f, 0f);
    private int controllerDeviceIndex = -1;

	void Start () {
        ringGO = Instantiate(ringPrefab);
        ringGO.transform.parent = transform;
        ringGO.transform.Rotate(ringRotation);
        ringGO.transform.Translate(transform.forward * standardDistance);
        ringGO.GetComponent<MeshRenderer>().material = standardMaterial;
        ring = ringGO.GetComponent<ControllerRing>();
        ring.SetUpPositions(standardDistance * ringGO.transform.forward, extendedDistance * ringGO.transform.forward);

        SteamVR_ControllerManager SVRCM = GameObject.FindObjectOfType<SteamVR_ControllerManager>();
        if (leftController) {
            controllerDeviceIndex = (int)SVRCM.leftIndex;
        } else if (rightController) {
            controllerDeviceIndex = (int)SVRCM.rightIndex;
        } else {
            Debug.LogError("No controller checked. Things gon' fuck up.");
        }
	}
	
	void Update () {
        if (ring.Colliding) {        
            DataPoint dpToHighlight = ring.DataPointToSelect;
            ringGO.GetComponent<MeshRenderer>().material = hoverMaterial;

            bool controllerTriggerPulled = SteamVR_Controller.Input(controllerDeviceIndex).GetHairTriggerDown();
            if (controllerTriggerPulled) {
                ring.DataPointToSelect.TogglePanel();
            }
        } else {
            ringGO.GetComponent<MeshRenderer>().material = standardMaterial;
        }

        bool controllerTrackpadPressed = SteamVR_Controller.Input(controllerDeviceIndex).GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
        if (controllerTrackpadPressed) {
            ring.ExtendOrContract();
        }
	}


}
