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
        [SerializeField] private TextMeshProUGUI textLv;

        public void ChangeHp(int currentHp, int maxHp)
        {
            sliderHp.maxValue = maxHp;
            sliderHp.value = currentHp;
            textHpCount.text = $"{currentHp}/{maxHp}";
        }

        public void ChangeMp(int currentMp, int maxMp)
        {
            sliderMp.maxValue = maxMp;
            sliderMp.value = currentMp;
            textMpCount.text = $"{currentMp}/{maxMp}";
        }

        public void BalanceUpdate()
        {
            textBalance.text = $"{SaveData.TempData.GetMoney()}¤";
        }

        public void SoulsUpdate()
        {
            textSoulsCount.text = $"{SaveData.currentSouls}/{SaveData.maxSouls}";
        }

        public void LvUpdate()
        {
            textLv.text = $"Lv {SaveData.TempData.playerParams.lv}";
        }
    }
}