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
   [SerializeField] private SpriteRenderer spriteRenderer1;
   [SerializeField] private SpriteRenderer spriteRenderer2;
   [SerializeField] private SpriteRenderer spriteRenderer3;
   private void Start()
   {
      spriteRenderer1.sprite = skin[Random.Range(0, skin.Length)];
      spriteRenderer2.sprite = secondLayer[Random.Range(0, secondLayer.Length)];
      spriteRenderer3.sprite = shadows[Random.Range(0, shadows.Length)];
   }
}
