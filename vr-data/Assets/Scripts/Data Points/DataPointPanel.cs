using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DataPointPanel : MonoBehaviour {

    public DataPoint dataPoint;

    public TextMesh textTitle;
    public TextMesh textLatLong;
    public TextMesh textDateTime;
    public TextMesh textWeather;
    public TextMesh textVehiclesInvolved;
    public TextMesh textCasualities;
    public GameObject panelBlock;

    public DataPointBase pointBase;

    void Update()
    {
        UpdatePositionForDataPoint();
        UpdateLineToDataPoint();
    }

    public void SetUpPanelWithNewDataPoint(DataPoint dp)
    {
        List<KeyValuePair<string, object>> data = dp.data.ToList();
        string lat = "", lon = "", date = "", time = "";
        for (int i = 0; i < data.Count; i++)
        {
            switch (data[i].Key)
            {
                case "Severity":
                    textTitle.text = data[i].Value.ToString() + " RTC";
                    break;
                case "Latitude":
                    lat = data[i].Value.ToString();
                    break;
                case "Longitude":
                    lon = data[i].Value.ToString();
                    break;
                case "Date":
                    date = data[i].Value.ToString();
                    break;
                case "Time":
                    time = data[i].Value.ToString();
                    break;
                case "Weather Conditions":
                    textWeather.text = data[i].Value.ToString();
                    break;
                case "Vehicles Involved":
                    textVehiclesInvolved.text = data[i].Value.ToString();
                    break;
                case "Casualties":
                    textCasualities.text = data[i].Value.ToString();
                    break;
                default:
                    break;
            }
        }

        textLatLong.text = lat + ", " + lon;
        textDateTime.text = date + " at " + time;

        dataPoint = dp;
        SetUpPointBase();
    }

    public void SetUpPointBase()
    {
        pointBase.dataPointToTrack = dataPoint;
    }

    void UpdatePositionForDataPoint()
    {
        Vector3 dpPosition = dataPoint.transform.position;
        Vector3 headPosition = GameObject.Find("Camera (head)").transform.position;

        float metersAbovePoint = 1.2f;

        transform.position = new Vector3(dpPosition.x, metersAbovePoint, dpPosition.z);
        transform.LookAt(new Vector3(headPosition.x, metersAbovePoint, headPosition.z));
        transform.Rotate(transform.up, 90f);
    }

    void UpdateLineToDataPoint()
    {
        Vector3 bottomOfPanelPosition = panelBlock.transform.position - (panelBlock.GetComponent<MeshRenderer>().bounds.size.y * Vector3.up * 0.5f);
        Vector3 dataPointPosition = pointBase.transform.position;

        LineRenderer lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, bottomOfPanelPosition);
        lr.SetPosition(1, dataPointPosition);
        lr.enabled = true;
    }
}
