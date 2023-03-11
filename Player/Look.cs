using UnityEngine;

public class Look : MonoBehaviour
{

    public float Sensitivity = 100f;
    public Transform Player;
    private float RotateX;

    // Start is called before the first frame update
    void Start()
    {
        //The cusor is locked in the middles
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets the movements of the mouse through the X and Y
        float MouseUpY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;
        float MouseAcrossX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        //Allows the user to look up and but no more than 90 degrees each way
        RotateX -= MouseUpY;
        RotateX = Mathf.Clamp(RotateX, -90f, 90f);

        //Moves the camera
        transform.localRotation = Quaternion.Euler(RotateX, 0f, 0f);
        Player.Rotate(Vector3.up * MouseAcrossX);
    }
    public void MouseRelease()
    {
        //the mouse is released and no longer locked to allow for the menu to be accessed
        Cursor.lockState = CursorLockMode.None;
    }
}

