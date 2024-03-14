using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using Plugins.KennethDevelops.Events;

public class NPC1Dialogue : MonoBehaviour
{
    public Dialogue dialogue;
    private PlayerEvent playerEvent;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            Debug.Log("Player entered dialogue");
            PlayerEvent.Instance.onPlayerEnterDialogue.dialogue = dialogue;
            PlayerEvent.Instance.onPlayerEnterDialogue.Trigger(this);
        }
    }
}
