using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepeatImage : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [SerializeField] private float speedScroll = 1f;

    private void FixedUpdate()
    {
        //Déplacement de l'image
        image.uvRect = new Rect(image.uvRect.position + (Vector2.right * (speedScroll * Time.fixedDeltaTime)), image.uvRect.size);
    }
}
