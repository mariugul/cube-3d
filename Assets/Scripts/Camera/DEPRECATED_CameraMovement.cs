
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Camera speed multiplicators
    float speedH = 2.0f;
    float speedV = 2.0f;
    float speedR = 2.0f;

    // Rotation variables
    private float yawDefault;
    private float pitchDefault;
    private float rollDefault;

    private float yaw;
    private float pitch;
    private float roll;

    // Position variables
    private float xDefault;
    private float yDefault;
    private float zDefault;
    
    private float x;
    private float y;
    private float z;

    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        // Save default rotation values for camera reset
        pitchDefault = transform.eulerAngles.x;
        yawDefault   = transform.eulerAngles.y;
        rollDefault  = transform.eulerAngles.z;

        // Save default position values for camera reset
        xDefault = transform.position.x;
        yDefault = transform.position.y;
        zDefault = transform.position.z;

        // Set camera start rotation
        pitch = pitchDefault;
        yaw   = yawDefault;
        roll  = rollDefault;

        // Set camera start position
        x = xDefault; 
        y = yDefault;
        z = zDefault;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        //gameObject.transform.LookAt(target.position);

        // Mouse right click
        if (Input.GetMouseButton(1))
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
        }

        // Mouse middle click
        else if (Input.GetMouseButton(2))
        {
            roll += speedR * Input.GetAxis("Mouse X");
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
            Camera.main.transform.position = new Vector3(CamX + X, CamY + Y, CamZ + Z);
        }
        
        // Move camera with mouse keys
        else if (Input.GetKeyDown(KeyCode.W))
            pitch -= 5;
        else if (Input.GetKeyDown(KeyCode.A))
            yaw -= 5;
        else if (Input.GetKeyDown(KeyCode.S))
            pitch += 5;
        else if (Input.GetKeyDown(KeyCode.D))
            yaw += 5;

        // Reset Camera Position
        else if (Input.GetKeyDown(KeyCode.R))
        {
            // Reset position variables
            x = xDefault;
            y = yDefault;
            z = zDefault;

            // Reset rotation variables
            yaw   = yawDefault;
            pitch = pitchDefault;
            roll  = rollDefault;

            // Reset camera position
            Camera.main.transform.position = new Vector3(xDefault, yDefault, zDefault);
        }

        // Update camera rotation
        transform.eulerAngles = new Vector3(pitch, yaw, roll);
    }
}
