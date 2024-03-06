using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetXPTextController : MonoBehaviour
{
    [SerializeField]
    private GameObject xpUI;
    private TextMeshProUGUI xpText;
    private Image xpImage;
    void Awake()
    {
        xpText = xpUI.GetComponentInChildren<TextMeshProUGUI>();
        xpImage = xpUI.GetComponent<Image>();
    }
    void Start()
    {
        xpUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void XPUIShowUp(int xp)
    {
        StartCoroutine(XPUIShowUpCoroutine(xp));
    }

    public IEnumerator XPUIShowUpCoroutine(int xp)
    {
        xpUI.SetActive(true);
        xpText.color = new Color(xpText.color.r, xpText.color.g, xpText.color.b, 1);
        xpImage.color = new Color(xpText.color.r, xpText.color.g, xpText.color.b, 1);
        xpText.text = "+" + xp.ToString() + "XP";
        yield return new WaitForSeconds(3);
        StartCoroutine(FadeOutImage(2, xpImage));
        StartCoroutine(FadeOutText(2, xpText));
        yield return new WaitForSeconds(2);
        xpUI.SetActive(false);
    }

    public IEnumerator FadeOutImage(float time, Image image)
    {
        int ticks = Mathf.RoundToInt(time / Time.deltaTime);
        float unit = 1f / ticks;
        for (int i = 0; i < ticks; i++)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1 - i * unit);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public IEnumerator FadeOutText(float time, TextMeshProUGUI text)
    {
        int ticks = Mathf.RoundToInt(time / Time.deltaTime);
        float unit = 1f / ticks;
        for (int i = 0; i < ticks; i++)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1 - i * unit);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
