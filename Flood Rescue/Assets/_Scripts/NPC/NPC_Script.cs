using UnityEngine;

public class NPC_Script : MonoBehaviour
{
    [SerializeField] private Transform HelpParticle;
    [SerializeField] private Transform Timer;
    [SerializeField] private NPC_Timer npcTimerScript;
    [SerializeField] private Rigidbody boatRigidbody;
    [SerializeField] private NPC_GrowShrink npc_GrowShrink;
    private void Update()
    {
        if (!npc_GrowShrink.state.Equals(NPC_State.SHRINK)) CheckRescueProximity();
    }


    private void CheckRescueProximity()
    {
        if (GameManager.Instance.GameTime > 3f &&
        Vector3.Distance(transform.position, GameManager.Instance.boat.transform.position) <= Objective.Instance.givenBoatDistance &&
        !(Objective.Instance.peopleRescued >= Objective.Instance.peopleToRescue))
        {
            npcTimerScript.StartTimer(Objective.Instance.givenRescueTime);

            if (transform.childCount >= 2)
            {
                HelpParticle.gameObject.SetActive(true);
                Timer.gameObject.SetActive(true);
            }

            if (Vector3.Distance(transform.position, GameManager.Instance.boat.transform.position) <= GameManager.Instance.gameData.PickupDistance && boatRigidbody.linearVelocity.magnitude < 15)
            {
                UpdateRescueBar();
            }
            else DeactiveRescueBar();
        }
    }
    private void UpdateRescueBar()
    {
        if (!NPC_Rescue.Instance.IsRescuing)
        {
            NPC_Rescue.Instance.NPC = transform;
            NPC_Rescue.Instance.IsRescuing = true;
        }
    }
    private void DeactiveRescueBar()
    {
        if (NPC_Rescue.Instance.NPC == this)
        {
            NPC_Rescue.Instance.NPC = null;
            NPC_Rescue.Instance.IsRescuing = false;
        }
    }
}