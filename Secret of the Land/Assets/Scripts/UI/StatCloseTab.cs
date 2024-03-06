using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCloseTab : MonoBehaviour
{
    [SerializeField]
    private GameObject statPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked");
        statPanel.SetActive(false);
    }
}
