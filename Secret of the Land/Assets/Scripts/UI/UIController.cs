
using Plugins.KennethDevelops.Events;
using Plugins.KennethDevelops.Extensions;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject statPanel;
    [SerializeField]
    private GameObject helpPanel;
    [SerializeField]
    private GameObject dialoguePanel;
    private bool isStatPanelOpen = false;
    private bool isHelpPanelOpen = false;
    private bool isDialoguePanelOpen = false;

    void Awake()
    {
        statPanel.SetActive(false);
        helpPanel.SetActive(false);
        dialoguePanel.SetActive(false);
    }
    void Start()
    {
        EventManager.Subscribe<OnPlayerEnterDialogue>(OpenDialoguePanel, this);
        EventManager.Subscribe<OnPlayerExitDialogue>(CloseDialoguePanel, this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenCloseStatPanel();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            OpenCloseHelpPanel();
        }
    }

    public void OpenCloseStatPanel()
    {
        isStatPanelOpen = !isStatPanelOpen;
        statPanel.SetActive(isStatPanelOpen);
    }

    public void OpenCloseHelpPanel()
    {
        isHelpPanelOpen = !isHelpPanelOpen;
        helpPanel.SetActive(isHelpPanelOpen);
    }

    public void OpenDialoguePanel(OnPlayerEnterDialogue e)
    {
        dialoguePanel.SetActive(true);
    }

    public void CloseDialoguePanel(OnPlayerExitDialogue e)
    {
        dialoguePanel.SetActive(false);
    }
}
