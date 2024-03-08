using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

namespace Controller
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator anim;
        private PlayerController playerController;
        private SpriteRenderer spriteRenderer;
        private BattleController battleController;
        [SerializeField]
        private GameEvent playerDeathEvent;
        private AnimatorState animationState;
        private AnimatorStateInfo animInfo;
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
            ChangeRoleDirection(playerController.Player.properties.LatestMoveDirection);
        }

        ///<summary>
        ///Update the animator of the object
        ///</summary>
        void MakeTransition()
        {
            animInfo = anim.GetCurrentAnimatorStateInfo(0);
            var direction = playerController.Player.properties.Direction;

            if (direction != Vector3.zero)
            {
                anim.SetBool("isMove", true);
            }
            else
            {
                anim.SetBool("isMove", false);
                
            }

            anim.SetBool("isAttack", battleController.IsAttack);
            anim.SetInteger("health", playerController.Player.properties.Health);

            if (animInfo.normalizedTime >= .99f)
            {
                Debug.Log("normalizedTime: " + animInfo.normalizedTime);
            }
            if (animInfo.normalizedTime >= .99f && animInfo.IsName("death"))
            {
                anim.SetTrigger("isDead");
                playerDeathEvent.TriggerEvent();
            }
        }

        ///<summary>
        ///Update the blend tree of the animator
        ///</summary>
        ///<param name="direction">The direction of the object</param>
        ///<param name="state">The state of the object</param>
        void UpdateBlendTree()
        {
            var unitDirection = playerController.Player.properties.Direction;
            var unitLatestMoveDirection = playerController.Player.properties.LatestMoveDirection;


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