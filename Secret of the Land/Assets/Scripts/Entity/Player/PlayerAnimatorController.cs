using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Controller
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator anim;
        private PlayerController playerController;
        private SpriteRenderer spriteRenderer;
        private BattleController battleController;
        private enum AnimationState
        {
            Idle,
            Move,
            Attack
        }

        void Awake()
        {
            anim = GetComponentInParent<Animator>();
            playerController = GetComponentInParent<PlayerController>();
            spriteRenderer = GetComponentInParent<SpriteRenderer>();
            battleController = FindFirstObjectByType<BattleController>();
        }

        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            MakeTransition();
            UpdateBlendTree();
            ChangeRoleDirection(playerController.LatestMoveDirection);
        }

        ///<summary>
        ///Update the animator of the object
        ///</summary>
        void MakeTransition()
        {
            var direction = playerController.Direction;

            if (direction != Vector3.zero)
            {
                anim.SetBool("isMove", true);
            }
            else
            {
                anim.SetBool("isMove", false);
                
            }

            anim.SetBool("isAttack", battleController.IsAttack);
        }

        ///<summary>
        ///Update the blend tree of the animator
        ///</summary>
        ///<param name="direction">The direction of the object</param>
        ///<param name="state">The state of the object</param>
        void UpdateBlendTree()
        {
            var unitDirection = playerController.Direction;
            var unitLatestMoveDirection = playerController.LatestMoveDirection;


            anim.SetFloat("lastMoveX", unitLatestMoveDirection.x);
            anim.SetFloat("lastMoveY", unitLatestMoveDirection.y);
            anim.SetFloat("moveX", unitDirection.x);
            anim.SetFloat("moveY", unitDirection.y);


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
}