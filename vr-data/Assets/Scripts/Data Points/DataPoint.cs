using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DataPoint : MonoBehaviour {

    public GameObject dataPanelPrefab;
    public Dictionary<string, object> data;

    private GameObject dataPanelObject;
    private DataPointPanel dataPanel;
    private bool dataPanelIsSetUp = false;
	
	void Start () {

        switch (data["Severity"].ToString()) {
            case "Slight":
                gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            case "Serious":
                gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                break;
            case "Fatal":
                gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            default:
                break;
        }
	}

    void SetUpPanel()
    {
        dataPanelObject = Instantiate(dataPanelPrefab, transform.position, transform.rotation) as GameObject;
        dataPanel = dataPanelObject.GetComponent<DataPointPanel>();
        dataPanel.SetUpPanelWithNewDataPoint(this);
        dataPanelIsSetUp = true;
    }

    void TearDownPanel()
    {
        Destroy(dataPanelObject);
        dataPanel = null;
        dataPanelIsSetUp = false;
    }

    public void TogglePanel() {
        if (dataPanelIsSetUp) {
            TearDownPanel();
        } else {
            SetUpPanel();
        }
    }
}
