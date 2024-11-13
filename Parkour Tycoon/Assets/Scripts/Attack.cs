using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject Player, GameOverUI;
    [SerializeField] SoundManager soundManager;
    [SerializeField] GameManager GameManager;
    [SerializeField] TrainScript trainScript;

    Rigidbody playerRigidbody;
    Animator playerAnimator;
    PlayerMovement playerMovement;
    [SerializeField] BoxCollider[] bx;
    public bool isPlayerDead = false;

    void Start()
    {
        playerRigidbody = Player.GetComponent<Rigidbody>();
        playerAnimator = Player.GetComponent<Animator>();
        playerMovement = Player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (isPlayerDead) trainScript.IsGameOver = true;
        if (GameOverUI.activeInHierarchy)
        {
            GameManager.isGameOver = true;
        }
        else
        {
            GameManager.isGameOver = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!SceneManager.GetActiveScene().name.Equals("Lobby") && collision.collider.CompareTag("Player") && !isPlayerDead)
        {
            GameOverUI.SetActive(true);
            isPlayerDead = true;
            playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;
            playerAnimator.enabled = false;
            playerMovement.IsPlayerDead = true;
            soundManager.PlaySounds("death");
            foreach (var item in bx) item.enabled = false;
        }
    }
}