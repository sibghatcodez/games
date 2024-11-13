using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float sprintSpeed = 2f;

    [SerializeField] float jumpForce = 2.5f;
    [SerializeField] SoundManager soundManager;

    [SerializeField] Rigidbody rigidbody;
    [SerializeField] GameObject panel;
    Animator animator;
    bool IsGrounded = true;
    public bool IsPlayerDead = false;
    public bool IsGamePaused = false;
    public bool countdownStarted = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (!IsPlayerDead && !countdownStarted && !IsGamePaused)
        {
            MovePlayer();
            Jump();
        }
    }
    private void MovePlayer()
    {
        Vector3 pos = transform.position;
        bool isMoving = false;
        bool isSprinting = false;
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        int checkpoint = PlayerPrefs.GetInt("checkpoint", 0);
        if (checkpoint.Equals(0) && currentSceneIndex.Equals(1) && transform.position.z > 10 && transform.position.z < 105) panel.SetActive(true);
        else if (checkpoint.Equals(1) && currentSceneIndex.Equals(1) && transform.position.z > 105 ) panel.SetActive(true);
        else if( transform.position.z < 10 ) panel.SetActive(false);

        if (!transform.gameObject.CompareTag("Ground") || !transform.gameObject.CompareTag("Checkpoint"))
        {

            if (Input.GetKey(KeyCode.W) && currentSceneIndex.Equals(0))
            {
                pos += transform.forward * (movementSpeed + (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : 0)) * Time.deltaTime;
                isMoving = !Input.GetKey(KeyCode.LeftShift);
                isSprinting = Input.GetKey(KeyCode.LeftShift); // Check if sprinting
            }
            else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex.Equals(1) && !IsPlayerDead)
            {
                isSprinting = true;
                isMoving = false;
                pos += transform.forward * (movementSpeed + sprintSpeed) * Time.deltaTime;
            }
            if (animator != null)
            {
                animator.SetBool("Walk", isMoving);
                animator.SetBool("Sprint", isSprinting);
                animator.SetBool("Idle", !isMoving && !isSprinting);
            }
            transform.position = pos;
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            IsGrounded = false;
            if (currentSceneIndex.Equals(1)) soundManager.PlaySounds("jump");
            if (animator != null)
            {
                animator.SetTrigger("Jump");
                animator.SetBool("Idle", false);
                animator.SetBool("Sprint", false);
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;

            if (animator != null)
            {
                animator.SetBool("Idle", true);
                animator.ResetTrigger("Jump");
                animator.SetBool("Sprint", false);
            }
        }
    }
}
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerMovement : MonoBehaviour
// {
//     [SerializeField] float movementSpeed = 1f;
//     [SerializeField] float sprintSpeed = 2f;

//     [SerializeField] float jumpForce = 2.5f;
//     [SerializeField] SoundManager soundManager;

//     [SerializeField] Rigidbody rigidbody;
//     Animator animator;
//     bool IsGrounded = true;
//     public bool IsPlayerDead = false;
//     public bool IsGamePaused = false;
//     public bool countdownStarted = false;

//     private Vector2 startTouchPosition;
//     private Vector2 currentTouchPosition;
//     private bool isSwipeUp;

//     void Start()
//     {
//         animator = GetComponent<Animator>();
//     }

//     void Update()
//     {
//         if (!IsPlayerDead && !countdownStarted && !IsGamePaused)
//         {
//             HandleTouchInput();
//             MovePlayer();
//         }
//     }

//     private void HandleTouchInput()
//     {
//         if (Input.touchCount > 0)
//         {
//             Touch touch = Input.GetTouch(0);

//             switch (touch.phase)
//             {
//                 case TouchPhase.Began:
//                     startTouchPosition = touch.position;
//                     isSwipeUp = false;
//                     break;

//                 case TouchPhase.Moved:
//                     currentTouchPosition = touch.position;
//                     Vector2 distance = currentTouchPosition - startTouchPosition;

//                     // Detect swipe up for movement and sprint
//                     if (distance.y > 50 && Mathf.Abs(distance.x) < 100) // Vertical swipe
//                     {
//                         isSwipeUp = true;
//                     }
//                     break;

//                 case TouchPhase.Ended:
//                     // Single tap for jump
//                     if (!isSwipeUp && IsGrounded)
//                     {
//                         Jump();
//                     }
//                     break;
//             }
//         }
//     }

//     private void MovePlayer()
//     {
//         Vector3 pos = transform.position;
//         bool isMoving = false;
//         bool isSprinting = false;

//         int checkpoint = PlayerPrefs.GetInt("checkpoint", 0);
//         int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

//         // Swipe up to move forward
//         if (isSwipeUp && checkpoint.Equals(0) && currentSceneIndex.Equals(1))
//         {
//             float speed = (Input.touchCount == 2) ? sprintSpeed : movementSpeed; // Sprint if two fingers swipe up
//             pos += transform.forward * speed * Time.deltaTime;
//             isMoving = Input.touchCount < 2;
//             isSprinting = Input.touchCount == 2;
//         }
//         else if (checkpoint.Equals(1) && currentSceneIndex.Equals(1))
//         {
//             pos += transform.forward * (movementSpeed + sprintSpeed) * Time.deltaTime;
//             isSprinting = true;
//             isMoving = false;
//         }

//         // Update animator
//         if (animator != null)
//         {
//             animator.SetBool("Walk", isMoving);
//             animator.SetBool("Sprint", isSprinting);
//             animator.SetBool("Idle", !isMoving && !isSprinting);
//         }

//         transform.position = pos;
//     }

//     private void Jump()
//     {
//         rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//         IsGrounded = false;
// //        soundManager.PlaySounds("jump");
//         if (animator != null)
//         {
//             animator.SetTrigger("Jump");
//             animator.SetBool("Idle", false);
//             animator.SetBool("Sprint", false);
//         }
//     }

//     void OnCollisionEnter(Collision col)
//     {
//         if (col.gameObject.CompareTag("Ground"))
//         {
//             IsGrounded = true;

//             if (animator != null)
//             {
//                 animator.SetBool("Idle", true);
//                 animator.ResetTrigger("Jump");
//                 animator.SetBool("Sprint", false);
//             }
//         }
//     }
// }
