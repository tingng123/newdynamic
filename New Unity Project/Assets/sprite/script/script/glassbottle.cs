using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class glassbottle : MonoBehaviour
{
    private Rigidbody2D rb;
    public float damage =3f;
  ///   public GameObject glassshatter;
    public GameObject shatter;

    public float speed = 2f;
    // public GameObject fireballprefab;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();



    }

    void Update()
    {

      

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
      Destroy(this.gameObject);


        // GameObject glassshatterclone = Instantiate(glassshatter, transform.position, Quaternion.identity) as GameObject;
        GameObject shatterpoint = Instantiate(shatter, transform.position, Quaternion.identity) as GameObject;
        //    GameObject fireballclone = Instantiate(fireballprefab, transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f)) as GameObject;
        // BRICK_notHit.GetComponent<Rigidbody2D>().AddTorque(torque, ForceMode2D.Impulse);
        

     //   Destroy(fireballclone, 0.9f);
       Destroy(shatterpoint, 10f);

        Debug.Log(22);
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
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
