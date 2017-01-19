using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour 
{
    public float speed;
    public float tmp;

    Rigidbody rigid;

    void Start () 
    {
        rigid = GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void Update () 
    {
        float inputX    = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        bool  inputJump = Input.GetButtonDown("Jump");

        transform.Translate(inputX, 0, 0,Space.World);

        if (inputJump)
        {
            rigid. AddRelativeForce(Vector3.up * tmp);
        }
	}

}
