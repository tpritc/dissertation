using UnityEngine;
using System.Collections;

public class ControllerNaturalManipulation : MonoBehaviour {

    public GameObject objectToManipulate;
    public SteamVR_ControllerManager steamVRControllerManager;

    private GameObject controllerLeft;
    private GameObject controllerRight;

    private float lastDistanceBetweenControllers;
    private Vector3 lastAveragePositionOfControllers;

    void Start () {
        controllerLeft = steamVRControllerManager.left;
        controllerRight = steamVRControllerManager.right;
	}
	
	void Update () {
        UpdateObjectPosition();
        UpdateObjectRotation();
        UpdateObjectScale();

        lastAveragePositionOfControllers = AverageControllerPosition();
        lastDistanceBetweenControllers = DistanceBetweenControllers();
	}

    void UpdateObjectPosition()
    {
        if (BothTriggersPulled())
        {
            Vector3 translationVector = AverageControllerPosition() - lastAveragePositionOfControllers;
            translationVector.y = 0.0f;

            objectToManipulate.transform.Translate(translationVector);
        }
    }

    void UpdateObjectRotation()
    {

    }

    void UpdateObjectScale()
    {
        if (BothTriggersPulled())
        {
            float scaleFactor = 1 + (DistanceBetweenControllers() - lastDistanceBetweenControllers);
            Debug.Log(scaleFactor);

            Vector3 newScale = objectToManipulate.transform.localScale *= scaleFactor;
            objectToManipulate.transform.localScale.Set(newScale.x, newScale.y, newScale.z);
        }
    }

    /*
     * HELPERS
     * These are little helpers to help keep the main code nice and clean.
     */

    float DistanceBetweenControllers()
    {
        return Vector3.Distance(controllerLeft.transform.position, controllerRight.transform.position);
    }

    Vector3 AverageControllerPosition()
    {
        return (controllerLeft.transform.position + controllerRight.transform.position) / 2;
    }

    bool BothTriggersPulled()
    {
        bool controllerLeftTriggerPulled = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTrigger();
        bool controllerRightTriggerPulled = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTrigger();

        return controllerLeftTriggerPulled && controllerRightTriggerPulled;
    }


}
