using System;
using UnityEngine;

public class LevelData {
    public Sprite[] Stars = new Sprite[3];
    public float normalCompletionPercent = 0;
    public float practiceCompletionPercent = 0;
    public LevelData(Sprite Empty) {
        //Je set les images sur vide vu que je n'ai pas de sauvegarde
        for(var i = 0; i < Stars.Length; i++) if(!Stars[i]) Stars[i] = Empty;
    }      
}