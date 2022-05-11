using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    public int fuelCost = 1;
    public Animator drill_animator;

    public ParticleSystem particleEffect;
    public ParticleSystem particleEffectSparks;

    private Rigidbody2D _rigidBody;
    private CharacterDig characterDigScript;
    private PlayerController PlayerController;
    private float halfPlayerSizeX;

    Vector2 initial_movement;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        characterDigScript = GetComponent<CharacterDig>();
        PlayerController = GetComponent<PlayerController>();
        halfPlayerSizeX = GetComponent<BoxCollider2D>().bounds.size.x / 4;
    }

    void FixedUpdate()
    {
        initial_movement.x = Input.GetAxisRaw("Horizontal");
        initial_movement.y = Input.GetAxisRaw("Vertical");

        //move player
        var movement = initial_movement * Time.deltaTime * MovementSpeed;
        transform.position += new Vector3(movement.x,0,0) ;
        clampPlayerMovement();

        //play jetpack audio if flying
        AudioManager audioManager =  FindObjectOfType<AudioManager>();
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)){
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, JumpForce);
            PlayerController.useFuel(fuelCost);
            particleEffect.Emit(1);
            particleEffectSparks.Emit(1);            
            if(!audioManager.IsPlaying("Jetpack")){
                audioManager.Play("Jetpack");
            }
        } else {
            if(audioManager.IsPlaying("Jetpack")){
                audioManager.Stop("Jetpack");
            }
        }

        //play animations
        if(initial_movement.x != 0 || initial_movement.y != 0){
            if(characterDigScript.isGrounded()){
                drill_animator.SetFloat("Vertical", initial_movement.y); 
                drill_animator.SetFloat("Horizontal", initial_movement.x);     
            } else {
                drill_animator.SetFloat("Horizontal", 0);
                drill_animator.SetFloat("Vertical", 1); 
            }  
        }
       
    }

    void clampPlayerMovement()
    {
        Vector3 position = transform.position;

        float distance = transform.position.z - Camera.main.transform.position.z;

        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + halfPlayerSizeX;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - halfPlayerSizeX;

        position.x = Mathf.Clamp(position.x, leftBorder, rightBorder);
        transform.position = position;
    }
}

