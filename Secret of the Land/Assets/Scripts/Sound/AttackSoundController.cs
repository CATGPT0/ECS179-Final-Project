using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class AttackSoundController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip attackSound;
    private bool isAttacking = false;
    private PlayerController playerController;

    void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }
    void Start()
    {
        playerController.PlayerEvent.OnPlayerAttack.AddListener(PlayAttackSound);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            audioSource.PlayOneShot(attackSound);
            yield return new WaitForSeconds(0.5f);
            isAttacking = false;
        }
        StartCoroutine(AttackSound());
    }
}
