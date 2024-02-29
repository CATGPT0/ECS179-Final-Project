using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Controller
{
    public class EntityController : MonoBehaviour
{
    [SerializeField]
    private int health;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    [SerializeField]
    private int attackPower;
    public int AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }
    }
    [SerializeField]
    private int armor;
    public int Armor
    {
        get { return armor; }
        set { armor = value; }
    }
    [SerializeField]
    private int magicResist;
    public int MagicResist
    {
        get { return magicResist; }
        set { magicResist = value; }
    }

    protected EventManager eventManager;

    protected void Init()
    {
        eventManager = FindObjectOfType<EventManager>();
    }
    void Awake()
    {
        Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void CheckDeath()
    {
        if (eventManager == null)
        {
            Debug.Log("Event Manager is null");
        }
        if (Health <= 0)
        {
            eventManager.DestroyObject(this.gameObject);
        }
    }
}
}
