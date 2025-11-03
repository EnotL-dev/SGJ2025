using PlayerSystem;
using ReactiveVariables;
using SaveSystem;
using System;
using System.Collections;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private PlayerStatsController playerStats;
    public event Action IsOver;
    public event Action IsRestored;

    [SerializeField, Min(0)] private int _maxManaValue = 1;
    public ReactiveProperty<int> _current = new();
    public ReactiveProperty<int> _max = new();

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

        playerStats.ChangeMp(_current.Value, _max.Value);
    }

    public void Add(int value)
    {
        if (value < 0)
        {
            Debug.LogError($"Could not add mana: {nameof(value)} < 0.");
            return;
        }
        _current.Value = Mathf.Clamp(_current.Value + value, 0, _max.Value);
        playerStats.ChangeMp(_current.Value, _max.Value);
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
        _max.SetValueWithoutAction(SaveData.TempData.playerParams.maxMp);
        _current.SetValueWithoutAction(SaveData.TempData.playerParams.maxMp);

        playerStats.ChangeMp(_current.Value, _max.Value);
    }

    private void Start()
    {
        StartCoroutine(ManaUpdate());
    }

    IEnumerator ManaUpdate()
    {
        while(true)
        {
            int addValue = SaveData.TempData.playerParams.lv;
            Add(addValue);
            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }
}
