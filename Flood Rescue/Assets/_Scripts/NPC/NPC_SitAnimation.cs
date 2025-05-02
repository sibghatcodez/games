using UnityEngine;

public class NPC_SitAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start() => PlayRandomAnim();
    private void PlayRandomAnim()
    {
        int randInt = Random.Range(1, 5);
        animator.SetInteger("clip", randInt);
    }
}
