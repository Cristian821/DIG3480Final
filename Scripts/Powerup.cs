using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private GameController gameController;
    public float multiplier;
    public float divider; 
    public float duration;
    private float initialFireRate;
    private PlayerController playerController;




    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
        initialFireRate = playerController.fireRate;
       
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Gotit(other));
        }
    }

   IEnumerator Gotit(Collider player)
    { 

        //PlayerController fireUp = player.GetComponent<PlayerController>();
        playerController.fireRate *= multiplier;        

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;


        yield return new WaitForSeconds(duration);

        playerController.fireRate = initialFireRate;

        Destroy(gameObject);
    }

}