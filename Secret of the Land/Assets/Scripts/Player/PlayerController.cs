using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Controller
{
    public class PlayerController : EntityController
{
    [SerializeField]
    private float speed = 5.0f;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    private float velocityX;
    public float VelocityX
    {
        get { return velocityX; }
        set { velocityX = value; }
    }

    private float velocityY;
    public float VelocityY
    {
        get { return velocityY; }
        set { velocityY = value; }
    }

    private Vector3 direction;
    public Vector3 Direction
    {
        get { return direction; }
        private set { direction = value; }
    }

    private Vector3 latestMoveDirection;
    public Vector3 LatestMoveDirection
    {
        get { return latestMoveDirection; }
        private set { latestMoveDirection = value; }
    }

    private BattleController battleController;

    public Action<Vector3> AttackCallback;

    void Awake()
    {
        //This should be modifed later because we may have multiple battle controllers
        battleController = FindObjectOfType<BattleController>(); 
        AttackCallback += battleController.Attack;
        Init();
    }

    void Start()
    {
        battleController.transform.position = this.transform.position;
    }

    void Update()
    {
        velocityX = Input.GetAxisRaw("Horizontal");
        velocityY = Input.GetAxisRaw("Vertical");
        Move();
    }

    void Move()
    {
        this.direction = new Vector3(velocityX, velocityY, 0);

        if (direction != Vector3.zero)
        {
            latestMoveDirection = direction;
        }

        this.transform.position += GetCalculatedSpeed() * Time.deltaTime * direction;
    }

    ///<summary>
    ///Get the calculated speed of the player
    ///</summary>
    ///<returns> The calculated speed of the player </returns>
    private float GetCalculatedSpeed()
    {
        if (velocityX != 0 && velocityY != 0)
        {
            return speed / Mathf.Sqrt(2);
        }
        else
        {
            return speed;
        }
    }

    public void Attack()
    {
        AttackCallback?.Invoke(latestMoveDirection);
    }

}
}
