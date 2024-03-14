using System.Collections;
using System.Collections.Generic;
using Plugins.KennethDevelops.Extensions;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using Plugins.KennethDevelops.Events;

public class DialogueUIController : MonoBehaviour
{
    private TextMeshProUGUI dialogueText;
    void Awake()
    {
        EventManager.Subscribe<OnPlayerEnterDialogue>(StartDialogue, this);
        dialogueText = GetComponentInChildren<TextMeshProUGUI>();
    }
    void OnEnable()
    {
        
    }

    void StartDialogue(OnPlayerEnterDialogue e)
    {
        Debug.Log("Starting conversation with " + e.dialogue.name);
        dialogueText.text = e.dialogue.sentences[0];
        // DialogueUI.Instance.dialogueName.text = e.dialogue.name;
        // DialogueUI.Instance.dialogueSentences.text = "";
        // StartCoroutine(TypeSentence(e.dialogue));
    }
}
