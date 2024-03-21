using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<GameObject> consumables = new List<GameObject>();
    public List<GameObject> equips = new List<GameObject>();
    public List<GameObject> guaranteed = new List<GameObject>();
    private AnimatorStateInfo stateInfo;
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.name == "Player")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            animator.Play("open");
        }
    }

    void Update()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("open") && stateInfo.normalizedTime >= .90f)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        int count = Random.Range(1, 4);
        bool haveEquip = Random.Range(0, 2) == 0;
        List<GameObject> items = new List<GameObject>();
        if (haveEquip && equips.Count > 0)
        {
            Debug.Log("haveEquip");
            items.Add(equips[Random.Range(0, equips.Count)]);
            count--;
        }
        for (int i = 0; i < count; i++)
        {
            items.Add(consumables[Random.Range(0, consumables.Count)]);
        }

        foreach (var item in items)
        {
            var booty = Instantiate(item, transform.position, Quaternion.identity);
            booty.transform.position = new Vector3(Random.Range(transform.position.x - 1f, transform.position.x + 1f), Random.Range(transform.position.y - 2f, transform.position.y + 2f), 0);
            booty.transform.SetParent(GameObject.Find("Booties").transform);
        }

        foreach (var item in guaranteed)
        {
            var booty = Instantiate(item, transform.position, Quaternion.identity);
            booty.transform.position = new Vector3(Random.Range(transform.position.x - 1f, transform.position.x + 1f), Random.Range(transform.position.y - 2f, transform.position.y + 2f), 0);
            booty.transform.SetParent(GameObject.Find("Booties").transform);
        }
    }
}
