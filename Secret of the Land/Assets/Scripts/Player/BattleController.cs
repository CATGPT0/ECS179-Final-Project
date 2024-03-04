using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class BattleController : MonoBehaviour
    {
        private GameObject rightHitBox;
        private GameObject upHitBox;
        private PlayerController playerController;
        [SerializeField]
        private Vector2 right;
        [SerializeField]
        private Vector2 up;
        private bool isAttack;
        public bool IsAttack
        {
            get { return isAttack; }
            private set { isAttack = value; }
        }
        //public Action<GameObject, GameObject> attackCallback;
        public enum BattleState
        {
            LeftAttack,
            RightAttack,
            UpAttack,
            DownAttack
        }

        void Awake()
        {
            playerController = GetComponentInParent<PlayerController>();
            rightHitBox = transform.Find("RightHitBox").gameObject;
            upHitBox = transform.Find("UpHitBox").gameObject;
            isAttack = false;
        }
        void Start()
        {
            upHitBox.transform.localPosition = new Vector3(up.x, up.y, 0);
            upHitBox.SetActive(false);
            rightHitBox.transform.localPosition = new Vector3(right.x, right.y, 0);
            rightHitBox.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            DetectAttack();
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

        public void Attack()
        {
            var attackDirection = playerController.LatestMoveDirection.normalized;
            BattleState state;

            if (attackDirection.x < 0)
            {
                state = BattleState.LeftAttack;
            }
            else if (attackDirection.x > 0)
            {
                state = BattleState.RightAttack;
            }
            else if (attackDirection.y > 0)
            {
                state = BattleState.UpAttack;
            }
            else
            {
                state = BattleState.DownAttack;
            }

            StartCoroutine(ActivateHitBox(state));
        }

        private IEnumerator ActivateHitBox(BattleState state)
        {
            upHitBox.SetActive(false);
            rightHitBox.SetActive(false);

            if (state == BattleState.LeftAttack)
            {
                rightHitBox.transform.localPosition = new Vector3(-right.x, right.y, 0);
                rightHitBox.SetActive(true);
                Debug.Log("Left");
            }
            else if (state == BattleState.RightAttack)
            {
                rightHitBox.transform.localPosition = new Vector3(right.x, right.y, 0);
                rightHitBox.SetActive(true);
                Debug.Log("Right");
            }
            else if (state == BattleState.UpAttack)
            {
                upHitBox.transform.localPosition = new Vector3(up.x, up.y, 0);
                upHitBox.SetActive(true);
                Debug.Log("Up");
            }
            else
            {
                upHitBox.transform.localPosition = new Vector3(up.x, -up.y, 0);
                upHitBox.SetActive(true);
                Debug.Log("Down");
            }
            yield return new WaitForSeconds(0.1f);
            rightHitBox.SetActive(false);
            upHitBox.SetActive(false);
        }
    }
}
