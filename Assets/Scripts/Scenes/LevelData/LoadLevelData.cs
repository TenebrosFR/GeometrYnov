using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevelData : MonoBehaviour
{
    [SerializeField] private Image[] Stars;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int currentLevel;
    [SerializeField] private Slider LevelProgression;
    [SerializeField] private Slider PracticeLevelProgression;

    public void Start() {
        if (text) {
            text.text = ApplicationData.levels[currentLevel].normalCompletionPercent.ToString() + "%";
            for (var i = 0; i < Stars.Length; i++) Stars[i].sprite = ApplicationData.levels[currentLevel].Stars[i];
        }
    }
    private void FixedUpdate() {
        //Change les étoiles qui ont été rammasser si il y en a de ramasser
        for (var i = 0; i < Stars.Length; i++) {
            if (Stars[i].sprite != ApplicationData.levels[currentLevel].Stars[i])
                Stars[i].sprite = ApplicationData.levels[currentLevel].Stars[i];
        }
        Debug.Log(LevelProgression.value);
        //Change les valeurs du slider du lvl si elle ont changer
        if (LevelProgression.value != ApplicationData.levels[currentLevel].normalCompletionPercent) {
            LevelProgression.value = ApplicationData.levels[currentLevel].normalCompletionPercent;
            if(text)text.text = ApplicationData.levels[currentLevel].normalCompletionPercent.ToString() + "%";
        }
    }
}
