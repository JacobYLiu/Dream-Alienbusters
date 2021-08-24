using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaimen : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
  public  void kai()
    {
        anim.Play("kaimen");
    }
    public void guan()
    {
        anim.Play("guanmen");
    }
}
