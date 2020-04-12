using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    // Start is called before the first frame update
    public enum Sound
    {
        playerthrow,
        playerwalk
       
    }

    private static Dictionary<Sound, float> soundTimerDictionary;

    public static void Initialize() {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.playerwalk] = 0f;
    }

      public static void PlaySound(Sound sound)
    {if (CanPlaySound(sound)) {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
       audioSource.PlayOneShot(GetAudioClip(sound));
         }
    }

    public static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.playerwalk:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerwalkTimeMax = .4f;
                    if (lastTimePlayed + playerwalkTimeMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }   else { return true; }
                
            //    break;
        }
    }
 




    public static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAsset.SoundAudioClip soundAudioClip in GameAsset.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + "not found");
        return null;
    }


}