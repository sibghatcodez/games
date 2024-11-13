using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraLook : MonoBehaviour
{
    [SerializeField] GameObject playerBody; // Assign this to the player body (not the camera)
    [SerializeField] float mouseSensitivity = 200f;

    private float xRotation = 0f;
    private float yRotation = 0f; // Track the player's y-axis rotation

    void Start()
    {
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the camera up and down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -15f, 15f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Calculate and clamp y-axis rotation
        yRotation += mouseX;
        if(SceneManager.GetActiveScene().buildIndex.Equals(1)) yRotation = Mathf.Clamp(yRotation, -45f, 45f);
        playerBody.transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}

// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class CameraLook : MonoBehaviour
// {
//     [SerializeField] GameObject playerBody; // Assign this to the player body (not the camera)
//     [SerializeField] float touchSensitivity = 0.2f; // Adjust for touch sensitivity

//     private float xRotation = 0f;
//     private float yRotation = 0f; // Track the player's y-axis rotation
//     private Vector2 lastTouchPosition; // Store the last touch position

//     void Update()
//     {
//         // Check if there is a touch on the screen
//         if (Input.touchCount > 0)
//         {
//             Touch touch = Input.GetTouch(0); // Get the first touch

//             if (touch.phase == TouchPhase.Moved)
//             {
//                 // Calculate the touch delta (movement) and apply sensitivity
//                 float touchX = touch.deltaPosition.x * touchSensitivity;
//                 float touchY = touch.deltaPosition.y * touchSensitivity;

//                 // Rotate the camera up and down
//                 xRotation -= touchY;
//                 xRotation = Mathf.Clamp(xRotation, -15f, 15f);
//                 transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

//                 // Calculate and clamp y-axis rotation
//                 yRotation += touchX;
//                 if (SceneManager.GetActiveScene().buildIndex == 1)
//                     yRotation = Mathf.Clamp(yRotation, -45f, 45f);
                
//                 playerBody.transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
//             }
//         }
//     }
// }