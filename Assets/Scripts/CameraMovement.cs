using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float xSpeed;
    public float ySpeed;

    public Transform angleFace;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //mouseN = Mouse Position s.t Position/Time * Time = Position
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSpeed;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySpeed;

        yRotation += mouseX;
        xRotation -= mouseY;
        
        //Clamp up and down to 90deg
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Calc done, do the rotations
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        angleFace.rotation = Quaternion.Euler(0, yRotation, 0);
    }

}
