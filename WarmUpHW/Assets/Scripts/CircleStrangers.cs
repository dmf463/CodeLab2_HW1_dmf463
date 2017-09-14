using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleStrangers : MonoBehaviour {

    public bool isJumping;
    public float jump = 15;
    GameObject player;
    public static float survivorNum;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
		
	}
	
	// Update is called once per frame
	void Update () {

        if(survivorNum == player.GetComponent<Player>().friends.Count)
        {
            Debug.Log("You saved " + survivorNum + " friends!");
        }
		
	}

    public void OnCollisionEnter2D(Collision2D other)
    {
        isJumping = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Death")
        {
            if(gameObject.name == "Player" && !player.GetComponent<Player>().endGame)
            {
                Debug.Log("GAME OVER MOTHERFUCKER YOU LET EVERYONE DOWN");
            }
            else
            {
                player.GetComponent<Player>().friends.Remove(gameObject);
                player.GetComponent<Player>().friendNum = (player.GetComponent<Player>().friendNum + 1) % player.GetComponent<Player>().friends.Count;
                Destroy(gameObject);
            }
        }
        if(other.gameObject.tag == "Goal")
        {
            survivorNum++;
            GetComponent<SpriteRenderer>().enabled = false;
            player.GetComponent<Player>().endGame = true;
            player.GetComponent<Player>().mainCamera.transform.position = new Vector3(other.gameObject.transform.position.x, 
                                                                                      other.gameObject.transform.position.y,
                                                                                      -10);
        }
    }
}
