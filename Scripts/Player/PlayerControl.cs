using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private PlayerDying dyingScript;
    [SerializeField] private PlayerCustomisation skinScript;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Rigidbody2D rb;
    //Rotation
    [SerializeField] Vector2 minMaxAngle;
    [SerializeField] Vector2 minMaxVelocity;
    //Forces
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float distanceFromGround;
    [SerializeField] private float distanceFromCeiling;
    public bool isJumping;
    public float jumpSpeed = 0f;
    private float time;
    //Changement d'état vol / sol    
    public string state = "cube";
    public void ChangeState(string value) { 
        state = value; 
        jumpSpeed = 0;
        ResetRotation(true);
        skinScript.SwitchTo(value);
        if (value == "cube") distanceFromGround -= 0f;
        if (value == "ship") distanceFromGround += 0f;
        transform.position += Vector3.up*0.133f;
    }
    private void Start() {
        rb.velocity = new Vector2(1,0);
    }
    private void FixedUpdate()
    {
        //Pour éviter d'avoir a l'écrire avec *30 a chaque fois
        time = 30 * Time.fixedDeltaTime;
        if (state == "cube") cubeMovement();
        else if (state == "ship") shipMovement();
        //Déplacement, mutliplicateur de vitesse de saut si on est un vaisseau
        if (rb.velocity.x < 1) StartCoroutine(dyingScript.Die());
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
                GameManager.GameManagerInstance.totalJump++;
        }
        //Sinon je chute
        else
            jumpSpeed -= fallSpeed * time;
        //Animation de rotation en l'air
        if (!IsGrounded())
            gameObject.transform.Rotate(Vector3.back * rotateSpeed);
        //au sol j'arrondi pour que sa prite soit droite
        else {
            Vector3 rotation = gameObject.transform.rotation.eulerAngles;
            rotation.z = Mathf.Round(rotation.z / 90) * 90;
            gameObject.transform.rotation = Quaternion.Euler(rotation);
        }
    }
    private void shipMovement() {
        //Si je suis au sol et que je ne veux pas sauter ou que je suis au plafond
        if (IsRoofed() && isJumping || IsGrounded() && !isJumping)
            //J'arrête de tomber si je suis au sol
            jumpSpeed = 0;
        //Sinon je chute (je réinitialise la force si avant je montais et que maintenant je tombe
        else if (!isJumping) {
            jumpSpeed -= fallSpeed * 0.33f * time;
        }
        //Sinon je m'envole doucement si je ne suis pas au plafond et que je suis au sol et que je veux sauter
        else if (!IsRoofed()) {
            if(jumpSpeed < 0 ) jumpSpeed = 1;
            else jumpSpeed += fallSpeed * 0.33f * time;
        }
        if (!IsGrounded() && !IsRoofed()) {
            //Si je dépasse pas la rotation voulu de mon vaisseau
            if (transform.rotation.z < 45 && transform.rotation.z > -45)
                rb.rotation = jumpSpeed;
            else
                //Sinon je set la valeur max de la rotation
                rb.rotation = (transform.rotation.z > 0) ? 45 : -45;
        }
        //au sol j'arrondi pour que sa prite soit droite
        else ResetRotation();
    }
    private void ResetRotation(bool total = false) {
        if (!total) {
            if (state == "cube") {
                Vector3 rotation = gameObject.transform.rotation.eulerAngles;
                rotation.z = Mathf.Round(rotation.z / 90) * 90;
                gameObject.transform.rotation = Quaternion.Euler(rotation);
            } 
            //la rotation du vaisseau est fait sur une transition et est toujours basé sur un identity donc elle est a part
            if(state=="ship") transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.identity,time/2);
        }
        //quand je change d'état je me remet a plat sur la rotation originale
        else   gameObject.transform.rotation = Quaternion.identity;
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
        //Sinon je suis "en train de sauter"
        isJumping = true;
    }
    //Je raycast du centre de mon cube vers le sol pour savoir si il est assez proche du sol pour dire qu'il est "grounded"
    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, distanceFromGround + 0.1f, groundMask);
    }
    //Je raycast du centre de mon cube vers le plafond pour savoir si il est assez proche du plafond pour dire qu'il est "roofed"
    private bool IsRoofed() {
        return Physics2D.Raycast(transform.position, Vector2.up, distanceFromCeiling + 0.1f, groundMask);
    }
}