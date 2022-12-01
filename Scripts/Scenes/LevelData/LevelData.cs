using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class LevelData {
    public Sprite[] Stars = new Sprite[3];
    LevelData(Sprite Empty) {
        //Je set les images sur vide vu que je n'ai pas de sauvegarde
        //foreach (var i = 0;i < 3;i++) coin.Value = Empty;
    }      
}
