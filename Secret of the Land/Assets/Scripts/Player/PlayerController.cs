using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += new Vector3(0, 0.01f, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            this.transform.position += new Vector3(0, -0.01f, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += new Vector3(-0.01f, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += new Vector3(0.01f, 0, 0);
        }

    }
}
