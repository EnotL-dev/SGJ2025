using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace LevelSystem
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> clips;
        [SerializeField] private MinMaxValue _cutoffFreq;
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private string lowPassParameterNameMusic = "LowpassCutoff_Music";
        [SerializeField] private string lowPassParameterNameSound = "LowpassCutoff_Sound";

        [SerializeField] private bool playOnStart = true;

        private void Start()
        {
            if (playOnStart)
                PlayRandomMusic();
        }

        public void PlayRandomMusic()
        {
            if(clips.Count > 0)
            {
                int rndClip = Random.Range(0, clips.Count);
                GetComponent<AudioSource>().PlayOneShot(clips[rndClip]);
            }
        }

        [ContextMenu("Muffle Music")]
        public void MuffleMusic()
        {
            _mixer.SetFloat(lowPassParameterNameMusic, _cutoffFreq.Min);
        }

        public void MuffleMusic(bool selector)
        {
            _mixer.SetFloat(lowPassParameterNameMusic, selector ? _cutoffFreq.Max : _cutoffFreq.Min);
        }

        [ContextMenu("Unmuffle Music")]
        public void UnmuffleMusic()
        {
            _mixer.SetFloat(lowPassParameterNameMusic, _cutoffFreq.Max);
        }

        [ContextMenu("Muffle Sound")]
        public void MuffleSound()
        {
            _mixer.SetFloat(lowPassParameterNameSound, _cutoffFreq.Min);
        }

        public void MuffleSound(bool selector)
        {
            _mixer.SetFloat(lowPassParameterNameSound, selector ? _cutoffFreq.Max : _cutoffFreq.Min);
        }

        [ContextMenu("Unmuffle Sound")]
        public void UnmuffleSound()
        {
            _mixer.SetFloat(lowPassParameterNameSound, _cutoffFreq.Max);
        }
    }
}