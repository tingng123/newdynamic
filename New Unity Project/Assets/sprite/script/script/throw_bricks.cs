using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class throw_bricks : MonoBehaviour
{
    public Rigidbody2D rb;


    public GameObject BRICK_notHit;
    public float torque;
    public float damage = 1f;


    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Vector2.right;
        torque = 0.5f;
        //   rb.AddForce(new Vector2(0f, UpForce), ForceMode2D.Impulse);
   
    }

    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb.velocity.magnitude < 1f)
        {
            BRICK_notHit = Instantiate(BRICK_notHit, transform.position, Quaternion.identity) as GameObject;
            Destroy(this.gameObject);
        }




    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag == "Enemy")

        {    collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);

            GameObject   BRICK_notHitclone = Instantiate(BRICK_notHit, transform.position, Quaternion.identity) as GameObject;
         BRICK_notHitclone.GetComponent<Rigidbody2D>().AddTorque(torque, ForceMode2D.Impulse);
            Destroy(this.gameObject);
            Destroy(BRICK_notHitclone, 20f);

            

      

           

        }
    }




    /// void OnCollisionExit2D(Collision2D collision)
    /// {
    ///     if (collision.gameObject.tag == "Enemy")
    ///       {
    ///           transform.gameObject.tag = "Untagged";
    ///      }
    ///  }



    // Update is called once per frame
    void FixedUpdate() 
    {
       // rb.velocity = direction * speed;
  //// rb.velocity  = new Vector2( 0f , 20f);   

    }
}
 