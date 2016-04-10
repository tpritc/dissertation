using UnityEngine;
using System.Collections.Generic;

public class DataPoint : MonoBehaviour {

    public GameObject dataPanelPrefab;
    public Dictionary<string, object> data;

    private GameObject dataPanelObject;
    private DataPointPanel dataPanel;
    private bool dataPanelIsSetUp = false;
	
	void Update () {
        //if (!dataPanelIsSetUp) { SetUpPanel(); }
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
