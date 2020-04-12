using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shatterpoint : MonoBehaviour
{
    ParticleSystem particle;
    public GameObject fireprefab;
    public GameObject fireballprefab;
    //public GameObject glassbroken;
    ///public Transform bloodground;

    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
   // public AudioSource audioSource;
  //  public AudioClip[] sounds;
    public float SoundCapResetSpeed = 0.55f;
    public int MaxSounds = 3;
    float TimePassed;
    int soundsPlayed;
    /// public float offset;
    ///float  Vector3 Offset;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()


    {

        ///Vector3 Offset = new Vector3(0.0f, offset, 0.0f);
       // TimePassed += Time.deltaTime;
     //   if (TimePassed > SoundCapResetSpeed)
    //    {
      //      soundsPlayed = 0;
     //       TimePassed = 0;

      //  }

    }
    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particle, other, collisionEvents);

        int count = collisionEvents.Count;

        ///   for (int i = 0; i < count; i++)
        // {
        GameObject fireprefabclone = Instantiate(fireprefab, transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f)) as GameObject;
        GameObject fireballclone = Instantiate(fireballprefab, transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f)) as GameObject;
        Destroy(fireprefabclone, 5f);

     //   if (soundsPlayed < MaxSounds)
       // {
         //   soundsPlayed += 1;
           // audioSource.pitch = Random.Range(0.9f, 1.1f);
  //          audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)], Random.Range(0.1f, 0.35f));
        //}
        //}
    }
}
