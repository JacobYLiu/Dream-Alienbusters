using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 25f;//need to adjust base on map size
    public Vector3 velocity;// for gravity

    public CharacterController myController;
    public Transform camaraHead;//camera control
    public float gravityModifier = 100f;
    
    public float mouseSensitivity = 100f;


    //variables for jump
    public float jumpHeight = 10f;
    public Transform ground;
    public LayerMask groundLayer;
    public float groundDistance = 0.5f;
    public bool allowJump = true;

    private float cameraVerticalRotation = 0f;

    public Transform firePosition;


    //animation
    public Animator animator_control;

    //running
    public float runningSpeed;
    private bool running = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player_movement();
        player_view();
        Jump();
        //Shoot();
    }

    public void animation_manager(string gunName)
    {
        switch (gunName)
        {
            case "arms_assault_rifle_01":
                break;

            default:
                break;
        }
    }


    private void player_view()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;//get mouse x input
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;//get mouse y input

        cameraVerticalRotation -= mouseY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);//camera vertical range
        transform.Rotate(Vector3.up * mouseX);// set camara and character rotate with mouse x input

        camaraHead.localRotation = Quaternion.Euler(cameraVerticalRotation, 0f, 0f);

    }

    private void player_movement()
    {
        float x = Input.GetAxis("Horizontal");//get x axis
        float z = Input.GetAxis("Vertical");//get z axis

        Vector3 move = x * transform.right + z * transform.forward;//set move vector

        if (Input.GetKey(KeyCode.LeftShift) && move.magnitude > 0.2 && z > 0)
        {
            animator_control.SetBool("Run", true);
            running = true;
            move = move * runningSpeed * Time.deltaTime;//set movement base on time
        }
        else
        {
            animator_control.SetBool("Run", false);
            running = false;
            move = move * speed * Time.deltaTime;//set movement base on time
        }
       
        if (move.magnitude > 0.2 && !running)
        {
            animator_control.SetBool("Walk", true);
        }
        else
        {
            animator_control.SetBool("Walk", false);
        }
        //Debug.Log(move.magnitude);

        myController.Move(move);//set controller

        velocity.y += Physics.gravity.y * Mathf.Pow(Time.deltaTime, 2) * gravityModifier;

        if (myController.isGrounded)
        {
            velocity.y = Physics.gravity.y * Time.deltaTime;
        }

        myController.Move(velocity);
    }

    private void Jump()
    {
        allowJump = Physics.OverlapSphere(ground.position, groundDistance, groundLayer).Length > 0;

        if (Input.GetButtonDown("Jump") && allowJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * (-2f) * Physics.gravity.y) * Time.deltaTime;
        }

        myController.Move(velocity);
    }
}
