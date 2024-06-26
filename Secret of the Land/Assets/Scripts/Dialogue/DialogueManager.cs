using System.Collections;
using System.Collections.Generic;
using CardBattle;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    [SerializeField]
    private TextMeshProUGUI NPCname;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private Image image;
    public Dialogue dialogue;
    public Image border;
    public NPCCardEntryController npcCardEntryController;
    private int activeMessage = 0;

    void Awake()
    {
        Instance = this;
        border = GetComponent<Image>();
    }

    void Start()
    {
        if (border == null)
        {
            Debug.LogError("Border is null");
        }
        if (NPCname == null)
        {
            Debug.LogError("NPCname is null");
        }
        if (dialogue == null)
        {
            Debug.LogError("dialogue is null");
        }
        ClosePanel();
    }

    public void StartDialogue(Dialogue dialogue, Sprite sprite)
    {
        PlayerEvent.Instance.onPlayerEnterDialogue.Invoke();
        OpenPanel();
        this.dialogue = dialogue;
        if (image == null)
        {
            Debug.LogError("Image is null");
        }
        image.sprite = sprite;
        activeMessage = 0;
        ShowMessage();
    }

    public void ShowMessage()
    {
        NPCname.text = dialogue.name;
        if (activeMessage >= dialogue.text.Length)
        {
            ClosePanel();
            return;
        }
        text.text = dialogue.text[activeMessage];
        activeMessage++;
    }

    public void EnterCardScene()
    {
        npcCardEntryController.npcBehavior.UI = GameObject.Find("UI");
        npcCardEntryController.npcBehavior.GamePlay = GameObject.Find("GamePlay");
        var parent = GameObject.Find("CardBattle");
        parent.AddComponent<NPCCardEntryController>();
        parent.GetComponent<NPCCardEntryController>().npcBehavior = npcCardEntryController.npcBehavior;
        GameObject.Find("UI").SetActive(false);
        GameObject.Find("GamePlay").SetActive(false);
        SceneManager.LoadSceneAsync(SceneManager.Scene.CardBattleScene);
    }

    public void ContinueDialogue()
    {
        ShowMessage();
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        PlayerEvent.Instance.onPlayerExitDialogue.Invoke();
        gameObject.SetActive(false);
    }
}
