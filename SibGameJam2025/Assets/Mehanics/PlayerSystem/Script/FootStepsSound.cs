using UnityEngine;

namespace PlayerSystem
{
    public class FootStepsSound : MonoBehaviour
    {
        //private Moving _moving
        //private float _timeToStep;
        //private float _currentTimeToStep = 0;
        //private bool _left = true;

        //public void Initialize()
        //{
        //    _player.IsCrouched.Changed += ChangeTimeToStep;
        //    _player.IsCrouched.Changed += ChangeStepVolume;
        //    ChangeTimeToStep(false, false);
        //    ChangeStepVolume(false, false);
        //}

        //public void Dispose()
        //{
        //    _player.IsCrouched.Changed -= ChangeTimeToStep;
        //    _player.IsCrouched.Changed -= ChangeStepVolume;
        //}

        //public void Tick()
        //{
        //    if (_player.IsGrounded.Value && _player.Velocity.magnitude > 0)
        //    {
        //        _currentTimeToStep += Time.deltaTime;
        //        if (_currentTimeToStep > _timeToStep)
        //        {
        //            _currentTimeToStep = 0;
        //            _refs.FootSteps.pitch = GetPitch();
        //            _refs.FootSteps.Play();
        //        }
        //    }
        //    else
        //    {
        //        _currentTimeToStep = _timeToStep + 1;
        //    }
        //}

        //private float GetPitch()
        //{
        //    if (_left)
        //    {
        //        _left = false;
        //        return 0.9f;
        //    }
        //    else
        //    {
        //        _left = true;
        //        return 1f;
        //    }
        //}

        //private void ChangeTimeToStep(bool old, bool crouching)
        //{
        //    _timeToStep = (crouching) ?
        //        _player.PlayerConfig.StepSoundFrequency.Max
        //        : _player.PlayerConfig.StepSoundFrequency.Min;
        //}

        //private void ChangeStepVolume(bool old, bool crouching)
        //{
        //    _refs.FootSteps.volume = (crouching) ?
        //        _player.PlayerConfig.StepSoundVolume.Min
        //        : _player.PlayerConfig.StepSoundVolume.Max;
        //}
    }
}