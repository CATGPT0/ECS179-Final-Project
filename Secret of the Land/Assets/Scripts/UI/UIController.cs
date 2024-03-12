
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject statPanel;
    private bool isStatPanelOpen = false;

    void Awake()
    {
        statPanel.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            OpenCloseStatPanel();
        }
    }

    public void OpenCloseStatPanel()
    {
        isStatPanelOpen = !isStatPanelOpen;
        statPanel.SetActive(isStatPanelOpen);
    }
}
