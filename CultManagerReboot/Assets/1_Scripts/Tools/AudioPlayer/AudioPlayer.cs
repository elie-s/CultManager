using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class AudioPlayer : MonoBehaviour
    {
        public AudioSource source;
        public Sound[] sounds;

        public void PlayThisSound(string _clipName)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].clipName.Equals(_clipName))
                {
                    source.clip = sounds[i].clip;
                    source.Play();
                    break;
                }
            }
        }
        public void PlayRandom()
        {
            AudioClip clip = sounds[Random.Range(0, sounds.Length)].clip;
            source.clip = clip;
            source.Play();
        }
    }
}

