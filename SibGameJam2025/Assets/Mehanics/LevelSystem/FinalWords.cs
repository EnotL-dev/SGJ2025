using UnityEngine;


public class FinalWords : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Animation _wordAnimation;

    private void OnEnable()
    {
        _health.IsOver += Show;
    }

    private void OnDisable()
    {
        _health.IsOver -= Show;
    }

    private void Show()
    {
        _wordAnimation.gameObject.SetActive(true);
        _wordAnimation.Play();
    }
}
