using System.Collections;
using System.Collections.Generic;
using Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropertyTextController : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField]
    private TextMeshProUGUI healthText;
    [SerializeField]
    private TextMeshProUGUI speedText;
    [SerializeField]
    private TextMeshProUGUI attackPowerText;
    [SerializeField]
    private TextMeshProUGUI armorText;
    [SerializeField]
    private TextMeshProUGUI healthBarText;
    [SerializeField]
    private TextMeshProUGUI levelText;
    void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + playerController.Player.properties.Health + "/" + playerController.Player.properties.MaxHealth;
        speedText.text = "Speed: " + playerController.Player.properties.Speed;
        attackPowerText.text = "Attack Power: " + playerController.Player.properties.AttackPower;
        armorText.text = "Armor: " + playerController.Player.properties.Armor;
        healthBarText.text = playerController.Player.properties.Health + "/" + playerController.Player.properties.MaxHealth;
        levelText.text = "Level: " + playerController.LevelManager.Level + " XP: " + playerController.LevelManager.XP + "/" + playerController.LevelManager.levelThresholds[playerController.LevelManager.Level];
    }
}
