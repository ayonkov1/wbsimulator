using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
    
    [SerializeField] private float moveAmount = 15f;
    [SerializeField] private float zoomChangeAmount = 20f;
    [SerializeField] private float zoomMax = 20f;
    [SerializeField] private float zoomMin = 10f;
    [SerializeField] private Tilemap Tilemap;
    [SerializeField] private float zoom = 6f;
    [SerializeField] private float edgeSize = 5f;
    
    private Vector3 cameraFollowPosition;
 
    private Vector3 tilemapMinPoints;
    private Vector3 tilemapMaxPoints;
    private Camera myCamera;



    // Set up is in Awake instead Start because we will disable the scipt during the Dialogue but I want the measures of the camera
    void Awake()
    {
        myCamera = transform.GetComponent<Camera>();
        cameraFollowPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        transform.position = cameraFollowPosition;

        GetTilemapSize();
        //called to get the current zoom 
        HandleZoom();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleZoom();
        CameraClamp();

    }


    private void HandleMovement()
    {
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y > Screen.height - edgeSize)
        {
            cameraFollowPosition.y += moveAmount * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y < edgeSize)
        {
            cameraFollowPosition.y -= moveAmount * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x < edgeSize)
        {
            cameraFollowPosition.x -= moveAmount * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x > Screen.width - edgeSize)
        {
            cameraFollowPosition.x += moveAmount * Time.deltaTime;
        }

       
    }

    private void FixedUpdate()
    {
        transform.position = cameraFollowPosition;
    }

    //Resizes the orthographic size of the camera to change the zoom
    private void HandleZoom()
    {
        // if I do it with also + and minus keys it needs to multiply the speed
        if (Input.mouseScrollDelta.y > 0)
        {
            zoom -= zoomChangeAmount * Time.deltaTime;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            zoom += zoomChangeAmount * Time.deltaTime;
        }
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);

        myCamera.orthographicSize = zoom;

    }


    // Gets the extents of the camera
    //private void GetCameraBounds ()
    //{
    //    var vertExtent = myCamera.orthographicSize;
    //    var horzExtent = vertExtent * myCamera.aspect;

    //    Debug.Log("Camera h: " + vertExtent + "Camera w: " + horzExtent);
    //}

    // Clamps the camera so it doesn't get bigger than the tilemap
    private void CameraClamp ()
    {
        //camera extents
        var vertExtent = myCamera.orthographicSize;
        var horzExtent = vertExtent * myCamera.aspect;

        cameraFollowPosition.x = Mathf.Clamp(cameraFollowPosition.x, tilemapMinPoints.x + horzExtent, tilemapMaxPoints.x - horzExtent);
        cameraFollowPosition.y = Mathf.Clamp(cameraFollowPosition.y, tilemapMinPoints.y + vertExtent, tilemapMaxPoints.y - vertExtent);

    }

    //Gets the local bounds of the tilemap
    private void GetTilemapSize ()
    {
        Bounds lBounds = Tilemap.localBounds;
        tilemapMaxPoints = lBounds.max - new Vector3(1f, 1f, 0);
        tilemapMinPoints = lBounds.min + new Vector3(1f, 1f, 0);

        //adjusts the zoomMax from the bounds size
        zoomMax = lBounds.size.y / 4f;
    }
}
