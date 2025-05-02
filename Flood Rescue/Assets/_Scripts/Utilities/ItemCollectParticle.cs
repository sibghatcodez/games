using UnityEngine;

public enum Particles
{
    BEACH_BALL,
    UMBRELLA,
    PLASTIC_BOTTLE,
    TIN,
}
public class ItemCollectParticle : MonoBehaviour
{
    #region Singleton
    public static ItemCollectParticle Instance { get; private set; }
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
    [SerializeField] private GameObject ItemPickParticle;
    [SerializeField] private GameObject beachBall, umbrella, plasticBottle, tin;

    public void ShowParticle(Particles ps)
    {
        ItemPickParticle.SetActive(true);
        AudioManager.Instance.PlayAudio(AudioName.ITEM_PICKUP);

        if (ps.Equals(Particles.BEACH_BALL)) beachBall.SetActive(true);
        else if (ps.Equals(Particles.UMBRELLA)) umbrella.SetActive(true);
        else if (ps.Equals(Particles.PLASTIC_BOTTLE)) plasticBottle.SetActive(true);
        else if (ps.Equals(Particles.TIN)) tin.SetActive(true);

        Invoke("DisableAll", 2.5f);
    }

    private void DisableAll()
    {
        beachBall.SetActive(false);
        umbrella.SetActive(false);
        plasticBottle.SetActive(false);
        tin.SetActive(false);
        ItemPickParticle.SetActive(false);
    }
}