using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogue
{
    public string[] text;
    public string name;
}
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Sprite sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
        if (sprite == null)
        {
            Debug.LogError("Sprite is null");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            Debug.Log("PlayerAttack");
            DialogueManager.Instance.StartDialogue(dialogue, sprite);
        }
    }

    public void Test()
    {
        //DialogueManager.Instance.ClosePanel();
        DialogueManager.Instance.StartDialogue(dialogue, sprite);
    }
}




