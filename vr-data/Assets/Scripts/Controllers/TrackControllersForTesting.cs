using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class TrackControllersForTesting : MonoBehaviour {

    public bool tracking;

    public GameObject head;
    public GameObject leftController;
    public GameObject rightController;

    public List<float> headMovements;
    public List<float> leftControllerMovements;
    public List<float> rightControllerMovements;
    public List<float> times;

    public float timer;
    
    private Vector3 headLastFramePosition;
    private Vector3 leftControllerLastFramePosition;
    private Vector3 rightControllerLastFramePosition;

	void Update() {
        if (tracking) {
            timer += Time.deltaTime;

            headMovements.Add(Vector3.Distance(headLastFramePosition, head.transform.position));
            leftControllerMovements.Add(Vector3.Distance(leftControllerLastFramePosition, leftController.transform.position));
            rightControllerMovements.Add(Vector3.Distance(rightControllerLastFramePosition, rightController.transform.position));
            times.Add(timer);

            headLastFramePosition = head.transform.position;
            leftControllerLastFramePosition = leftController.transform.position;
            rightControllerLastFramePosition = rightController.transform.position;
        }
	}

    public void StartTracking() {
        if (!tracking) {
            headLastFramePosition = head.transform.position;
            leftControllerLastFramePosition = leftController.transform.position;
            rightControllerLastFramePosition = rightController.transform.position;
            tracking = true;
        } else {
            Debug.LogError("Can't start tracking if we're already tracking. End a session first.");
        }
    }

    public void EndTracking() {
        if (tracking) {
            tracking = false;
            ExportDataToFile("test.csv");
        } else {
            Debug.LogError("Can't end a tracking session if one isn't started.");
        }
    }

    public void ExportDataToFile(string filename) {
        if (File.Exists(filename)) {
            Debug.LogWarning(filename + " already exists. Overwriting it!");
            File.Delete(filename);
        }
        StreamWriter sw = File.CreateText(filename);
        sw.WriteLine("time,headMovement,leftControllerMovement,rightControllerMovement");
        for (int i = 0; i < times.Count; i++) {
            sw.WriteLine(times[i].ToString() + ", " + headMovements[i].ToString() + ", " + leftControllerMovements[i].ToString() + ", " + rightControllerMovements[i].ToString());
        }
        sw.Close();        
    }
}
