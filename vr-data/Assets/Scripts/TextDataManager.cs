using UnityEngine;
using System.Collections;

public class TextDataManager : MonoBehaviour {

    public TextMesh title;
    public TextMesh data;

    public void SetTitle(string newTitle)
    {
        title.text = newTitle;
    }

    public void SetData(string newData)
    {
        data.text = newData;
    }
}
