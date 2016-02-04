using UnityEngine;
using System.Collections;

public class TestScalePrefabWithViveController : MonoBehaviour {

    public GameObject vrFeet;
    public GameObject vrRoom;
    public float scaleRate;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTrigger())
        {
            Scale(true);
        } else if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTrigger())
        {
            Scale(false);
        }
	}

    void Scale(bool up)
    {
        Vector3 targetPos = vrRoom.transform.position;
        Vector3 feetPos = vrFeet.transform.position;
        float RS = 0.2f * Time.deltaTime;
        if (!up)
        {
            RS *= -1;
        }
        RS += 1;

        Vector3 endScale = vrRoom.transform.localScale * RS;

        Vector3 C = targetPos - feetPos;
        Vector3 finalPosition = C * RS + feetPos;

        vrRoom.transform.localScale = endScale;
        vrRoom.transform.position = finalPosition;
    }
}
