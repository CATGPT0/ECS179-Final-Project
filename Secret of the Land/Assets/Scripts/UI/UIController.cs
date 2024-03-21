
using Controller;
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
    private GameObject monsterManager;
    private bool isStatPanelOpen = false;
    private bool isHelpPanelOpen = false;

    void Awake()
    {
        statPanel.SetActive(false);
        helpPanel.SetActive(false);
        monsterManager = GameObject.Find("MonsterManager");
        //dialoguePanel.SetActive(false);
    }
    void Start()
    {
        
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
        monsterManager.SetActive(!isStatPanelOpen);
        PlayerController.Instance.Player.properties.CanMove = !isStatPanelOpen;
        PlayerController.Instance.PlayerAnimation.CanAttack = !isStatPanelOpen;
    }

    public void OpenCloseHelpPanel()
    {
        isHelpPanelOpen = !isHelpPanelOpen;
        helpPanel.SetActive(isHelpPanelOpen);
        monsterManager.SetActive(!isHelpPanelOpen);
        PlayerController.Instance.Player.properties.CanMove = !isStatPanelOpen;
        PlayerController.Instance.PlayerAnimation.CanAttack = !isStatPanelOpen;
    }
}
