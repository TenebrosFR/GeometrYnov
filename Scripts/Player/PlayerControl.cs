using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float distanceFromGround;
    private bool isJumping;
    private float jumpSpeed = 0f;
    
    //Déplacement
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, jumpSpeed);
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
        StartCoroutine(Jumping());
    }

    //Je raycast du centre de mon cube vers le sol pour savoir si il est assez proche du sol pour dire qu'il est "grounded"
    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, distanceFromGround + 0.05f);
    }
    //Je tombe tant que je 
    private IEnumerator Jumping()
    {
        yield return new WaitForSeconds(.1f);
        jumpSpeed -= fallSpeed;
        //Si je veux enchainer les sauts et je suis au sol je re saute et continue ma prochaine chute
        if (IsGrounded() && isJumping)
        {
            jumpSpeed = jumpForce;
            StartCoroutine(Jumping());
        }
        //Si je suis pas au sol je continue de tomber
        if (IsGrounded())
            StartCoroutine(Jumping());
    }
}
