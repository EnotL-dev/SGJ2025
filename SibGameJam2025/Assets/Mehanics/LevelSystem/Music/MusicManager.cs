using UnityEngine;
using UnityEngine.Audio;

namespace LevelSystem
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private MinMaxValue _cutoffFreq;
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private string lowPassParameterNameMusic = "LowpassCutoff_Music";
        [SerializeField] private string lowPassParameterNameSound = "LowpassCutoff_Sound";

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