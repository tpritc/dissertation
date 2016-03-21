using UnityEngine;
using System.Collections;

public class ControllerNaturalManipulation : MonoBehaviour {

    public GameObject objectToManipulate;
    public SteamVR_ControllerManager steamVRControllerManager;

    private GameObject controllerLeft;
    private GameObject controllerRight;

    private float lastDistanceBetweenControllers;
    private Vector3 lastAveragePositionOfControllers;
    private float lastRotationOfControllers;


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
        lastRotationOfControllers = RotationInDegreesOfControllers();
	}

    void UpdateObjectPosition()
    {
        if (BothTriggersPulled())
        {
            Vector3 translationVector = AverageControllerPosition() - lastAveragePositionOfControllers;
            translationVector.y = 0.0f;

            objectToManipulate.transform.position += translationVector;
        }
    }

    void UpdateObjectRotation()
    {
        if (BothTriggersPulled())
        {
            float amountToRotate = (RotationInDegreesOfControllers() - lastRotationOfControllers);
            objectToManipulate.transform.RotateAround(AverageControllerPosition(), Vector3.up, amountToRotate);
        }
    }

    void UpdateObjectScale()
    {
        if (BothTriggersPulled())
        {
            float scaleFactor = 1 + (DistanceBetweenControllers() - lastDistanceBetweenControllers);
            float objectScaleY = objectToManipulate.transform.localScale.y;
            ScaleObjectAroundPoint(objectToManipulate, AverageControllerPosition(), scaleFactor);
            objectToManipulate.transform.localScale.Set(objectToManipulate.transform.localScale.x, objectScaleY, objectToManipulate.transform.localScale.z);
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
    
    float RotationInDegreesOfControllers()
    {
        return Mathf.Atan2(controllerLeft.transform.position.z - controllerRight.transform.position.z, controllerLeft.transform.position.x - controllerRight.transform.position.x) * Mathf.Rad2Deg * -1.0f;
    }

    bool BothTriggersPulled()
    {
        bool controllerLeftTriggerPulled = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTrigger();
        bool controllerRightTriggerPulled = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTrigger();

        return controllerLeftTriggerPulled && controllerRightTriggerPulled;
    }

    void ScaleObjectAroundPoint(GameObject objectToScale, Vector3 pivotPoint, float amountToScaleBy)
    {
        Vector3 a = objectToScale.transform.position;
        Vector3 startScale = objectToScale.transform.localScale;
        Vector3 endScale = objectToScale.transform.localScale * amountToScaleBy;
        Vector3 c = a - pivotPoint;
        Vector3 finalPosition = (c * amountToScaleBy) + pivotPoint;

        finalPosition.y = objectToScale.transform.position.y;

        objectToScale.transform.localScale = endScale;
        objectToScale.transform.position = finalPosition;
    }



}
