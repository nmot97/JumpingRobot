using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EnemyController : MonoBehaviour {
 
    // Range of movement
    public float rangeX = 2f;
 
    // Speed
    public float speed = 3f;
 
    // Initial direction
    public float direction = 1f;
 
    // To keep the initial position
    Vector3 initialPosition;
 
    // Use this for initialization
    void Start () {
 
        // Initial location in Y
        initialPosition = transform.position;
    }
	
    
    // Update is called once per frame
    void Update() 
    {
        // How much we are moving
        float movementX = direction * speed * Time.deltaTime;
 
        // New position
        float newX = transform.position.x + movementX;
 
        // Check whether the limit would be passed
        if (Mathf.Abs(newX - initialPosition.x) > rangeX)
        {
            // Move the other way
            direction *= -1;
        }
 
        // If it can move further, move
        else
        {
            // Move the object
            transform.Translate(new Vector3(movementX,0 , 0));
        }        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GameManager.instance.HurtPlayer(1);
        }
    }
}