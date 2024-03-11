using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;
using Unity.VisualScripting;
using JetBrains.Annotations;


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
    [SerializeField]
    private AudioClip attackSound;
    private PlayerController playerController;
    private TerrainDetector.TerrainType currentTerrainType;
    private bool isAttacking = false;


    void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }
    void Start()
    {
        playerController.PlayerEvent.OnPlayerAttack.AddListener(PlayAttackSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(playerController.Player.properties.VelocityX) > 0.1f || Mathf.Abs(playerController.Player.properties.VelocityY) > 0.1f || isAttacking)
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

    public void PlayAttackSound()
    {
        if (isAttacking)
        {
            return;
        }
        IEnumerator AttackSound()
        {
            isAttacking = true;
            audioSource.enabled = true;
            audioSource.PlayOneShot(attackSound);
            Debug.Log("Attack sound played");
            yield return new WaitForSeconds(0.5f);
            isAttacking = false;
            audioSource.enabled = false;
        }
        StartCoroutine(AttackSound());
    }


    
}
