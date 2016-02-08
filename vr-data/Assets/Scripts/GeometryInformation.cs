using UnityEngine;
using System.Collections;

public class GeometryInformation : MonoBehaviour {

    public float topLeftLatitude;
    public float topLeftLongitude;

    public float bottomRightLatitude;
    public float bottomRightLongitude;

    private float deltaLatitude;
    private float deltaLongitude;

    public float mapHeight;
    public float mapWidth;

    void Start()
    {
        deltaLatitude = topLeftLatitude - bottomRightLatitude;
        deltaLongitude = topLeftLongitude - bottomRightLongitude;
    }

    public Vector3 GetPositionForCoordinate(float latitude, float longitude)
    {
        Vector3 position = new Vector3();
        position.x = GetXZPositionForCoordinate(latitude, longitude).x;
        position.z = GetXZPositionForCoordinate(latitude, longitude).y;
        position.y = GetAltitudeInMetresForPosition(GetXZPositionForCoordinate(latitude, longitude));
        return position;
    }

    private Vector2 GetXZPositionForCoordinate(float latitude, float longitude)
    {
        Vector2 returnPosition = new Vector2(0.0f, 0.0f);

        returnPosition.x = (((longitude - bottomRightLongitude) / deltaLongitude) * mapWidth) - (mapWidth / 2);
        returnPosition.y = (((latitude - bottomRightLatitude) / deltaLatitude) * mapHeight) - (mapHeight / 2);

        print(returnPosition.x + ", " + returnPosition.y);

        // (((50.3763 - 50.3482) / (50.4043 - 50.3482)) * 6240) - (6240 / 2)
        // ((      0.0281        /        0.0561      ) * 6240) - 3210
        // (                  0.50089                   * 6240) - 3210
        //                                 3125.5615            - 3210
        //                                              -84.4385

        return returnPosition;
    }

    private float GetAltitudeInMetresForPosition(Vector2 position)
    {
        Ray raycast = new Ray(new Vector3(position.x, 10000.0f, position.y), Vector3.down);
        RaycastHit hitInfo;
        bool hasTarget = Physics.Raycast(raycast, out hitInfo);

        if (hitInfo.distance == 0)
        {
            return 0.0f;
        }
        else
        {
            return hitInfo.point.y;
        }
    }

    public bool IsCoordinateInRange(float latitude, float longitude)
    {
        return (latitude >= bottomRightLatitude && latitude <= topLeftLatitude && longitude >= topLeftLongitude && longitude <= bottomRightLongitude);
    }
}
