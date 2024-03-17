using System.Collections;
using Plugins.KennethDevelops.Events;
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
        private AnimatorStateInfo animInfo;
        private bool canAttack = true;
        private bool isAttack = false;
        private bool inDialogue = false;
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
            playerController.PlayerEvent.OnPlayerRespawn.AddListener(Respawn);
            playerController.PlayerEvent.OnPlayerGitHit += GitHitAnimation;
            PlayerEvent.Instance.onPlayerEnterDialogue.AddListener(StartDialogue);
            PlayerEvent.Instance.onPlayerExitDialogue.AddListener(ExitDialogue);
        }

        // Update is called once per frame
        void Update()
        {
            MakeTransition();
            DetectAttack();
            UpdateBlendTree();
            FlipTo(playerController.Player.properties.LatestMoveDirection);
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

            anim.SetBool("isAttack", isAttack);
            anim.SetInteger("health", playerController.Player.properties.Health);
            anim.SetBool("canAttack", canAttack);

            if (animInfo.IsName("Attack"))
            {
                playerController.PlayerEvent.OnPlayerAttack?.Invoke();
                if (animInfo.normalizedTime <= .70f)
                {
                    canAttack = false;
                    playerController.Player.properties.CanMove = false;
                }
                else
                {
                    canAttack = true;
                    playerController.Player.properties.CanMove = true;
                }
            }

            if (animInfo.IsName("death"))
            {
                playerController.Player.properties.CanMove = false;
                playerController.PlayerEvent.OnPlayerDeathEnter?.Invoke();
                if (animInfo.normalizedTime >= .99f)
                {
                playerController.PlayerEvent.OnPlayerDeathExit?.Invoke();
                }   
                
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
        void FlipTo(Vector3 direction)
        {
            if (direction.x >= 0)
            {
                GetComponentInParent<PlayerController>().gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                GetComponentInParent<PlayerController>().gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        private void DetectAttack()
        {
            if (inDialogue)
            {
                isAttack = false;
                return;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                isAttack = true;
            }
            else
            {
                isAttack = false;
            }
        }

        private void Respawn()
        {
            playerController.Player.properties.CanMove = true;
            anim.Play("Idle");
        }

        private void GitHitAnimation()
        {
            Debug.Log("Git Hit");
            Transform tr = playerController.gameObject.transform;
            float maxTime = 0.5f;
            float timer = 0;
            IEnumerator GitHitAni()
            {
                while (timer < maxTime)
                {
                    tr.position += new Vector3(0.02f, 0, 0);
                    timer += Time.deltaTime;
                    yield return new WaitForSeconds(Time.deltaTime);
                    timer += Time.deltaTime;
                    tr.position -= new Vector3(0.02f, 0, 0);
                    yield return new WaitForSeconds(Time.deltaTime);
                }
            }
            StartCoroutine(GitHitAni());
        }

        private void StartDialogue()
        {
            inDialogue = true;
        }

        private void ExitDialogue()
        {
            inDialogue = false;
        }
    }
}