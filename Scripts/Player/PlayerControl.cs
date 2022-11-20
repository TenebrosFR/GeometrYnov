using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private PlayerDying dyingScript;
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
    public void ChangeState(string value) { state = value; jumpSpeed = 0;ResetRotation(); }


    private void FixedUpdate()
    {
        //Pour éviter d'avoir a l'écrire avec *30 a chaque fois
        time = 30 * Time.fixedDeltaTime;
        if (state == "cube") cubeMovement();
        if (state == "ship") shipMovement();
        //Déplacement, mutliplicateur de vitesse de saut si on est un vaisseau
        rb.velocity = new Vector2(speed * time, jumpSpeed * time * (state=="ship" ? 1.1f : 1) );
    }
    private void cubeMovement() {
        if(IsRoofed()) StartCoroutine(dyingScript.Die());
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
        //Si je suis au sol et que je ne veux pas sauter
        if (IsGrounded() && !isJumping)
            //J'arrête de tomber si je suis au sol
            jumpSpeed = 0;
        //Sinon je chute
        else if (!isJumping)
            jumpSpeed -= fallSpeed * 0.75f * time;
        //Sinon je m'envole doucement si je ne suis pas au plafond
        else if (!IsRoofed()) jumpSpeed += fallSpeed * 0.75f * time;
        //Si je suis au plafond je met ma vitesse de saut a 0 pour pas être ralenti par celui-ci
        else jumpSpeed = 0;
        if (!IsGrounded() && !IsRoofed()) {
            if (isJumping) {
                Vector3 movement = new Vector3(speed, 0, jumpSpeed).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(movement);
                targetRotation = Quaternion.RotateTowards(
                                    transform.rotation,
                                     targetRotation,
                                        360 * Time.fixedDeltaTime);
                Debug.Log(targetRotation);
                rb.MoveRotation(targetRotation);
            }
            else {
                Vector3 movement = new Vector3(speed, 0, jumpSpeed).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(movement);
                targetRotation = Quaternion.RotateTowards(
                                    transform.rotation,
                                     targetRotation,
                                        360 * Time.fixedDeltaTime);
                Debug.Log(targetRotation);
                rb.MoveRotation(targetRotation);
            };
        }
        //au sol j'arrondi pour que sa prite soit droite
        else ResetRotation();
    }
    private void ResetRotation() {
        Vector3 rotation = gameObject.transform.rotation.eulerAngles;
        rotation.z = Mathf.Round(rotation.z / 90) * 90;
        gameObject.transform.rotation = Quaternion.Euler(rotation);
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
        //Si j'arrive ici et que je ne suis pas au sol je ne re saute pas (éviter le spam pour sauter plusieur fois) (vrai que si je suis en cube)
        if (!IsGrounded() && state=="cube") return;
        //Sinon je suis "en train de sauter"
        isJumping = true;
        //Pour le mode avions je gère autrement la speed
        if (state == "cube") {
            jumpSpeed = jumpForce;
            jumpCount++;
        }
    }

    //Je raycast du centre de mon cube vers le sol pour savoir si il est assez proche du sol pour dire qu'il est "grounded"
    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, distanceFromGround + 0.05f,groundMask);
    }
    //Je raycast du centre de mon cube vers le plafond pour savoir si il est assez proche du plafond pour dire qu'il est "roofed"
    private bool IsRoofed() {
        return Physics2D.Raycast(transform.position, Vector2.up, distanceFromGround + 0.05f, groundMask);
    }
}