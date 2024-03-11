using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;
using Unity.VisualScripting;
using JetBrains.Annotations;


public class MovementSoundController : MonoBehaviour
{
    private AudioSource audioSource;
    private TerrainDetector terrainDetector;
    [SerializeField]
    private AudioClip grassSound;
    [SerializeField]
    private AudioClip roadSound;
    [SerializeField]
    private AudioClip woodSound;
    [SerializeField]
    private AudioClip attackSound;
    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private AudioClip respawnSound;
    private PlayerController playerController;
    private TerrainDetector.TerrainType currentTerrainType;

    void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        terrainDetector = GetComponent<TerrainDetector>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        //playerController.PlayerEvent.OnPlayerDeathEnter.AddListener(PlayDeathSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(playerController.Player.properties.VelocityX) > 0.1f || Mathf.Abs(playerController.Player.properties.VelocityY) > 0.1f)
        {
            audioSource.enabled = true;
            audioSource.loop = true;
        }
        else
        {
            audioSource.enabled = false;
        }

        if (currentTerrainType != terrainDetector.Type)
        {
            currentTerrainType = terrainDetector.Type;
            ChangeSound();
        }
    }

    void ChangeSound()
    {
        if (terrainDetector.Type == TerrainDetector.TerrainType.Grass)
        {
            audioSource.clip = grassSound;
        }
        else if (terrainDetector.Type == TerrainDetector.TerrainType.Road)
        {
            audioSource.clip = roadSound;
        }
        else if (terrainDetector.Type == TerrainDetector.TerrainType.Bridge)
        {
            audioSource.clip = woodSound;
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.Play();
    }

    // public void PlayDeathSound()
    // {
    //     if (isDead)
    //     {
    //         return;
    //     }
    //     IEnumerator DeathSound()
    //     {
    //         isDead = true;
    //         audioSource.enabled = true;
    //         audioSource.loop = false;
    //         audioSource.PlayOneShot(deathSound);
    //         yield return new WaitForSeconds(1.5f);
    //         audioSource.enabled = false;
    //         isDead = false;
    //     }
    //     StartCoroutine(DeathSound());
    // }


    
}
