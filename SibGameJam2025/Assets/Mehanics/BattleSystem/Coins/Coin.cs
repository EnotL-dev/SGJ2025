using UnityEngine;

namespace BattleSystem
{
    public class Coin : MonoBehaviour
    {
        private float LifeTime = 2f;

        private void Update()
        {
            LifeTime -= Time.deltaTime;
            if (LifeTime <= 0)
                Destroy(gameObject);
        }
    }
}