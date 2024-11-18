using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    [SerializeField] GameManager gameManagerScript;

    private void Awake()
    {
        // Instantiate the InputSystem_Actions class
        inputActions = new InputSystem_Actions();

        // Enable the action map that contains the action you want to listen to
        inputActions.Touch.Enable(); // Ensure the correct action map name is used
    }

    private void OnEnable()
    {
        // Register to the specific action
        inputActions.Touch.touchAction.performed += OnTouchPerformed; // Ensure correct action name
    }

    private void OnDisable()
    {
        // Unregister from the action to prevent memory leaks
        inputActions.Touch.touchAction.performed -= OnTouchPerformed;

        // Disable the action map
        inputActions.Touch.Disable();
    }

    private void OnTouchPerformed(InputAction.CallbackContext context)
    {

        // Get the screen position of the mouse
        Vector2 screenPosition = Mouse.current.position.ReadValue();

        // Convert screen position to world position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane));

        // Perform a raycast to detect what object was clicked
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // If the raycast hits an object, log the object's name
            //Debug.Log("You clicked on: " + hit.collider.gameObject.name);
            if(gameManagerScript.bagCapacityLeft > 0 && gameManagerScript.gameStatus && !hit.collider.tag.Equals("Untagged")) {
                gameManagerScript.IncrementItem(hit.collider.tag);
                Destroy(hit.collider.gameObject);
            }
            if(gameManagerScript.bagCapacityLeft == 0) gameManagerScript.bagCapacityUI.color = Color.red;
            if(gameManagerScript.itemsPicked[gameManagerScript.level] == gameManagerScript.itemsToBePicked) gameManagerScript.pickedItemsUI.color = Color.green;
        }
        else
        {
            // If no object is hit, log that no object was hit
            Debug.Log("No object was hit.");
        }
    }

    private void OnDestroy()
    {
        // Dispose of the input actions instance to free up resources
        inputActions.Dispose();
    }
}
