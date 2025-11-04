using PlayerSystem;
using SaveSystem;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{
    public class CoinDroper : MonoBehaviour
    {
        [SerializeField] private Coin coinPrefab;
        [SerializeField] private float _offset = 1f;
        [SerializeField] private int _coinsCountMin = 0;
        [SerializeField] private int _coinsCountMax = 0;
        [SerializeField] private Health _health;
        [Space(5)]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private List<AudioClip> coinClips;

        private float spawnForce = 5f;
        private float radius = 0.3f;
        private float upwardForce = 5f;

        private void OnEnable()
        {
            _health.IsOver += DropCoins;
        }

        private void OnDisable()
        {
            _health.IsOver -= DropCoins;
        }

        private void DropCoins()
        {
            Vector3 pos = transform.position;
            pos.y += _offset;
            int count = Random.Range(_coinsCountMin, _coinsCountMax + 1);
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

            int addGoldRush = 0;
            if (SaveData.TempData.nodeGraph.feature is GoldRush)
                addGoldRush = 1;

            MakeSound();
            SaveData.TempData.AddMoney(count * (3+addGoldRush));
            PlayerRefs.Instance.PlayerStastController.BalanceUpdate();
        }

        private void MakeSound()
        {
            if (!audioSource || coinClips.Count < 1)
                return;

            int rndClip = Random.Range(0, coinClips.Count);
            audioSource.PlayOneShot(coinClips[rndClip]);
        }
    }
}
