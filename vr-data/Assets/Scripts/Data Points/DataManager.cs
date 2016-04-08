using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(World))]
public class DataManager : MonoBehaviour {

    public string dataFile;
    public GeometryInformation geoInfo;
    public GameObject dataPointPrefab;
    public GameObject dataPanelPrefab;

    public float dataPointHeightAboveGround = 0.1f;

    public void SetUpData()
    {
        List<Dictionary<string, object>> dataSet = CSVReader.Read(dataFile);
        int completed = 0;
        int inBounds = 0;

        GameObject dataHolder = new GameObject("Data Points");
        dataHolder.transform.parent = transform;
        for (int i = 0; i < dataSet.Count; i++)
        {
            Dictionary<string, object> dataItem = dataSet[i];
            float lat = (float)dataItem["Latitude"];
            float lon = (float)dataItem["Longitude"];

            if (geoInfo.IsCoordinateInRange(lat, lon))
            {
                Vector3 position = geoInfo.GetPositionForCoordinate(lat, lon) * gameObject.GetComponent<World>().startScale;
                position.y += dataPointHeightAboveGround;
                GameObject newDataPoint = Instantiate(dataPointPrefab, position, Quaternion.identity) as GameObject;
                newDataPoint.transform.parent = dataHolder.transform;
                newDataPoint.name = "Data Point (" + lat + ", " + lon + ")";
                inBounds++;

                newDataPoint.GetComponent<DataPoint>().data = dataItem;
                newDataPoint.GetComponent<DataPoint>().dataPanelPrefab = dataPanelPrefab;
            }
            completed++;
        }
        print("Data Processed: " + completed.ToString());
        print("Data Shown:     " + inBounds.ToString());
    }
    
}
