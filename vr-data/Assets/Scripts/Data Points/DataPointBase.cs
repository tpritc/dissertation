using UnityEngine;
using System.Collections;

public class DataPointBase : MonoBehaviour {

    public DataPoint dataPointToTrack;

	void Update () {
        transform.position = dataPointToTrack.gameObject.transform.position;
	}
}
