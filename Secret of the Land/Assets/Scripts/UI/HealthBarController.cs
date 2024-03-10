using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    private Gradient gradient;
    [SerializeField]
    private Image fill;
    private Slider slider;
    private Player player;
    private PlayerEvent playerEvent;
    void Awake()
    {
        slider = GetComponent<Slider>();
        player = FindFirstObjectByType<Player>();
        playerEvent = FindFirstObjectByType<PlayerEvent>();
    }

    void Start()
    {
        SetMaxHealth(player.properties.Health);
        playerEvent.OnPlayerHealthChanged += SetHealth;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }
}
