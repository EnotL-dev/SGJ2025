using ReactiveVariables;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public IReadOnlyReactiveProperty<int> Current => _current;
    public event Action IsOver;
    public event Action IsRestored;

    [SerializeField, Min(0)] private int _maxHealthValue = 1;
    private ReactiveProperty<int> _current;
    private ReactiveProperty<int> _max;

    public void Reduce(int value)
    {
        if (value < 0)
        {
            Debug.LogError($"Could not reduce health: {nameof(value)} < 0.");
            return;
        }
        if (_current.Value > 0)
            _current.Value = Mathf.Clamp(_current.Value - value, 0, _max.Value);
        if (_current.Value <= 0)
            IsOver?.Invoke();
    }

    public void Add(int value)
    {
        if (value < 0)
        {
            Debug.LogError($"Could not add health: {nameof(value)} < 0.");
            return;
        }
        _current.Value = Mathf.Clamp(_current.Value + value, 0, _max.Value);
    }

    public void RestoreToFull()
    {
        _current.Value = _max.Value;
        IsRestored?.Invoke();
    }

    public void KillImmediately()
    {
        _current.Value = 0;
        IsOver?.Invoke();
    }

    private void Awake()
    {
        _max.SetValueWithoutAction(_maxHealthValue);
        _current.SetValueWithoutAction(_maxHealthValue);
    }
}
