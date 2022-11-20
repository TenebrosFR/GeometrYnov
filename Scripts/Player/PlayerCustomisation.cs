using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCustomisation : MonoBehaviour
{
   [SerializeField] private Sprite[] skin;
   [SerializeField] private Sprite[] secondLayer;
   [SerializeField] private Sprite[] shadows;
   [SerializeField] private SpriteRenderer FirstLayer;
   [SerializeField] private SpriteRenderer SecondLayer;
   [SerializeField] private SpriteRenderer ShadowLayer;
   [SerializeField] private SpriteRenderer ShipLayer;
   [SerializeField] private float shipScale;
    private void Start()
   {
      FirstLayer.sprite = skin[Random.Range(0, skin.Length)];
      SecondLayer.sprite = secondLayer[Random.Range(0, secondLayer.Length)];
      ShadowLayer.sprite = shadows[Random.Range(0, shadows.Length)];
   }
    public void SwitchTo(string state) {
        if (state == "cube") {
            ShipLayer.gameObject.SetActive(false);
            gameObject.transform.localScale = new Vector3(1,1,1);
        }
        if (state == "ship") {
            ShipLayer.gameObject.SetActive(true);
            gameObject.transform.localScale = new Vector3(shipScale, shipScale, 1);
        }
    }
}
