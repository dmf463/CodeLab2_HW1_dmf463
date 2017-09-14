using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float speed;
    public float distanceBetweenFriends;
    public Camera mainCamera;
    public List<GameObject> friends = new List<GameObject>();
    public int friendNum;
    public GameObject jumpIndicator;
    public Vector3 jumpIndicatorOffset;
    public bool endGame;
    public Text time;
    float elapsedTime = 0;

	// Use this for initialization
	void Start () {

        friends.Add(gameObject);
    }
	
	// Update is called once per frame
	void Update () {

        elapsedTime += Time.deltaTime;
        time.text = (elapsedTime).ToString("F0");

        if (!endGame)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
        jumpIndicator.transform.position = friends[friendNum].transform.position + jumpIndicatorOffset;

        for (int i = 1; i < friends.Count; i++)
        {
            friends[i].transform.position = 
                new Vector3(transform.position.x - (distanceBetweenFriends * i), friends[i].transform.position.y);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed;
        }

        if (Input.GetMouseButtonDown(0) && friends[friendNum].GetComponent<CircleStrangers>().isJumping == false)
        {
            friends[friendNum].GetComponent<Rigidbody2D>().velocity = new Vector3(0, 1) * friends[friendNum].GetComponent<CircleStrangers>().jump;
            friends[friendNum].GetComponent<CircleStrangers>().isJumping = true;
            friendNum = (friendNum + 1) % friends.Count;
            Debug.Log("friendNum = " + friendNum + " and friends.Count = " + friends.Count);
        }

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Friend" && !friends.Contains(other.gameObject))
        {
            other.transform.position = new Vector3(transform.position.x - (distanceBetweenFriends * friends.Count), other.transform.position.y);
            other.transform.parent = transform;
            friends.Add(other.gameObject);
        }
    }
}
