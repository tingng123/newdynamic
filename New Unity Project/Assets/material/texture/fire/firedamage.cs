using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firedamage : MonoBehaviour
{
    ParticleSystem particle;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    public int damage;
    public float DPS = 1f;
    private float timeSinceDPS;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        timeSinceDPS = DPS;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceDPS += Time.deltaTime;
    }


    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particle, other, collisionEvents);

        int count = collisionEvents.Count;

        if (other.gameObject.tag == "Enemy" && timeSinceDPS >= DPS)

        {
            int damage = Random.Range( 2, 5);
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);


            timeSinceDPS = 0f;

        }


    }
}