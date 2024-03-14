using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogue
{
    public string name;
    [TextArea]
    public string[] sentences;
}

namespace Manager
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;
        void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static void StartDialogue(Dialogue dialogue)
        {
            Debug.Log("Starting conversation with " + dialogue.name);
            // DialogueUI.Instance.dialogueName.text = dialogue.name;
            // DialogueUI.Instance.dialogueSentences.text = "";
            // StartCoroutine(TypeSentence(dialogue));
        }
    }
}

