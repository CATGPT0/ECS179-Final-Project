using System.Collections;
using System.Collections.Generic;
using Plugins.KennethDevelops.Extensions;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using Plugins.KennethDevelops.Events;
using System.Xml;
using SchemaEditor.Internal;

public class DialogueUIController : MonoBehaviour
{
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Button exitButton;
    private TextMeshProUGUI dialogueText;
    public Dialogue dialogue;
    private int currentSentence;
    void Awake()
    {
        dialogueText = GetComponentInChildren<TextMeshProUGUI>();
        exitButton.onClick.AddListener(() => PlayerEvent.Instance.onPlayerExitDialogue.Trigger(this));
        EventManager.Subscribe<OnPlayerEnterDialogue>(StartDialogue, this);
        EventManager.Subscribe<OnPlayerExitDialogue>(QuitDialogue, this);
    }

    void Start()
    {
        dialogueText.text = "";
        dialogue = null;
        currentSentence = 0;
    }
    void OnEnable()
    {
        
    }

    void Update()
    {
        //if (dialogue == null) Debug.LogError("Dialogue is null in update");
        //else Debug.Log("Dialogue is not null in update");
    }

    void StartDialogue(OnPlayerEnterDialogue e)
    {
        dialogue = new Dialogue(e.dialogue);
        if (dialogue == null) Debug.LogError("Dialogue is null");
        Debug.Log("Start dialogue");
        currentSentence = 0;
        dialogueText.text = dialogue.sentences[currentSentence];
    }

    public void ContinueDialogue()
    {
        if (dialogue == null) Debug.LogError("Dialogue is null!!!");
        if (currentSentence < dialogue.sentences.Length - 1)
        {
            currentSentence++;
            dialogueText.text = dialogue.sentences[currentSentence];
        }
        else
        {
            PlayerEvent.Instance.onPlayerExitDialogue.Trigger(this);
        }
    }

    void QuitDialogue(OnPlayerExitDialogue e)
    {
        dialogueText.text = "";
        dialogue = null;
        currentSentence = 0;
    }
}
