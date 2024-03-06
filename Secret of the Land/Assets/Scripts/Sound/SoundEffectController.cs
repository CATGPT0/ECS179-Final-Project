using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;
using Unity.VisualScripting;


public class SoundEffectController : MonoBehaviour
{
    public AudioSource audioSource;
    private PlayerController playerController;
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
        if (Mathf.Abs(playerController.VelocityX) > 0.1f || Mathf.Abs(playerController.VelocityY) > 0.1f)
        {
            audioSource.enabled = true;
        }
        else
        {
            audioSource.enabled = false;
        }
    }
}
