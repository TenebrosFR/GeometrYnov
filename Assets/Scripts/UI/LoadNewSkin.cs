using TMPro;
using UnityEngine;

public class LoadNewSkin : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;
    private static int Main = 0;
    private static int Second = 0;
    private static int Shadow = 0;

    public void ChangeMainSkin(int value) {
        Main += value;
        if (Main >= ApplicationData.SMainSkin.Count) Main = 0;
        else if (Main < 0) Main = ApplicationData.SMainSkin.Count -1;
        ApplicationData.PlayerSkin[0] = ApplicationData.SMainSkin[Main];
        text.text = Main.ToString();
    }
    public void ChangeSecondSkin(int value) {
        Second += value;
        if (Second >= ApplicationData.SSecondSkin.Count) Second = 0;
        else if (Second < 0) Second = ApplicationData.SSecondSkin.Count -1;
        ApplicationData.PlayerSkin[1] = ApplicationData.SSecondSkin[Second];
        text.text = Second.ToString();
    }
    public void ChangeShadowSkin(int value) {
        Shadow += value;
        if (Shadow >= ApplicationData.SShadowSkin.Count) Shadow = 0;
        else if (Shadow < 0) Shadow = ApplicationData.SShadowSkin.Count -1;
        ApplicationData.PlayerSkin[2] = ApplicationData.SShadowSkin[Shadow];
        text.text = Shadow.ToString();
    }
}
