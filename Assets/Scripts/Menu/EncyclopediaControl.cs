using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncyclopediaControl : MonoBehaviour
{
    [SerializeField] private List<GameObject> selectors = new List<GameObject>();

    private Camera mainCamera;
    private bool isOpen = false;
   
    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;
        CloseEncyclopedia();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseEncyclopedia ()
    {

        mainCamera.GetComponent<CameraFollow>().enabled = true;
        gameObject.SetActive(false);
        SetSelecotrsToState(true);
        isOpen = false;
    }
    public void OpenEncyclopedia ()
    {
        if (isOpen) {
            CloseEncyclopedia();
            return;
        }

        mainCamera.GetComponent<CameraFollow>().enabled = false;
        gameObject.SetActive(true);
        SetSelecotrsToState(false);
        isOpen = true;
    }

    void SetSelecotrsToState(bool state)
    {
        foreach (GameObject selector in selectors)
        {
            selector.SetActive(state);
        }
    }


}
