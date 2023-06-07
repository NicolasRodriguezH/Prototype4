using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500;
    private GameObject focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    private Coroutine powerUpCountdown;

    public int powerUpDuration = 5;

    public ParticleSystem speedIncreaseParticle;

    private float normalStrength = 10.0f; // how hard to hit enemy without powerup
    private float powerupStrength = 25.0f; // how hard to hit enemy with powerup
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Hacia falta, instanciar la referencia del ParticleSystem en metodo Update y llamar el Play() method;
            speedIncreaseParticle.Play();
            // Add force to player in direction of the focal point (and camera)
            Instantiate(speedIncreaseParticle, transform.position, speedIncreaseParticle.transform.rotation);
            float verticalSmashInput = Input.GetAxis("Vertical");
            playerRb.AddForce(focalPoint.transform.forward * verticalSmashInput * speed * 2 * Time.deltaTime, ForceMode.Impulse);
        }

        // Aca instancian las particulas en la posicion del Player
        speedIncreaseParticle.transform.position = transform.position + new Vector3(0, -0.6f, 0);

        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime); 

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
        }
        if (powerUpCountdown != null)
        {
            StopCoroutine(powerUpCountdown);
        }
        powerUpCountdown = StartCoroutine(PowerupCooldown());
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }


        }
    }



}
