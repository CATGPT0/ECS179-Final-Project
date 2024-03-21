using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnnouncementManager : MonoBehaviour
{
    public AnnouncementManager Instance { get; private set; }
    public TextMeshProUGUI text;

    public Coroutine currentCoroutine;

    void Awake()
    {
        Instance = this;
        ShowText("Press H to get help", 3);
    }

    public void ShowText(string message, float time, float alpha = 1.0f)
    {
        gameObject.SetActive(true);
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(ShowTextCoroutine(message, time, alpha));
        IEnumerator ShowTextCoroutine(string message, float time, float alpha)
        {
            text.text = message;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return new WaitForSeconds(time);
            text.text = "";
            gameObject.SetActive(false);
        }
        
    }
}
