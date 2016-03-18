using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataPoint : MonoBehaviour {
    public float latitude;
    public float longitude;
    public Dictionary<string, object> data;

    public void SetColor(Color color)
    {
        transform.gameObject.GetComponent<Renderer>().material.color = color;
    }
}
