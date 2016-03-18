using UnityEngine;
using System.Collections.Generic;


public class ControllerSelection : MonoBehaviour {

    public DisplayData screenDisplayData;

    public GameObject vrController;
    public GameObject vrHead;
    public GameObject pointerCircle;
    public TextMesh pointerText;

    public float pointerDistance = 0.2f;

    private LineRenderer lineRenderer;
    private float lineWidth = 0.002f;

    private List<GameObject> selectableDataPoints;
    private int selectedDataPointIndex = -1;
        
	void Start ()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        pointerText.text = "";
	}
	
	void Update ()
    {
        lineRenderer.SetWidth(lineWidth, lineWidth);
        lineRenderer.SetPosition(0, vrController.transform.position);
        lineRenderer.SetPosition(1, vrController.transform.position + (vrController.transform.forward * pointerDistance));
        lineRenderer.enabled = true;

        pointerCircle.transform.position = vrController.transform.position + (vrController.transform.forward * pointerDistance);
        selectableDataPoints = getSelectableObjects(pointerCircle);
        pointerText.text = selectableDataPoints.Count.ToString();
        pointerText.transform.LookAt(vrHead.transform);
        pointerText.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));

        if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerDown())
        {
            if (selectableDataPoints.Count > 0)
            {
                selectedDataPointIndex++;
                if (selectedDataPointIndex >= selectableDataPoints.Count) selectedDataPointIndex = 0;
                screenDisplayData.SetDataPoint(selectableDataPoints[selectedDataPointIndex].GetComponent<DataPoint>());

            }
        }
	}

    List<GameObject> getSelectableObjects(GameObject pointerCircle)
    {
        Collider[] hitColliders = Physics.OverlapSphere(pointerCircle.transform.position, pointerCircle.GetComponent<MeshRenderer>().bounds.extents.y / 2);
        List<GameObject> selectableObjects = new List<GameObject>();
        int i = 0;
        while (i < hitColliders.Length) {
            if (hitColliders[i].gameObject.layer == LayerMask.NameToLayer("Data Point"))
            {
                selectableObjects.Add(hitColliders[i].gameObject);
            }
            i++;
        }
        return selectableObjects;
    }
}
