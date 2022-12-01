using UnityEngine;
using UnityEngine.UI;

public class UIPlayerCustomization : MonoBehaviour
{
    [SerializeField] private Image FirstLayer;
    [SerializeField] private Image SecondLayer;
    [SerializeField] private Image ShadowLayer;
    private void FixedUpdate() {
        if (FirstLayer.sprite != ApplicationData.PlayerSkin[0])
            FirstLayer.sprite = ApplicationData.PlayerSkin[0];
        if (SecondLayer.sprite != ApplicationData.PlayerSkin[1])
            SecondLayer.sprite = ApplicationData.PlayerSkin[1];
        if (ShadowLayer.sprite != ApplicationData.PlayerSkin[2])
            ShadowLayer.sprite = ApplicationData.PlayerSkin[2];
    }
}
