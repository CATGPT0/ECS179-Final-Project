using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    private GameObject rightHitBox;
    private GameObject upHitBox;
    [SerializeField]
    private float right;
    [SerializeField]
    private float up;
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
        isAttack = false;
    }
    void Start()
    {
        upHitBox.transform.localPosition = new Vector3(0, up, 0);
        upHitBox.SetActive(false);
        rightHitBox.transform.localPosition = new Vector3(right, 0, 0);
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

    public void Attack(Vector3 lastMoveDirection)
    {
        var attackDirection = lastMoveDirection.normalized;
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
            rightHitBox.transform.localPosition = new Vector3(-right, 0, 0);
            rightHitBox.SetActive(true);
            Debug.Log("Left");
        }
        else if (state == BattleState.RightAttack)
        {
            rightHitBox.transform.localPosition = new Vector3(right, 0, 0);
            rightHitBox.SetActive(true);
            Debug.Log("Right");
        }
        else if (state == BattleState.UpAttack)
        {
            upHitBox.transform.localPosition = new Vector3(0, up, 0);
            upHitBox.SetActive(true);
            Debug.Log("Up");
        }
        else
        {
            upHitBox.transform.localPosition = new Vector3(0, -up, 0);
            upHitBox.SetActive(true);
            Debug.Log("Down");
        }
        print(this.transform.position);
        yield return new WaitForSeconds(0.1f);
        rightHitBox.SetActive(false);
        upHitBox.SetActive(false);
    }
}
