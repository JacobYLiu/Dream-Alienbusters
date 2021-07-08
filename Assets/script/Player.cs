using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 25f;//need to adjust base on map size
    public int hp = 100;
    public bool isDead = false;
    public CharacterController myController;
    public Transform camaraHead;//camera control

    private float cameraVerticalRotation;
    public float mouseSensitivity = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player_movement();
        player_view();
    }

    void player_view()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;//get mouse x input
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;//get mouse y input

        cameraVerticalRotation -= mouseY;
        transform.Rotate(Vector3.up * mouseX);// set camara and character rotate with mouse x input
        camaraHead.localRotation = Quaternion.Euler(cameraVerticalRotation, 0f, 0f);

    }

    void player_movement()
    {
        float x = Input.GetAxis("Horizontal");//get x axis
        float z = Input.GetAxis("Vertical");//get z axis

        Vector3 move = x * transform.right + z * transform.forward;//set move vector
        move = move * speed * Time.deltaTime;//set movement base on time



        myController.Move(move);//set controller
    }
}
