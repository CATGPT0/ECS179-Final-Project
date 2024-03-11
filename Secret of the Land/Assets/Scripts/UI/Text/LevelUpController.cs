using System;
using System.Collections;
using System.Collections.Generic;
using Schema.Builtin.Nodes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpController : MonoBehaviour
{
    [SerializeField]
    private GameObject healthUI;
    [SerializeField]
    private GameObject speedUI;
    [SerializeField]
    private GameObject attackPowerUI;
    [SerializeField]
    private GameObject armorUI;
    [SerializeField]
    private GameObject levelUI;
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI speedText;
    private TextMeshProUGUI attackPowerText;
    private TextMeshProUGUI armorText;
    private TextMeshProUGUI levelText;
    private Image healthImage;
    private Image speedImage;
    private Image attackPowerImage;
    private Image armorImage;
    private Image levelImage;
    private Image healthIconImage;
    private Image speedIconImage;
    private Image attackPowerIconImage;
    private Image armorIconImage;
    private Image levelIconImage;
    private PlayerEvent playerEvent;
    void Awake()
    {
        healthText = healthUI.GetComponentInChildren<TextMeshProUGUI>();
        speedText = speedUI.GetComponentInChildren<TextMeshProUGUI>();
        attackPowerText = attackPowerUI.GetComponentInChildren<TextMeshProUGUI>();
        armorText = armorUI.GetComponentInChildren<TextMeshProUGUI>();
        levelText = levelUI.GetComponentInChildren<TextMeshProUGUI>();
        healthImage = healthUI.GetComponent<Image>();
        speedImage = speedUI.GetComponent<Image>();
        attackPowerImage = attackPowerUI.GetComponent<Image>();
        armorImage = armorUI.GetComponent<Image>();
        levelImage = levelUI.GetComponent<Image>();
        healthIconImage = GameObject.Find("HealthIcon").GetComponent<Image>();
        speedIconImage = GameObject.Find("SpeedIcon").GetComponent<Image>();
        attackPowerIconImage = GameObject.Find("ApIcon").GetComponent<Image>();
        armorIconImage = GameObject.Find("ArmorIcon").GetComponent<Image>();
        levelIconImage = GameObject.Find("LevelIcon").GetComponent<Image>();
        playerEvent = FindFirstObjectByType<PlayerEvent>();
    }
    void Start()
    {
        healthUI.SetActive(false);
        speedUI.SetActive(false);
        attackPowerUI.SetActive(false);
        armorUI.SetActive(false);
        levelUI.SetActive(false);
        playerEvent.OnPlayerLevelUp += LevelUIShowUp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUIShowUp(int health, float speed, int attackPower, int armor, int level)
    {
        StartCoroutine(LevelUIShowUpCoroutine(health, speed, attackPower, armor, level));
    }

    public IEnumerator LevelUIShowUpCoroutine(int health, float speed, int attackPower, int armor, int level)
    {
        healthUI.SetActive(true);
        healthText.text = "+" + health.ToString();
        speedUI.SetActive(true);
        speedText.text = "+" + speed.ToString();
        attackPowerUI.SetActive(true);
        attackPowerText.text = "+" + attackPower.ToString();
        armorUI.SetActive(true);
        armorText.text = "+" + armor.ToString();
        levelUI.SetActive(true);
        levelText.text = "Lv." + level.ToString() + "!";
        healthText.color = new Color(healthText.color.r, healthText.color.g, healthText.color.b, 1);
        speedText.color = new Color(speedText.color.r, speedText.color.g, speedText.color.b, 1);
        attackPowerText.color = new Color(attackPowerText.color.r, attackPowerText.color.g, attackPowerText.color.b, 1);
        armorText.color = new Color(armorText.color.r, armorText.color.g, armorText.color.b, 1);
        levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, 1);
        healthImage.color = new Color(healthImage.color.r, healthImage.color.g, healthImage.color.b, 1);
        speedImage.color = new Color(speedImage.color.r, speedImage.color.g, speedImage.color.b, 1);
        attackPowerImage.color = new Color(attackPowerImage.color.r, attackPowerImage.color.g, attackPowerImage.color.b, 1);
        armorImage.color = new Color(armorImage.color.r, armorImage.color.g, armorImage.color.b, 1);
        levelImage.color = new Color(levelImage.color.r, levelImage.color.g, levelImage.color.b, 1);
        healthIconImage.color = new Color(healthIconImage.color.r, healthIconImage.color.g, healthIconImage.color.b, 1);
        speedIconImage.color = new Color(speedIconImage.color.r, speedIconImage.color.g, speedIconImage.color.b, 1);
        attackPowerIconImage.color = new Color(attackPowerIconImage.color.r, attackPowerIconImage.color.g, attackPowerIconImage.color.b, 1);
        armorIconImage.color = new Color(armorIconImage.color.r, armorIconImage.color.g, armorIconImage.color.b, 1);
        levelIconImage.color = new Color(levelIconImage.color.r, levelIconImage.color.g, levelIconImage.color.b, 1);
        yield return new WaitForSeconds(3);
        StartCoroutine(FadeOutImage(2, levelImage));
        StartCoroutine(FadeOutImage(2, levelIconImage));
        StartCoroutine(FadeOutText(2, levelText));
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeOutImage(2, attackPowerImage));
        StartCoroutine(FadeOutImage(2, attackPowerIconImage));
        StartCoroutine(FadeOutText(2, attackPowerText));
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeOutImage(2, speedImage));
        StartCoroutine(FadeOutImage(2, speedIconImage));
        StartCoroutine(FadeOutText(2, speedText));
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeOutImage(2, armorImage));
        StartCoroutine(FadeOutImage(2, armorIconImage));
        StartCoroutine(FadeOutText(2, armorText));
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeOutImage(2, healthImage));
        StartCoroutine(FadeOutImage(2, healthIconImage));
        StartCoroutine(FadeOutText(2, healthText));
        yield return new WaitForSeconds(2);
        healthUI.SetActive(false);
        speedUI.SetActive(false);
        attackPowerUI.SetActive(false);
        armorUI.SetActive(false);
        levelUI.SetActive(false);
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
