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

    public Dialogue(Dialogue dialogue)
    {
        name = dialogue.name;
        sentences = dialogue.sentences;
    }
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
    }
}

