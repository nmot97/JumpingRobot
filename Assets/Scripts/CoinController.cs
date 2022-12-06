using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public AudioSource coinAudioSource;

    public float rotationSpeed = 100f;
 
    // Update is called once per frame
    void Update()
    {
        //distance (in angles) to rotate on each frame. distance = speed * time
        float angle = rotationSpeed * Time.deltaTime;
 
        //rotate on Y
        transform.Rotate(Vector3.up * angle, Space.World);
    }

    void OnTriggerEnter(Collider collider)
    {
        // Check if we ran into a coin
        if (collider.gameObject.tag == "Player")
        {
            print("Grabbing coin..");

            GameManager.instance.IncreaseScore(1);
            
            // Play coin collection sound
            coinAudioSource.Play();

            // Destroy coin
            Destroy(gameObject); 
        }
    }
}
