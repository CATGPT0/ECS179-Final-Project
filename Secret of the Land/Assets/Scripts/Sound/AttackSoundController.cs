using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class AttackSoundController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip attackSound;
    [SerializeField]
    private AudioClip gethitSound;
    private PlayerController playerController;

    void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }
    void Start()
    {
        playerController.PlayerEvent.OnPlayerAttack.AddListener(PlayAttackSound);
        playerController.PlayerEvent.OnGetHitSound += PlayGetHitSound;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAttackSound()
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        IEnumerator AttackSound()
        {
            audioSource.PlayOneShot(attackSound);
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(AttackSound());
    }

    public void PlayGetHitSound()
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        IEnumerator AttackSound()
        {
            audioSource.PlayOneShot(gethitSound);
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(AttackSound());
    }
}
