using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MoveLeft : MonoBehaviour
{
    private float speed = 10.0f;
    private PlayerController playerControllerScript;
    private float leftBound = -15;
    public bool isDestroyGameObject = false;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( !playerControllerScript.gameOver && !playerControllerScript.isIdling ) {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.x < leftBound && (gameObject.CompareTag("Obstacle") 
            || gameObject.CompareTag("Box") 
            || gameObject.CompareTag("Hole")
            || gameObject.CompareTag("Bomb")
            || gameObject.CompareTag("Dimond"))
            )
        {
            Destroy(gameObject);
            isDestroyGameObject = true;
        }
    }
}
