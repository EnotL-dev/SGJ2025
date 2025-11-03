using PlayerSystem;
using SaveSystem;
using UnityEngine;

namespace BattleSystem
{
    public class CoinDroper : MonoBehaviour
    {
        [SerializeField] private PlayerStatsController playerStatsController;
        [SerializeField] private Coin coinPrefab;

        private float spawnForce = 5f;
        private float radius = 0.3f;
        private float upwardForce = 5f;

        public void DropCoins(Vector3 pos, int count)
        {
            for (int i = 0; i < count; i++)
            {
                // равномерное распределение углов по кругу
                float angle = i * Mathf.PI * 2f / count;

                Vector3 spawnPos = pos + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

                Coin coin = Instantiate(coinPrefab, spawnPos, Quaternion.identity);

                Rigidbody rb = coin.GetComponent<Rigidbody>();
                if (rb == null) continue;

                Vector3 dir = (spawnPos - pos).normalized + Vector3.up * 0.5f;

                rb.AddForce(dir * spawnForce + Vector3.up * upwardForce, ForceMode.Impulse);
            }


            SaveData.TempData.AddMoney(count * 3);
            playerStatsController.balanceUpdate();
        }
    }
}
