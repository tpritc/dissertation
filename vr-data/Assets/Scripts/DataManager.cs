using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataManager : MonoBehaviour {

    public string dataFile;
    public GeometryInformation geoInfo;
    public GameObject dataPointPrefab;

    public DisplayData screenDisplayData;

    public Material red;
    public Material green;

	void Start () {
        List<Dictionary<string, object>> dataSet = CSVReader.Read(dataFile);
        int completed = 0;
        int inBounds = 0;

        //foreach (var dataItem in dataSet)
        for (int i = 0; i < dataSet.Count; i++)
        {
            Dictionary<string, object> dataItem = dataSet[i];
            float lat = (float)dataItem["Latitude"];
            float lon = (float)dataItem["Longitude"];

            if (geoInfo.IsCoordinateInRange(lat, lon))
            {
                Vector3 position = geoInfo.GetPositionForCoordinate(lat, lon);
                GameObject newDataPoint = Instantiate(dataPointPrefab, position, Quaternion.identity) as GameObject;
                newDataPoint.transform.parent = transform;
                newDataPoint.name = "Data Point (" + lat + ", " + lon + ")";
                inBounds++;

                newDataPoint.GetComponent<DataPoint>().latitude = lat;
                newDataPoint.GetComponent<DataPoint>().longitude = lon;
                newDataPoint.GetComponent<DataPoint>().data = dataItem;

                screenDisplayData.SetDataPoint(newDataPoint.GetComponent<DataPoint>());
            }
            completed++;
        }
        print("Data Processed: " + completed.ToString());
        print("Data Shown:     " + inBounds.ToString());
	}
    
}
