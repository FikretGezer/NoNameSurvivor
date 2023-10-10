using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> clips;//0 shooting SFX, 1 enemy dying SFX
        [SerializeField] private List<AudioClip> musics;//0 shooting SFX, 1 enemy dying SFX

        private AudioSource sfxSource;

        public static SoundManager Instance;
        private void Awake()
        {
            if (Instance == null) Instance = this;
            sfxSource = GetComponent<AudioSource>();
        }   
        public void PlaySFX(int i)
        {
            AudioClip clip;
            if (i == 0)
                clip = clips[0];
            else
                clip = clips[1];
            sfxSource.PlayOneShot(clip);
        }
        public void PlayRandomMusic(AudioSource src)
        {
            int i = Random.Range(0, musics.Count);
            src.clip = musics[i];
            src.Play();
        }
    }
}
