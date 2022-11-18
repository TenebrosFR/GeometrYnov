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
    public float jumpSpeed = 0f;
    private int jumpCount = 0;
    private float time;
    //Changement d'état vol / sol    
    public string state = "cube";
    public void ChangeState(string value) { state = value; }


    private void FixedUpdate()
    {
        //Pour éviter d'avoir a l'écrire avec *30 a chaque fois
        time = 30 * Time.fixedDeltaTime;
        if (state == "cube") cubeMovement();
        if (state == "ship") shipMovement();
        //Déplacement
        rb.velocity = new Vector2(speed * time, jumpSpeed * time);
    }
    private void cubeMovement() {
        //Si je suis au sol
        if (IsGrounded()) {
            //Ternaire : Si je veux sauter en boucle (input pas lacher) je re saute sinon ma valeur de saut passe a 0
            jumpSpeed = isJumping ? jumpForce : 0;
            //Si je veux re sauter je compte le saut
            if (isJumping)
                jumpCount++;
        }
        //Sinon je chute
        else
            jumpSpeed -= fallSpeed * time;
        //Animation de rotation en l'air
        if (!IsGrounded())
            gameObject.transform.Rotate(Vector3.back * 5);
        //au sol j'arrondi pour que sa prite soit droite
        else {
            Vector3 rotation = gameObject.transform.rotation.eulerAngles;
            rotation.z = Mathf.Round(rotation.z / 90) * 90;
            gameObject.transform.rotation = Quaternion.Euler(rotation);
        }
    }
    private void shipMovement() {
        throw new NotImplementedException();
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
    }

    //Je raycast du centre de mon cube vers le sol pour savoir si il est assez proche du sol pour dire qu'il est "grounded"
    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, distanceFromGround + 0.05f,groundMask);
    }
}