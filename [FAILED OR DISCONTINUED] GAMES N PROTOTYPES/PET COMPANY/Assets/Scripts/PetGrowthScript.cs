using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PetGrowthScript : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    [SerializeField] GameManagerScene02 gameManagerScript;
    [SerializeField] Camera camera;
    [SerializeField] float incrementValue = 0.01f;
    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Touch.Enable();
    }
    private void OnEnable()
    {
        inputActions.Touch.touchAction.performed += OnTouchPerformed;
    }
    void Start () {
        float pet_size = PlayerPrefs.GetFloat("PetSize", 1f);
        transform.localScale = new Vector3 (pet_size, pet_size, pet_size);
    }
    private void OnDisable()
    {
        inputActions.Touch.touchAction.performed -= OnTouchPerformed;
        inputActions.Touch.Disable();
    }

    private void OnTouchPerformed(InputAction.CallbackContext context)
    {
        Vector2 screenPosition = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag.Equals("Pet"))
            {
                Debug.Log("pet was touched");
                int levelIndex = PlayerPrefs.GetInt("level", 0);
                int currentValue = int.Parse(gameManagerScript.count[levelIndex].text);
                if (currentValue > 0) {
                    currentValue -= 1;
                    gameManagerScript.count[levelIndex].text = currentValue.ToString();
                    float x = this.transform.localScale.x, y = this.transform.localScale.y, z = this.transform.localScale.z;
                    transform.localScale = new Vector3(x + incrementValue, y + incrementValue, z + incrementValue);
                    PlayerPrefs.SetFloat("PetSize", transform.localScale.x);
                    PlayerPrefs.Save();
                    //camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z - incrementValue * 2);
                } else {
                    Debug.LogWarning("NOT ENOUGH BANANANANANAS");
                }
            }
        }
    }

    private void OnDestroy()
    {
        inputActions.Dispose();
    }
}