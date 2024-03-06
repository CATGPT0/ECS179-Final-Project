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
    void Awake()
    {
        slider = GetComponent<Slider>();
        player = FindFirstObjectByType<Player>();
    }

    void Start()
    {
        SetMaxHealth(player.Health);
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
