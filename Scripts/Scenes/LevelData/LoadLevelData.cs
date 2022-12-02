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

    public void ChangeLevelProgression(float result) {
        if (result > ApplicationData.levels[currentLevel].normalCompletionPercent)
            ApplicationData.levels[currentLevel].normalCompletionPercent = result;
    }
    public void StarCollected(int index,Sprite sprite) {
        ApplicationData.levels[currentLevel].Stars[index] = sprite;
    }
    public void Start() {
        if (text) {
            text.text = ApplicationData.levels[currentLevel].normalCompletionPercent.ToString() + "%";
            for (var i = 0; i < Stars.Length; i++) Stars[i].sprite = ApplicationData.levels[currentLevel].Stars[i];
        }
    }
    private void FixedUpdate() {
        for (var i = 0; i < Stars.Length; i++) {
            if (Stars[i].sprite != ApplicationData.levels[currentLevel].Stars[i])
                Stars[i].sprite = ApplicationData.levels[currentLevel].Stars[i];
        }
        if (LevelProgression.value != ApplicationData.levels[currentLevel].normalCompletionPercent) {
            LevelProgression.value = ApplicationData.levels[currentLevel].normalCompletionPercent;
            text.text = ApplicationData.levels[currentLevel].normalCompletionPercent.ToString() + "%";
        }
    }
}
