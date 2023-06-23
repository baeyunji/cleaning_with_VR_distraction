using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementCharacterController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;
    private Vector3 moveForce; //이동 힘 


    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float gravitiy;

    private CharacterController characterController;


    [Header("mapping control")]
    public bool temp;
    public bool nk_sound;
    public bool tk_sound;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
  

    // Update is called once per frame
    private void Update()
    {

        if(!characterController.isGrounded)
        {
            moveForce.y += gravitiy * Time.deltaTime;
        }

        characterController.Move(moveForce * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.B))//온도가 낮아짐 
        {
            moveSpeed = 1;

        }

        if (Input.GetKeyDown(KeyCode.O))//노크소리
        {
            moveSpeed = 0;

        }

        if (Input.GetKeyDown(KeyCode.N))//온도가 올라가거나 말소리가 들림
        {
            moveSpeed = 5;

        }

        if (temp == true && nk_sound ==false && tk_sound ==false)
        {


            moveSpeed = 1;
        }
        
        if (temp == false && nk_sound == true && tk_sound == false)
        {


            moveSpeed = 0;
        }
       
        if (temp == false && nk_sound == false && tk_sound == true)
        {


            moveSpeed = 5;
        }
        if (temp == false && nk_sound == true && tk_sound == true)
        {


            moveSpeed = 5;
            nk_sound = false;
        }
        if (temp == false && nk_sound == false && tk_sound == false)
        {


            moveSpeed = 5;
        }


    }

    public void MoveTo(Vector3 direction)
    {

        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);

        moveForce = new Vector3(direction.x * moveSpeed, moveForce.y, direction.z * moveSpeed);

    }

    public void Jump()
    {

        if(characterController.isGrounded)
        {
            moveForce.y = jumpForce;
        }
    }
}
