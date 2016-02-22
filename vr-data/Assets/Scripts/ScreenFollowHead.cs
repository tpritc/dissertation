using UnityEngine;
using System.Collections;

public class ScreenFollowHead : MonoBehaviour {

    public Transform headTransform;
    public float distance = 8.0f;
    public float height = 3.0f;

    void Start () {
        transform.position = new Vector3();
    }
	
	void Update () {
        transform.position = GetHeadVector() * distance;
        transform.Translate(Vector3.up * height);
        transform.LookAt(new Vector3(0.0f, height, 0.0f));
        transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
	}

    Vector3 GetHeadVector() {
        return new Vector3(headTransform.forward.x, 0.0f, headTransform.forward.z).normalized;
    }


}
