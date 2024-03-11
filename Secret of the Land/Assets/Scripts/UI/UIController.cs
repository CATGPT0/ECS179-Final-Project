
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject statPanel;

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
            OpenStatPanel();
        }
    }

    public void OpenStatPanel()
    {
        statPanel.SetActive(true);
    }

    public void CloseStatPanel()
    {
        statPanel.SetActive(false);
    }
}
