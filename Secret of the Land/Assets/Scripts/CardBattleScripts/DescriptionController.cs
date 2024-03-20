using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionController : MonoBehaviour
{

    public GameObject textGameObject;
    private Text descriptionText;
    private void Start()
    {
        descriptionText = textGameObject.GetComponent<Text>();
    }
    void Update()
    {
        Vector3 m_MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        this.transform.position = Camera.main.ScreenToWorldPoint(m_MousePos);
    }

    public void SetDescription(string description)
    {
        descriptionText.text = description;
    }

}
