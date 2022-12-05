using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Finishing : MonoBehaviour
{
    [SerializeField] LoadLevelData levelDataManager;
    [SerializeField] TextMeshProUGUI[] textes;
    [SerializeField] Canvas EndMenu;
    [SerializeField] Image Sign;
    [SerializeField] Image[] Stars;
    [SerializeField] Sprite Empty;
    [SerializeField] Sprite Loaded;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == 3) {
            Time.timeScale = 0;
            var resultArray = GameManager.GameManagerInstance.Result();
            //Distribuer les textes a saisir
            textes[0].text = "+"+resultArray[0];
            textes[1].text = resultArray[1];
            textes[2].text = resultArray[2];
            //Activer le menu
            EndMenu.gameObject.SetActive(true);
            //SignMovement
            Sign.rectTransform.offsetMin = new Vector2(0, 10000); // new Vector2(left, bottom); 
            Sign.rectTransform.offsetMax = new Vector2(0, -10000); // new Vector2(-right, -top); 
            Sign.rectTransform.offsetMin = Vector2.Lerp(Sign.rectTransform.offsetMin, Vector2.zero,10);
            Sign.rectTransform.offsetMax = Vector2.Lerp(Sign.rectTransform.offsetMax, Vector2.zero, 10);
            //Set les Images
            foreach (var img in Stars)
                img.sprite = Empty;
            //Les étoiles ramasser on l'image pleine
            foreach (var coinTaken in GameManager.GameManagerInstance.tookCoin) {
                Stars[coinTaken.index].sprite = Loaded;
                ApplicationData.levels[1].Stars[coinTaken.index] = Loaded;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        EndMenu.gameObject.SetActive(false);
    }
}
