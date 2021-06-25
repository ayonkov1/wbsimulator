using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public GameObject[] selectorArr;
    public GameObject finalInfo;
    public GameObject selector;

    public void ShowInfo()
    {
        finalInfo.SetActive(true);
    }

    public void DeactivateButtons()
    {
        foreach (GameObject gameObject in selectorArr)
        {
            gameObject.SetActive(false);
        }
    }
}
