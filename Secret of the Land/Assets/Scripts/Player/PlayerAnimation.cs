using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private PlayerController playerController;
    private Vector3 latestMoveDirection;
    private SpriteRenderer spriteRenderer;
    private enum AnimationState
    {
        Idle,
        Move
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        latestMoveDirection = new Vector3(0, 0, 0);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimator();
    }

    ///<summary>
    ///Update the animator of the object
    ///</summary>
    void UpdateAnimator()
    {
        var direction = playerController.Direction;

        if (direction != Vector3.zero)
        {
            anim.SetBool("isMove", true);
            latestMoveDirection = direction;
            UpdateBlendTree(direction, AnimationState.Move);
            ChangeRoleDirection(direction);
        }
        else
        {
            anim.SetBool("isMove", false);
            UpdateBlendTree(latestMoveDirection, AnimationState.Idle);
            ChangeRoleDirection(latestMoveDirection);
        }
    }

    ///<summary>
    ///Update the blend tree of the animator
    ///</summary>
    ///<param name="direction">The direction of the object</param>
    ///<param name="state">The state of the object</param>
    void UpdateBlendTree(Vector3 direction, AnimationState state)
    {
        var unitDirection = direction.normalized;

        if (state == AnimationState.Idle)
        {
            anim.SetFloat("lastMoveX", unitDirection.x);
            anim.SetFloat("lastMoveY", unitDirection.y);
        }

        else if (state == AnimationState.Move)
        {
            anim.SetFloat("moveX", unitDirection.x);
            anim.SetFloat("moveY", unitDirection.y);
        }
    }

    ///<summary>
    ///Change the direction of the object (the flip of an object)
    ///</summary>
    ///<param name="direction">The direction vector of the object</param>
    void ChangeRoleDirection(Vector3 direction)
    {
        if (direction.x >= 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}
