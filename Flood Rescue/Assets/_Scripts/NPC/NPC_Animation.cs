using UnityEngine;

public class NPC_Animation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start() => PlayRandomAnim();
    private void PlayRandomAnim()
    {
        int randInt = Random.Range(1, 9);
        animator.SetInteger("clip", randInt);
    }
}