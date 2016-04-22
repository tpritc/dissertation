using UnityEngine;
using System.Collections;

public class MouseSelection : MonoBehaviour {

    public Camera regularCamera;

	void Update () {
        CheckForClick();
	}

    void CheckForClick() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = regularCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f)) {
                if (hit.collider.gameObject.GetComponent<DataPoint>()) {
                    hit.collider.gameObject.GetComponent<DataPoint>().TogglePanel();
                } else if (hit.collider.gameObject.transform.parent.gameObject.GetComponent<DataPointPanel>()) {
                    hit.collider.gameObject.transform.parent.gameObject.GetComponent<DataPointPanel>().dataPoint.TogglePanel();
                }
                
            }
        }
    }
}
