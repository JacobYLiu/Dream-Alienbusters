using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0.1f;//need to adjust base on map size
    public int hp = 100;
    public bool isDead = false;
    public CharacterController myController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");//get x axis
        float z = Input.GetAxis("Vertical");//get z axis

        Vector3 move = x * transform.right + z * transform.forward;//set move vector

        myController.Move(move);//set controller
    }
}
