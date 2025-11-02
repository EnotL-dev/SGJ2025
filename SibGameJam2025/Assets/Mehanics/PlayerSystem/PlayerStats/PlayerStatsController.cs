using SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerSystem
{
    public class PlayerStatsController : MonoBehaviour
    {
        [SerializeField] private Slider sliderHp;
        [SerializeField] private TextMeshProUGUI textHpCount;
        [SerializeField] private Slider sliderMp;
        [SerializeField] private TextMeshProUGUI textMpCount;
        [Space(5)]
        [SerializeField] private TextMeshProUGUI textBalance;
        [SerializeField] private TextMeshProUGUI textSoulsCount;

        public void ChangeHp(int currentHp, int maxHp)
        {
            sliderHp.value = currentHp;
            textHpCount.text = $"{currentHp}/{maxHp}";
        }

        public void ChangeMp(int currentMp, int maxMp)
        {
            sliderMp.value = currentMp;
            textMpCount.text = $"{currentMp}/{maxMp}";
        }

        public void balanceUpdate()
        {
            textBalance.text = $"{SaveData.TempData.GetMoney()}¤";
        }
    }
}