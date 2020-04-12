using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodsplatter2 : MonoBehaviour
{
    ParticleSystem particle;
    public GameObject bloodsplatterprefab;
    ///public Transform bloodground;

    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    public AudioSource audioSource;
    public AudioClip[] sounds;
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
        TimePassed += Time.deltaTime;
        if (TimePassed > SoundCapResetSpeed)
        {
            soundsPlayed = 0;
            TimePassed = 0;

        }

    }
    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particle, other, collisionEvents);

        int count = collisionEvents.Count;

    ///   for (int i = 0; i < count; i++)
       // {
            GameObject bloodsplatterclone = Instantiate(bloodsplatterprefab, transform.position , Quaternion.Euler(0.0f, 0.0f, Random.Range(0f, 180f))) as GameObject;
            Destroy(bloodsplatterclone, 5f);

            if (soundsPlayed < MaxSounds)
            {
                soundsPlayed += 1;
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)], Random.Range(0.1f, 0.35f));
            }
        //}
    }
}
