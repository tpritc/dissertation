using UnityEngine;
using System.Collections;

public class GeometryInformation : MonoBehaviour {

    public float topLeftLatitude;
    public float topLeftLongitude;

    public float bottomRightLatitude;
    public float bottomRightLongitude;

    private float deltaLatitude;
    private float deltaLongitude;

    public float mapEdgeLength;

    void Awake()
    {
        deltaLatitude = topLeftLatitude - bottomRightLatitude;
        deltaLongitude = topLeftLongitude - bottomRightLongitude;

        deltaLatitude *= deltaLatitude < 0.0f ? -1.0f : 1.0f;
        deltaLongitude *= deltaLongitude < 0.0f ? -1.0f : 1.0f;

        print("deltaLat is " + deltaLatitude.ToString());
        print("deltaLon is " + deltaLongitude.ToString());

        //print(GetPositionForCoordinate(50.3688f, -4.1478f));  // Ocean Crescent
    }

    public Vector3 GetPositionForCoordinate(float latitude, float longitude)
    {
        Vector3 position = new Vector3();
        
        print("((" + latitude + " - " + bottomRightLatitude + ") / " + deltaLatitude + ") * " + mapEdgeLength);
        // Latitude
        position.z = (((latitude - bottomRightLatitude) / deltaLatitude) * mapEdgeLength);
        position.z += (position.z < 0.0f ? 1.0f : -1.0f) * (mapEdgeLength / 2);

        // Longitude
        position.x = (((longitude - bottomRightLongitude) / deltaLongitude) * mapEdgeLength);
        position.x += (position.x < 0.0f ? 1.0f : -1.0f) * (mapEdgeLength / 2);

        // Altitude
        Ray raycast = new Ray(new Vector3(position.x, 10000.0f, position.z), Vector3.down);
        RaycastHit hitInfo;
        bool hasTarget = Physics.Raycast(raycast, out hitInfo);
        position.y = hitInfo.distance == 0 ? 0.0f : hitInfo.point.y;

        print(position);
        return position;
    }

    public bool IsCoordinateInRange(float latitude, float longitude)
    {
        return (latitude >= bottomRightLatitude && latitude <= topLeftLatitude && longitude >= topLeftLongitude && longitude <= bottomRightLongitude);
    }
}
