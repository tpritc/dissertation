using UnityEngine;
using System.Collections;

public class ManageLocationPoints : MonoBehaviour {

    public GameObject n;
    public GameObject ne;
    public GameObject e;
    public GameObject se;
    public GameObject s;
    public GameObject sw;
    public GameObject w;
    public GameObject nw;

    public Material red;
    public Material green;

    public GameObject vrRoom;
    public GameObject vrController;

    private LineRenderer lineRenderer;
    private float lineWidth = 0.005f;

	void Start ()
    {
        SetAllPointsToColor(red);
        lineRenderer = gameObject.GetComponent<LineRenderer>();
	}
	
	void Update ()
    {

        if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTrigger())
        {
            SetAllPointsToColor(green);
            Ray raycast = new Ray(vrController.transform.position, vrController.transform.forward);
            RaycastHit hitInfo;
            bool hasTarget = Physics.Raycast(raycast, out hitInfo);

            if (hitInfo.distance == 0)
            {
                SetAllPointsToColor(red);
                lineRenderer.enabled = false;
            }
            else
            {
                transform.position = hitInfo.point;

                float localLineWidth = lineWidth * vrRoom.transform.localScale.x;
                lineRenderer.SetWidth(localLineWidth, localLineWidth);
                lineRenderer.SetPosition(0, vrController.transform.position);
                lineRenderer.SetPosition(1, hitInfo.point);
                lineRenderer.enabled = true;
            }
        }
        else
        {
            SetAllPointsToColor(red);
            lineRenderer.enabled = false;
        }
        if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerUp())
        {
            TeleportUserToCenterPoint();
        }
	}

    void SetAllPointsToColor(Material color)
    {
        n.GetComponent<Renderer>().material = color;
        ne.GetComponent<Renderer>().material = color;
        e.GetComponent<Renderer>().material = color;
        se.GetComponent<Renderer>().material = color;
        s.GetComponent<Renderer>().material = color;
        sw.GetComponent<Renderer>().material = color;
        w.GetComponent<Renderer>().material = color;
        nw.GetComponent<Renderer>().material = color;
    }

    void TeleportUserToCenterPoint()
    {
        vrRoom.transform.position = transform.position;

    }
}
