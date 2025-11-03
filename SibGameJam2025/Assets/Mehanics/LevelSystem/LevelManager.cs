using PlayerSystem;
using SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelSystem
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textFountainSouls;
        [SerializeField] private Slider sliderFountainSouls;
        [Space(5)]
        [SerializeField] private CanvasGroup hintGroup;
        [SerializeField] private TextMeshProUGUI textHint;

        PlayerRefs playerRefs = new PlayerRefs();
        public void InitializeLevel(int maxSouls) //Инициализируем на старте
        {
            SaveData.currentSouls = 0;
            SaveData.maxSouls = maxSouls;
            playerRefs.PlayerStastController.SoulsUpdate();
        }

        public void SoulAdd()
        {
            SaveData.currentSouls++;

            if(SaveData.currentSouls == SaveData.maxSouls)
                LevelComplete();

            playerRefs.PlayerStastController.SoulsUpdate();
        }

        public void LevelComplete()
        {
            Debug.Log("Level complete");

            textHint.text = "Уровень пройден!";
        }

        public void NewWaweMessage()
        {
            textHint.text = "Грядет новая волна";
        }
    }
}