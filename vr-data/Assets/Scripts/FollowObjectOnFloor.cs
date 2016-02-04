using UnityEngine;
using System.Collections;

public class FollowObjectOnFloor : MonoBehaviour {

    public GameObject objectToTrack;
    public float floorHeight;

	void Update () {
        transform.position = objectToTrack.transform.position;
        transform.Translate(0.0f, (0.0f - transform.position.y + floorHeight), 0.0f);
	}
}
