using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xiaoshi : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        Invoke("xs",1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void xs()
    {
        gameObject.SetActive(false);
    }
}
