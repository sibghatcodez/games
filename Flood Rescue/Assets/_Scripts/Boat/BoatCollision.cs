using TMPro;
using UnityEngine;

public class BoatCollision : MonoBehaviour
{
    #region Singleton
    public static BoatCollision Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion
    [SerializeField] private BoatHealth boatHealth;
    [SerializeField] private Rigidbody boatRididbody;
    [SerializeField] private ParticleSystem collisionParticle;
    public bool IsBoatColliding { get; private set; } = false;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacles") || collision.collider.CompareTag("FloatingStuff") && !UIManager.Instance.currentUIState.Equals(UIState.GAMEWON))
        {
            boatHealth.TakeDamage(boatRididbody.linearVelocity.magnitude * 3);
            Vector3 collisionPosition = collision.contacts[0].point;
            collisionParticle.transform.position = collisionPosition;
            collisionParticle.Play();
            AudioManager.Instance.PlayAudio(AudioName.COLLISION);
            IsBoatColliding = true;
        }
        else IsBoatColliding = false;
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacles") || collision.collider.CompareTag("FloatingStuff"))
        {
            IsBoatColliding = false;
        }
    }
}