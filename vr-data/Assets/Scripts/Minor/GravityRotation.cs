using UnityEngine;
using System.Collections;

public class GravityRotation : MonoBehaviour {

    public GameObject ground;

	void Awake () {
        Physics.gravity = ground.transform.up * -1.0f;
	}
}
