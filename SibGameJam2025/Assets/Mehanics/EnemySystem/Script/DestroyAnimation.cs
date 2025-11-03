using System.Collections;
using UnityEngine;

public class DestroyAnimation : MonoBehaviour
{
    [SerializeField] private float _time = 1f;
    [SerializeField] private float _height = 5f;
    [SerializeField] private AnimationCurve movementCurve;

    public void StartPlayAnimation()
    {
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        float elapsedTime = 0f;
        Vector3 targetPosition = new Vector3(
            transform.position.x,
            transform.position.y - _height,
            transform.position.z
        );
        Vector3 startPostion = transform.position;

        while (elapsedTime < _time)
        {
            float progress = elapsedTime / _time;
            float curvedProgress = movementCurve.Evaluate(progress);

            transform.position = Vector3.Lerp(
                startPostion,
                targetPosition,
                curvedProgress
            );
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
