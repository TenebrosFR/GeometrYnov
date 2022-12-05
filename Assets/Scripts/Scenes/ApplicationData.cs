using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ApplicationData : MonoBehaviour
{
    [SerializeField] private Sprite Empty;
    //
    public static List<Sprite> SMainSkin;
    [SerializeField] private List<Sprite> MainSkin;
    public static List<Sprite> SSecondSkin;
    [SerializeField] private List<Sprite> SecondSkin;
    public static List<Sprite> SShadowSkin;
    [SerializeField] private List<Sprite> ShadowSkin;
    public static Sprite[] PlayerSkin = new Sprite[3];
    public static LevelData[] levels = new LevelData[14];

    private void Start() {
        //Set les statics
        if (SMainSkin == null) {
            for (var i = 0; i < levels.Length; i++) levels[i] = new LevelData(Empty);
            SMainSkin = MainSkin;
            SSecondSkin = SecondSkin;
            SShadowSkin = ShadowSkin;
            PlayerSkin[0] = SMainSkin[0];
            PlayerSkin[1] = SSecondSkin[0];
            PlayerSkin[2] = SShadowSkin[0];
        }
    }
}
