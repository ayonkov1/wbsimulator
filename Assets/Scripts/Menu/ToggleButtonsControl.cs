using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

public class ToggleButtonsControl : MonoBehaviour
{
    [SerializeField] private ToggleGroup tGroup;
    Toggle[] toggles;
    Toggle lastToggle;
    [SerializeField] private List<GameObject> contentViews = new List<GameObject>();
    GameObject lastContent;
    // Start is called before the first frame update
    void Start()
    {
        toggles = tGroup.GetComponentsInChildren<Toggle>();
        lastToggle = ReturnToggle(toggles);
        lastContent = contentViews.ElementAt(0);
        LoadContent(0);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (ToggleChanged())
        {
            int index = GetToggleIndex();
            LoadContent(index);
        }
    }
    Toggle ReturnToggle (Toggle[] toggles)
    {
        foreach (var t in toggles)
            if (t.isOn) return t;  //returns selected toggle
        return null;
    }

    bool ToggleChanged ()
    {
        Toggle current = ReturnToggle(toggles);

        if (lastToggle != current)
        {
            lastToggle = current;
            return true;
        }
          
        return false;
    }

    int GetToggleIndex ()
    {
        int index = 0;
        for (; index < toggles.Length; index++)
        {
            if (toggles[index] == lastToggle)
            {
                break;
            }
        }
        //print("Index " + index);

        return index;
        //contentViews.ElementAt(index);
    }

    void LoadContent(int index)
    {
        GameObject newContent = contentViews.ElementAt(index);
        lastContent.SetActive(false);
        newContent.SetActive(true);
        lastContent = newContent;



    }

}
