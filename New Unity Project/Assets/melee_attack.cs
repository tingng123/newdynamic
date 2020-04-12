using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee_attack : MonoBehaviour
{
    private Collider2D hitbox;
    public float damage = 3f;
    public Animator anim;
    public bool attack;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    void OnCollisionEnter2D(Collision2D collision)
    { 
        
        if (collision.gameObject.tag == "Enemy" && anim.GetBool("attack"))
        {

            if (this.transform.position.x > gameObject.transform.position.x)
            {
         //       Vector2 impulse = new Vector2(knockback, 0f);
            }
            else if (this.transform.position.x < gameObject.transform.position.x)
            {
      //          Vector2 impulse = new Vector2(-knockback, 0f);
            }

            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);

        }
    }
        
    }