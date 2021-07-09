using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public Transform playerHead;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = playerHead.position;
        transform.rotation = playerHead.rotation;
    }
}
