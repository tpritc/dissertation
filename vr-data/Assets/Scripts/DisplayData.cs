using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DisplayData : MonoBehaviour {

    public DataPoint dataPoint;
    public GameObject textDataPrefab;

    public float verticalSpacing = 0.2f;

    private float topOffset = 0.0f;

    void Awake()
    {
        Vector3 size = (transform.gameObject.GetComponent<Renderer>().bounds.size);
        topOffset = (0.0f - (size.y / 2.0f)) + verticalSpacing; 
    }

    void UpdateUI(DataPoint newDataPoint)
    {
        /*if (dataPoint != null)
        {
            dataPoint.SetColor(Color.red);
        }
        
        newDataPoint.SetColor(Color.green);
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        
        List<KeyValuePair<string, object>> data = newDataPoint.data.ToList();
        for (int i = 0; i < data.Count; i++)
        {
            GameObject dataPointText = Instantiate(textDataPrefab, transform.position, transform.rotation) as GameObject;
            dataPointText.transform.parent = transform;
            dataPointText.transform.Translate((Vector3.down * i * verticalSpacing) + (Vector3.down * topOffset));
            dataPointText.transform.Rotate(0.0f, 180.0f, 0.0f);
            dataPointText.GetComponent<TextDataManager>().SetTitle(data[i].Key);
            dataPointText.GetComponent<TextDataManager>().SetData(data[i].Value.ToString());
            dataPointText.name = "Data Text (" + data[i].Key + ")";
        }*/
    }

    public void SetDataPoint(DataPoint newDataPoint)
    {
        UpdateUI(newDataPoint);
        dataPoint = newDataPoint;
    }

}
