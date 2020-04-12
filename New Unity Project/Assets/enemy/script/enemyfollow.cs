using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyfollow : MonoBehaviour
{

    public float speed;
    public float stoppingDistance;

    private Transform target;


    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        
  

        if (Vector2.Distance(transform.position,target.position) < stoppingDistance)
        {

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime);
            anim.SetBool("chasing", true);
        }
        else
        {
            anim.SetBool("chasing", false);
        }
    }
}
