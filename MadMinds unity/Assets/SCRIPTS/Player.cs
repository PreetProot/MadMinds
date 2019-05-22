using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
//when add player script to obj, it forces the script to run with this script


public class Player : LivingObjects
{
    //PREET - player movement input

    public float moveSpeed = 5;
    PlayerController controller; //ref the script
    Camera viewCamera; //cam ref

    GunController gunController;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        //MOVEMENT -PREET
        //                                    GetAxisRaw = removes movement default smoothing
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed; //normal movement x and y axis times speed
        controller.Move(moveVelocity);

        //LOOK AROUND -PREET
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition); //raycast from cam to plane - follows mouse movement
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); //get raycast to ground
        float rayDistance;

        if (groundPlane.Raycast (ray, out rayDistance)) //assign raydistance - return tru if ray intersects with ground 
        {
            Vector3 point = ray.GetPoint(rayDistance); //point of intersection -knows the distance of the ray from ground

            Debug.DrawLine(ray.origin, point, Color.red); //uncomment to check raycast from cam to ground following mouse

            controller.LookAt(point);
        }

        //WEAPON INPUT - PREET
        if(Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }
}
