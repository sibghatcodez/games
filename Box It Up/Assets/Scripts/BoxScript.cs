using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class BoxScript : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Camera camera;
    [SerializeField] MoveMechanic holder;
    [SerializeField] PlayableDirector timeline;
    [SerializeField] Transform handle, parent;
    [SerializeField] GameObject Cubes;
    [SerializeField] GameManager gameManager;
    public bool isBoxReleased = false;
    public bool hasBoxCollided = false;

    private InputSystem_Actions inputActions;
    private InputAction controls;

    GameObject clonedBox;
    bool canInstantiate = true;
    float moveDuration = .5f;
    float elapsedTime = 0f;
    bool isGameOver = false;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        controls = inputActions.Box.MobileTap;

        inputActions.Box.MobileTap.performed += HandleTouch;
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        // Disable the input when the script is disabled
        inputActions.Disable();
    }
    private void HandleTouch(InputAction.CallbackContext context)
    {
        gameManager.TapSound();
        InstantiateBox();
        Invoke("TouchTimer", 2f);
    }
    void TouchTimer()
    {
        canInstantiate = true;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        if (transform.tag.Equals("CagedBox") && !isBoxReleased && canInstantiate && rb.useGravity == false)
        {
            transform.position = handle.position;
        }


        // Trigger gravity change if rotation exceeds limits
        float zRotation = transform.eulerAngles.z;
        if (zRotation > 180) zRotation -= 360;
        // Check if the rotation is greater than 25 degrees or less than -25 or -40 degrees
        if (Mathf.Floor(zRotation) > 3f || Mathf.Floor(zRotation) < -3f)
        {
            ChangeGravity(true);
            isGameOver = true;
            Debug.Log("hey");
            Invoke("GameEnd", 1f);
        }
    }
    void GameEnd()
    {
        gameManager.IsPlayerDead = true;

    }
void InstantiateBox()
{
    if (!gameManager.IsPlayerDead && canInstantiate)
    {
        gameManager.score++;

        clonedBox = Instantiate(gameObject, handle.position, handle.rotation, parent);
        clonedBox.SetActive(false);
        clonedBox.transform.localScale = new Vector3(0.09f, 0.09f, 0.09f);

        rb.useGravity = true;
        isBoxReleased = true;
        transform.parent = Cubes.transform;
        timeline.Play();
        canInstantiate = false;
    }
}




    void ChangeGravity(bool param)
    {
        foreach (var item in Cubes.GetComponentsInChildren<Rigidbody>())
        {
            item.useGravity = true;
            item.constraints = RigidbodyConstraints.None;
        }
    }

    void CreateCagedBox()
    {
        if (clonedBox != null) clonedBox.SetActive(true);
        timeline.Stop();
        transform.tag = "ReleasedBox";
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("ReleasedBox") && !hasBoxCollided)
        {
            //ChangeGravity(false);
            HandleBoxCollision();
        }
        else if (col.collider.CompareTag("Ground"))
        {
            gameManager.IsPlayerDead = true;
        }
        if (col.gameObject.CompareTag("ReleasedBox") && gameObject.CompareTag("ReleasedBox") && isGameOver)
        {
            gameManager.Signal("bounce", true);
        }
    }
    void HandleBoxCollision()
    {
        gameManager.Signal("bounce", false);
        transform.tag = "ReleasedBox";
        Invoke("UnfreezeRotation", .1f);
        hasBoxCollided = true;
        Invoke("CreateCagedBox", .5f);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        holder.yAxisRange += 10f;
        holder.yStartAxis += 10f;
        Invoke("StopGravity", 1f);
        ChangeCameraPosition();
    }

    void UnfreezeRotation()
    {
        rb.constraints = RigidbodyConstraints.None;
        Invoke("freezeY", .5f);
    }
    void freezeY()
    {
        rb.constraints = RigidbodyConstraints.FreezePosition;
    }

    void StopGravity()
    {
        rb.useGravity = false;
    }

    void ChangeCameraPosition()
    {
        Vector3 targetCameraPosition = new Vector3(camera.transform.position.x, transform.position.y + 10f, camera.transform.position.z);
        Vector3 targetBackgroundPosition = new Vector3(gameManager.background.transform.position.x, gameManager.background.transform.position.y + 10f, gameManager.background.transform.position.z);
        StartCoroutine(SmoothMove(camera.transform.position, targetCameraPosition, gameManager.background.transform.position, targetBackgroundPosition));
    }

    IEnumerator SmoothMove(Vector3 startCamera, Vector3 targetCamera, Vector3 startBackground, Vector3 targetBackground)
    {
        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            camera.transform.position = Vector3.Lerp(startCamera, targetCamera, t);
            gameManager.background.transform.position = Vector3.Lerp(startBackground, targetBackground, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        camera.transform.position = targetCamera;
        gameManager.background.transform.position = targetBackground;
        elapsedTime = 0f;
    }
}