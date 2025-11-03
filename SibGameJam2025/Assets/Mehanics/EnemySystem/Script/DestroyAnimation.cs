using System.Collections;
using UnityEngine;

public class DestroyAnimation : MonoBehaviour
{
    [SerializeField] private float _time = 1f;
    [SerializeField] private float _height = 5f;

    private IEnumerator PlayAnimation()
   {
        float time = _time;
        Vector3 startPostion = transform.position;
        float startY = startPostion.y;
        float targetY = startPostion.y - _height;
        while (time > 0)
        {
            time -= Time.deltaTime;
            //transform.position = transform.position.y / targetY
            transform.position = new Vector3(transform.position.x, transform.position.y - (2f * Time.deltaTime), transform.position.z);
            yield return new WaitForEndOfFrame();
        }
    }
}
