using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
//always have rigidbody attached to player

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRigidbody;
    Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    // void Update()
    //{}

    public void Move( Vector3 _velocity)
    {
        //use rigidbody to move   
        velocity = _velocity;
    }

    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);//fix player from not tilting towards mouse
        //transform.LookAt(lookPoint);//player rotates-looks at mouse direction
        transform.LookAt(heightCorrectedPoint);
    }

    public void FixedUpdate()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + velocity * Time.deltaTime);
    }
}
