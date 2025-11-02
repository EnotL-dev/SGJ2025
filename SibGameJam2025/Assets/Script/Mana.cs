using ReactiveVariables;
using System;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public IReadOnlyReactiveProperty<int> Current => _current;
    public event Action IsOver;
    public event Action IsRestored;

    [SerializeField, Min(0)] private int _maxManaValue = 1;
    private ReactiveProperty<int> _current = new();
    private ReactiveProperty<int> _max = new();

    public void Reduce(int value)
    {
        if (value < 0)
        {
            Debug.LogError($"Could not reduce mana: {nameof(value)} < 0.");
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
            Debug.LogError($"Could not add mana: {nameof(value)} < 0.");
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
        _max.SetValueWithoutAction(_maxManaValue);
        _current.SetValueWithoutAction(_maxManaValue);
    }

    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        KillImmediately();
    //}
}
