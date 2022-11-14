using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float distanceFromGround;
    private bool isJumping;
    private float jumpSpeed = 0f;
    private int jumpCount = 0;
    private void FixedUpdate()
    {
        //Déplacement
        rb.velocity = new Vector2(speed, jumpSpeed);
        //Animation de rotation en l'air
        if (!IsGrounded())
            gameObject.transform.Rotate(Vector3.back * 5);
        //au sol j'arrondi pour que sa prite soit droite
        else
        {
            Vector3 rotation = gameObject.transform.rotation.eulerAngles;
            rotation.z = Mathf.Round(rotation.z / 90) * 90;
            gameObject.transform.rotation = Quaternion.Euler(rotation);
        }
    }
    //Bind sur les touches pour "sauter"
    public void OnJump(InputAction.CallbackContext context)
    {
        //Si ce n'est pas l'action ou le relachement de l'input je quitte OnJump
        if(!context.performed && !context.canceled) return;
        //Si c'est le relachement je ne saute plus et je quitte
        if (context.canceled){
            isJumping = false;
            return;
        }
        //Si j'arrive ici et que je ne suis pas au sol je ne re saute pas (éviter le spam pour sauter plusieur fois)
        if (!IsGrounded()) return;
        //Sinon je suis "en train de sauter"
        isJumping = true;
        jumpSpeed = jumpForce;
        jumpCount++;
        //Routine de la chute
        StartCoroutine(Jumping());
    }

    //Je raycast du centre de mon cube vers le sol pour savoir si il est assez proche du sol pour dire qu'il est "grounded"
    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, distanceFromGround + 0.05f,groundMask);
    }
    //Je tombe tant que je 
    private IEnumerator Jumping()
    {
        yield return new WaitForSeconds(0.1f);
        jumpSpeed -= fallSpeed;
        //Si je suis au sol
        if (IsGrounded())
        {
            //Ternaire : Si je veux sauter en boucle (input pas lacher) je re saute sinon ma valeur de saut passe a 0
            jumpSpeed = isJumping ? jumpForce : 0 ;
            //Si je veux re sauter une fois que je lui ai redonner la "force" pour sauter je le re fait tomber après
            if (isJumping)
            {
                StartCoroutine(Jumping());
                jumpCount++;
            }
        }
        //Si je suis pas au sol je continue de tomber
        if (!IsGrounded())
            StartCoroutine(Jumping());
    }
}