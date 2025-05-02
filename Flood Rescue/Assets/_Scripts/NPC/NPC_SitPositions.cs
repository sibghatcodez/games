using System.Collections.Generic;
using UnityEngine;

public class NPC_SitPositions : MonoBehaviour
{
    #region Singleton
    public static NPC_SitPositions Instance { get; private set; }
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
    [SerializeField] private SitPositions sitPositions;
    [SerializeField] private GameObject[] rescuedNPCs;
    [SerializeField] private InventoryData inventoryData;
    private bool IsMan1Used = false, IsMan2Used = false, IsMan3Used = false;
    private bool IsWoman1Used = false, IsWoman2Used = false;
    private bool IsKidUsed = false;

    private int positionIndex = 0;

    public void SitManager(GameObject npc_recieved)
    {
        string npc = npc_recieved.name.Split(" ")[0];
        if (npc.Equals("Man")) SitMaleNPC(npc_recieved);
        else if (npc.Equals("Woman")) SitFemaleNPC(npc_recieved);
        else if (npc.Equals("Kid")) SitKidNPC(npc_recieved);
        else if (npc.Equals("Monkey")) SitMonkeyNPC(npc_recieved);
        else if (npc.Equals("Goat")) SitGoatNPC(npc_recieved);
    }
    private void SitMaleNPC(GameObject npc)
    {
        int npc_name = int.Parse(npc.name.Split(" ")[1]);
        if (npc_name.Equals(1))
        {
            if (IsMan1Used) EnableNPC(rescuedNPCs[1]);
            else
            {
                EnableNPC(rescuedNPCs[0]);
                IsMan1Used = true;
            }
        }
        if (npc_name.Equals(2))
        {
            if (IsMan2Used) EnableNPC(rescuedNPCs[5]);
            else
            {
                EnableNPC(rescuedNPCs[4]);
                IsMan2Used = true;
            }
        }
        if (npc_name.Equals(3))
        {
            if (IsMan3Used) EnableNPC(rescuedNPCs[7]);
            else
            {
                EnableNPC(rescuedNPCs[6]);
                IsMan3Used = true;
            }
        }

        int randSoundIndex = Random.Range(0, 3);
        if (randSoundIndex.Equals(0)) AudioManager.Instance.PlayAudio(AudioName.THANKYOU_1);
        else if (randSoundIndex.Equals(1)) AudioManager.Instance.PlayAudio(AudioName.THANKYOU_2);
        else if (randSoundIndex.Equals(2)) AudioManager.Instance.PlayAudio(AudioName.THANKYOU_3);
    }
    private void SitFemaleNPC(GameObject npc)
    {
        int npc_name = int.Parse(npc.name.Split(" ")[1]);
        if (npc_name.Equals(1))
        {
            if (IsWoman1Used) EnableNPC(rescuedNPCs[3]);
            else
            {
                EnableNPC(rescuedNPCs[2]);
                IsWoman1Used = true;
            }
        }
        if (npc_name.Equals(2))
        {
            if (IsWoman2Used) EnableNPC(rescuedNPCs[11]);
            else
            {
                EnableNPC(rescuedNPCs[10]);
                IsWoman2Used = true;
            }
        }

        int randSoundIndex = Random.Range(0, 6);
        if (randSoundIndex.Equals(0)) AudioManager.Instance.PlayAudio(AudioName.THANKYOU_4);
        else if (randSoundIndex.Equals(1)) AudioManager.Instance.PlayAudio(AudioName.THANKYOU_5);
        else if (randSoundIndex.Equals(2)) AudioManager.Instance.PlayAudio(AudioName.THANKYOU_6);
        else if (randSoundIndex.Equals(3)) AudioManager.Instance.PlayAudio(AudioName.THANKYOU_7);
        else if (randSoundIndex.Equals(4)) AudioManager.Instance.PlayAudio(AudioName.THANKYOU_8);
        else if (randSoundIndex.Equals(5)) AudioManager.Instance.PlayAudio(AudioName.THANKYOU_9);
    }
    private void SitKidNPC(GameObject npc)
    {
        if (IsKidUsed) EnableNPC(rescuedNPCs[9]);
        else
        {
            EnableNPC(rescuedNPCs[8]);
            IsKidUsed = true;
        }
        AudioManager.Instance.PlayAudio(AudioName.THANKYOU_1);
    }
    private void SitMonkeyNPC(GameObject npc)
    {
        EnableNPC(rescuedNPCs[12]);
    }
    private void SitGoatNPC(GameObject npc)
    {
        EnableNPC(rescuedNPCs[13]);
    }
    private void EnableNPC(GameObject npc)
    {
        if (npc.name.Split(" ")[0].Equals("Kid")) npc.transform.localPosition = new Vector3(sitPositions.positions[positionIndex].x, 1, sitPositions.positions[positionIndex].z);
        else npc.transform.localPosition = sitPositions.positions[positionIndex];
        npc.transform.localRotation = Quaternion.Euler(sitPositions.rotations[positionIndex]);
        npc.gameObject.SetActive(true);
        positionIndex++;
    }
}