using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private RotateToMouse rotateToMouse;
    private MovementCharacterController movement;



    private void Awake()
    {
        Cursor.visible = false;
       

        rotateToMouse = GetComponent<RotateToMouse>();
        movement = GetComponent<MovementCharacterController>();
    }


    

    // Update is called once per frame
    private void Update()
    {

        UpdateMove();
        UpdateRotate();
        UpdateJump();

    }

    private void UpdateRotate()
    {

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotateToMouse.UpdateRotate(mouseX, mouseY);
    }

    private void UpdateMove()
    {

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        movement.MoveTo(new Vector3(x, 0, z));
    }

    private void UpdateJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.Jump();

        }
            
               
    }
}
