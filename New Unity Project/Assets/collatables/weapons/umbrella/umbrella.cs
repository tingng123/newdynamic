using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class umbrella : MonoBehaviour
{

    public Animator anim;
    public Animator pen;
    float horizontalMove = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pen.GetBool("isRunning"))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
     
        if(pen.GetBool("isCrouching"))
        {
            anim.SetBool("isCrouching", true);
        }
           else
        {
            anim.SetBool("isCrouching", false);
        }

        if (pen.GetBool("isCrouchingrunning"))
        {
            anim.SetBool("isCrouchingrunning", true);
        }
        else
        {
            anim.SetBool("isCrouchingrunning", false);
        }
    }
}
