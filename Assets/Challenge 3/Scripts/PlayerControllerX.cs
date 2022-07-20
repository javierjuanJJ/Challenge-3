using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControllerX : MonoBehaviour
{
    private bool gameOver, isTooHigh, isLowHigh;

    public float floatForce;
    
    public float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;
    
    public BoxCollider highBackground;
    
    private float floatHighBackground, floatLowBackground;
    
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        
        floatHighBackground = highBackground.size.y;
        floatLowBackground = 1;
        
        isTooHigh = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        float positionY = transform.position.y;
        
        isTooHigh = positionY > floatHighBackground;
        isLowHigh = positionY < floatLowBackground;
        
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver && !isTooHigh && !isLowHigh)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
        }
        
        
        if (isTooHigh || isLowHigh)
        {
            playerAudio.PlayOneShot(bounceSound, 1.0f);
            Vector3 directionVector = isTooHigh ? Vector3.down : Vector3.up;
            playerRb.AddForce(directionVector * floatForce, ForceMode.Impulse);
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.transform.position = transform.position;
            explosionParticle.Play();
            
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            
            gameOver = true;
            
            Debug.Log("Game Over!");
            
            Destroy(other.gameObject);
            Destroy(gameObject,0.5f);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.transform.position = transform.position;
            fireworksParticle.Play();
            
            playerAudio.PlayOneShot(moneySound, 1.0f);
            
            Destroy(other.gameObject);
        }

    }


    public bool GameOver => gameOver;
}
