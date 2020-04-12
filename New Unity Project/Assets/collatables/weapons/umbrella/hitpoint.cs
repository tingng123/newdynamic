using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitpoint : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform attack;
    public float radiuss = 0.02f;
    public LayerMask enemy;
    public float damage = 3f;
    public Vector2 direction;
    public float knockbackAmount = 10f;
    public bool knockfromRt;
    public playercontroller controller;
    public Transform player;

 //   public float knockbackStrength;
  //  public float knockbackRadius;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


       



        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attack.transform.position, radiuss, enemy);

        foreach (Collider2D enemy in hitEnemies)
        {
         

        }
          
    }

  public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag ("Enemy"))
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if(enemy != null)
            {
               if (enemy.transform.position.x < player.transform.position.x)
                {
                    enemy.transform.Translate(-1f*knockbackAmount,0f,0f);
                }
               else if (enemy.transform.position.x > player.transform.position.x )
                {
                    enemy.transform.Translate(1f* knockbackAmount,0f,0f);
                }


                enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
            }

           
         //   enemy.transform.Translate(direction * knockbackAmount);
            Debug.Log("knockback");


        }
    }

        void OnDrawGizmosSelected()
    {
        if (attack == null)
            return;
        Gizmos.DrawWireSphere(attack.position, radiuss);
    }

}
