using System;
using UnityEngine;

public enum NPC_State
{
    GROW,
    IDLE,
    SHRINK
}
public class NPC_GrowShrink : MonoBehaviour
{
    private float progress = 0;
    private float time = 1; //the time it takes to grow/shrink
    private Vector3 growSize = new Vector3(3, 3, 3);
    private Vector3 shrinkSize = Vector3.zero;

    public NPC_State state = NPC_State.GROW;
    private void FixedUpdate()
    {
        if (progress < time && state.Equals(NPC_State.GROW))
        {
            progress += Time.deltaTime;
            transform.localScale = Vector3.Lerp(shrinkSize, growSize, progress / time);
        }
        if (progress < time && state.Equals(NPC_State.SHRINK))
        {
            progress += Time.deltaTime;
            transform.localScale = Vector3.Lerp(growSize, shrinkSize, progress / time);
        }
        if (progress.Equals(time) && state.Equals(NPC_State.GROW))
        {
            state = NPC_State.IDLE;
            progress = 0;
        }
        else if (progress.Equals(time) && state.Equals(NPC_State.SHRINK))
        {
            state = NPC_State.IDLE;
            progress = 0;
            gameObject.SetActive(false);
        }
    }
    public void ShrinkAndDeactivate()
    {
        state = NPC_State.SHRINK; progress = 0;
        Invoke("Deactive", time);
    }
    private void Deactive() => gameObject.SetActive(false);
    private void OnDisable()
    {
        progress = 0;
    }
}