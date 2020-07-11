using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private Vector3 previousPosition;

    void Update()
    {
        // Mouse Right Click
        if (Input.GetMouseButtonDown(1))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        // Mouse Right Click
        if (Input.GetMouseButton(1))
        {
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            cam.transform.position = new Vector3();
            
            cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            cam.transform.Rotate(new Vector3(0, 1, 0), (-direction.x * 180), Space.World);
            
            cam.transform.Translate(new Vector3(15, 20, -100));

            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        // Mouse wheel zoom
        else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float ScrollWheelChange = Input.GetAxis("Mouse ScrollWheel");       //This little peece of code is written by JelleWho https://github.com/jellewie
            float R = ScrollWheelChange * 15;                                   //The radius from current camera
            float PosX = Camera.main.transform.eulerAngles.x + 90;              //Get up and down
            float PosY = -1 * (Camera.main.transform.eulerAngles.y - 90);       //Get left to right

            //Convert from degrees to radians
            PosX = PosX / 180 * Mathf.PI;
            PosY = PosY / 180 * Mathf.PI;
            
            //Calculate new coords
            float X = R * Mathf.Sin(PosX) * Mathf.Cos(PosY);
            float Z = R * Mathf.Sin(PosX) * Mathf.Sin(PosY);
            float Y = R * Mathf.Cos(PosX);

            //Get current camera postition for the offset
            float CamX = Camera.main.transform.position.x;
            float CamY = Camera.main.transform.position.y;
            float CamZ = Camera.main.transform.position.z;

            // Zoom the main camera
            Camera.main.transform.position = new Vector3(CamX + X, CamY, CamZ + Z);
            
        }
    }
}
