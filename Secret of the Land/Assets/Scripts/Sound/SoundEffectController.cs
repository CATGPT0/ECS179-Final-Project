using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;
using Unity.VisualScripting;


public class SoundEffectController : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private TerrainDetector terrainDetector;
    [SerializeField]
    private AudioClip grassSound;
    [SerializeField]
    private AudioClip roadSound;
    [SerializeField]
    private AudioClip woodSound;
    private PlayerController playerController;
    private TerrainDetector.TerrainType currentTerrainType;

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
}
