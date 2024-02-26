using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    private GameObject rightHitBox;
    private GameObject upHitBox;

    private PlayerController playerController;
    private bool isAttack;
    public bool IsAttack
    {
        get { return isAttack; }
        private set { isAttack = value; }
    }

    public enum BattleState
    {
        LeftAttack,
        RightAttack,
        UpAttack,
        DownAttack
    }

    void Awake()
    {
        rightHitBox = transform.Find("RightHitBox").gameObject;
        upHitBox = transform.Find("UpHitBox").gameObject;
        playerController = FindObjectOfType<PlayerController>();
        isAttack = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectAttack();
    }

    private void ChangeState()
    {
        var direction = playerController.Direction;
    }

    private void DetectAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }
    }
}
