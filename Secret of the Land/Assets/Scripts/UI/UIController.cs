
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject statPanel;
    [SerializeField]
    private GameObject helpPanel;
    private bool isStatPanelOpen = false;
    private bool isHelpPanelOpen = false;

    void Awake()
    {
        statPanel.SetActive(false);
        helpPanel.SetActive(false);
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
    }

    public void OpenCloseHelpPanel()
    {
        isHelpPanelOpen = !isHelpPanelOpen;
        helpPanel.SetActive(isHelpPanelOpen);
    }
}
