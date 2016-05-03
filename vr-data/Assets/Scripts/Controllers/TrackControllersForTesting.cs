using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

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
            ExportDataToFile();
        } else {
            Debug.LogError("Can't end a tracking session if one isn't started.");
        }
    }

    public void ExportDataToFile() {

        string filename = "data-export-" + DateTime.Now.ToString("yyyy-MM-dd-H-mm-ss") + ".csv";

        if (File.Exists(filename)) {
            Debug.LogWarning(filename + " already exists. Overwriting it!");
            File.Delete(filename);
        }
        StreamWriter sw = File.CreateText(filename);
        sw.WriteLine("time,headMovement,leftControllerMovement,rightControllerMovement");

        float exportInterval = 1f;
        float nextExportTimeInSeconds = exportInterval;

        float headMovSinceLastInterval = 0f;
        float leftControllerMovSinceLastInterval = 0f;
        float rightControllerMovSinceLastInterval = 0f;

        for (int i = 0; i < times.Count; i++) {
            if (times[i] >= nextExportTimeInSeconds) {
                sw.WriteLine(nextExportTimeInSeconds.ToString() + ", " + headMovSinceLastInterval.ToString() + ", " + leftControllerMovSinceLastInterval.ToString() + ", " + rightControllerMovSinceLastInterval.ToString());

                headMovSinceLastInterval = 0f;
                leftControllerMovSinceLastInterval = 0f;
                rightControllerMovSinceLastInterval = 0f;
                nextExportTimeInSeconds += exportInterval;
            } else {
                headMovSinceLastInterval += headMovements[i];
                leftControllerMovSinceLastInterval += leftControllerMovements[i];
                rightControllerMovSinceLastInterval += rightControllerMovements[i];
            }
        }
        sw.Close();        
    }
}
