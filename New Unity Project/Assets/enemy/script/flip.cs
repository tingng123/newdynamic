using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flip : MonoBehaviour
{
    private Transform target;
    // Start is called before the first frame update
    //  private GameObject child;

       private Canvas healthCavas;
    // public GameObject canvasBoard; // variable
    ///  private Vector3 theScale;

 
    public bool m_FacingRight;

    public float stoppingDistance = 3f;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
      
        //    healthCavas.transform.localScale = new Vector3(1f, 1f, 1f);
    //Vector3 canvasscale =  canvasBoard.transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       


        if (Vector2.Distance(transform.position, target.position) < stoppingDistance)
        {
            if (target.position.x > transform.position.x && !m_FacingRight)
            {
                //face right
                //  child.transform.parent = parent.transform;
                //  transform.DetachChildren();
                //  transform.localScale = new Vector3(-1.2f, 1.4f, 1);
                //   theScale = transform.localScale;
                Flip();

            }
            else if (target.position.x < transform.position.x && m_FacingRight)
            {


                //face left
                //transform.localScale = new Vector3(1.2f, 1.4f, 1);
                ///  theScale = transform.localScale;

                //  theScale.x = Mathf.Abs(theScale.x);
                Flip();


            }



        }
     //   canvasBoard.transform.localScale = canvasscale;

    }
    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        ////////theScale.x *= Mathf.Abs(theScale.x);
      //  healthCavas.transform.localScale = theScale;
    //    canvasBoard.transform.localScale = canvasscale;
    }


}
