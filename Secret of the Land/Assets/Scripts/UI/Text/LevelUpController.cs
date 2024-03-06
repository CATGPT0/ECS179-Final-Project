using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    void Start()
    {
        healthUI.SetActive(false);
        speedUI.SetActive(false);
        attackPowerUI.SetActive(false);
        armorUI.SetActive(false);
        levelUI.SetActive(false);
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
        healthUI.GetComponentInChildren<TextMeshProUGUI>().text = "+" + health.ToString();
        speedUI.SetActive(true);
        speedUI.GetComponentInChildren<TextMeshProUGUI>().text = "+" + speed.ToString();
        attackPowerUI.SetActive(true);
        attackPowerUI.GetComponentInChildren<TextMeshProUGUI>().text = "+" + attackPower.ToString();
        armorUI.SetActive(true);
        armorUI.GetComponentInChildren<TextMeshProUGUI>().text = "+" + armor.ToString();
        levelUI.SetActive(true);
        levelUI.GetComponentInChildren<TextMeshProUGUI>().text = level.ToString() + "!";
        yield return new WaitForSeconds(3);
        healthUI.SetActive(false);
        speedUI.SetActive(false);
        attackPowerUI.SetActive(false);
        armorUI.SetActive(false);
        levelUI.SetActive(false);
    }
}
