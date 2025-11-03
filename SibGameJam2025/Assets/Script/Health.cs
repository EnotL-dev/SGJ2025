using PlayerSystem;
using ReactiveVariables;
using SaveSystem;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private PlayerStatsController playerStats;
    public event Action IsOver;
    public event Action IsRestored;

    [SerializeField, Min(0)] private int _maxHealthValue = 1;
    public ReactiveProperty<int> _current = new();
    public ReactiveProperty<int> _max = new();

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

        if(playerStats)
            playerStats.ChangeHp(_current.Value, _max.Value);
    }

    public void Add(int value)
    {
        if (value < 0)
        {
            Debug.LogError($"Could not add health: {nameof(value)} < 0.");
            return;
        }
        _current.Value = Mathf.Clamp(_current.Value + value, 0, _max.Value);

        if(playerStats)
            playerStats.ChangeHp(_current.Value, _max.Value);
    }

    public void RestoreToFull()
    {
        _current.Value = _max.Value;
        IsRestored?.Invoke();

        if(playerStats)
            playerStats.ChangeHp(_current.Value, _max.Value);
    }

    public void KillImmediately()
    {
        _current.Value = 0;
        IsOver?.Invoke();

        if(playerStats)
            playerStats.ChangeHp(_current.Value, _max.Value);
    }

    private void Awake()
    {
        if(playerStats)
            _max.SetValueWithoutAction(SaveData.TempData.playerParams.maxHp);
        else
            _max.SetValueWithoutAction(_maxHealthValue);

        if (playerStats)
            _current.SetValueWithoutAction(SaveData.TempData.playerParams.maxHp);
        else
            _current.SetValueWithoutAction(_maxHealthValue);

        if (playerStats)
            playerStats.ChangeHp(_current.Value, _max.Value);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            KillImmediately();
        if (Input.GetKey(KeyCode.KeypadMinus))
            Reduce(1);
    }
}
