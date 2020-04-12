using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_green_object : MonoBehaviour

{
    public EnemyHealth health;

    public GameObject[] bodypart;
    public CommonScript commonscript;

    //public GameObject head;
    //public GameObject chest;
    //public GameObject back;
    //public GameObject rtUpperArms;
    //public GameObject ltUpperArms;
    //public GameObject rtLowerArms;
    //public GameObject ltLowerArms;
    //public GameObject rtlegs;
    //public GameObject ltlegs;
    //public GameObject baton;
    //public GameObject shield;
    public GameObject blood;
    public GameObject bloodparticle;


    private Rigidbody2D rb;

    public Animator anim;
    public float speed = 1f;
    private Transform target;

    public float patrolspeed = 0.5f;
    public float waitTime = 4f;
    public float startWaitTime;

    public Transform[] moveSpots;
    private int randomspot;
    public flip flipping;
    public bool m_FacingRight;

    public Transform reference;
    public float knockbackAmount = 2f;
    public Vector2 impulse;
 

    public float attackrange;
    public float attackcooltime;
    public float Timesinceattack =0f;


    //private Transform target;

    // Start is called before the first frame update
    void Start()
    { 
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_FacingRight = flipping.m_FacingRight;
        //   waitTime = startWaitTime;
        randomspot = Random.Range(0, moveSpots.Length);
   
    }

    // Update is called once per frame
    void Update()
    {

        Timesinceattack += Time.deltaTime;

        // if (target.position.x > transform.position.x)
        //    {
        //       //face right
        //   transform.localScale = new Vector3(-1, 1, 1);
        //  }
        //   else if (target.position.x < transform.position.x)
        //  {
        //       //face left
        //       transform.localScale = new Vector3(1, 1, 1);
        //   }
        if (anim.GetBool("chasing"))
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime);

        }

        if (transform.position.x - target.position.x < attackrange && attackcooltime <= Timesinceattack)
        {

           
            Timesinceattack = 0f;
         //   anim.SetBool("chasinig", false);

        }

            if (anim.GetBool("patrol"))
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveSpots[randomspot].position.x, transform.position.y), patrolspeed * Time.deltaTime);


            if (moveSpots[randomspot].position.x > transform.position.x && flipping.m_FacingRight == false)
            {
                //face right
                //  child.transform.parent = parent.transform;
                //  transform.DetachChildren();
                //  transform.localScale = new Vector3(-1.2f, 1.4f, 1);
                //   theScale = transform.localScale;
               flipping.Flip();
                

            }

            else if (moveSpots[randomspot].position.x < transform.position.x && flipping.m_FacingRight )
            {


                //face left
                //transform.localScale = new Vector3(1.2f, 1.4f, 1);
                ///  theScale = transform.localScale;

                //  theScale.x = Mathf.Abs(theScale.x);
                //  flipping.Flip();
                flipping.Flip();
                

            }






            if (Vector2.Distance(transform.position, moveSpots[randomspot].position) < 0.3f)
            {

                anim.SetBool("patrol", false);
                //    if (waitTime <= 0)
                //       {
                randomspot = Random.Range(0, moveSpots.Length);
                //            waitTime = startWaitTime;
                //       }
                //        else
                //        {
                //        waitTime -= Time.deltaTime;
                ///        }


            }
        }



    }


void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "brick" || collision.gameObject.tag == "weapon")
        {

            GameObject bloodclone = Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(bloodclone, 5f);
            GameObject bloodparticleclone = Instantiate(bloodparticle, transform.position, Quaternion.identity);
            Destroy(bloodparticleclone, 5f);

        }

        if (collision.gameObject.tag == "weapon")
        {

            Debug.Log("knockback");


        }

        if (collision.gameObject.tag == "roadblock")
        {
        //    if (anim.)
                anim.SetBool("patrol", false);


        }



    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "brick")
        {
            GameObject bloodclone = Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(bloodclone, 5f);
            GameObject bloodparticleclone = Instantiate(bloodparticle, transform.position, Quaternion.identity);
            Destroy(bloodparticleclone, 5f);

        }
        if (collision.gameObject.tag == "weapon")
        {
            GameObject bloodclone = Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(bloodclone, 5f);
            GameObject bloodparticleclone = Instantiate(bloodparticle, transform.position, Quaternion.identity);
            Destroy(bloodparticleclone, 5f);

            Debug.Log("knockback");

        }


    }
    


    void OnDestroy()
    {
        commonscript.BodyBreakDown(bodypart, transform.position);
        //Instantiate(head, transform.position, Quaternion.identity);
        //Instantiate(chest, transform.position, Quaternion.identity);
        //Instantiate(back, transform.position, Quaternion.identity);
        //Instantiate(rtUpperArms, transform.position, Quaternion.identity);
        //Instantiate(rtLowerArms, transform.position, Quaternion.identity);
        //Instantiate(ltUpperArms, transform.position, Quaternion.identity);
        //Instantiate(ltLowerArms, transform.position, Quaternion.identity);
        //Instantiate(rtlegs, transform.position, Quaternion.identity);
        //Instantiate(ltlegs, transform.position, Quaternion.identity);
        //Instantiate(baton, transform.position, Quaternion.identity);
        //Instantiate(shield, transform.position, Quaternion.identity);

        

    }

}
