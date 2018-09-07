using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour {

    [HideInInspector]
    public GameManager gameManager;
    public Transform positions;

    int currentPosition = 4;
    public float moveDelay = 1;
    float lastMovement;
    bool isFalling = true;
    int topPosition;

    // Use this for initialization
    void Start ()
    {
        transform.position = positions.GetChild(currentPosition).transform.position;
        lastMovement = Time.time;
        topPosition = positions.childCount;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(moveDelay);
            MovePosition();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    void MovePosition()
    {
        if (currentPosition == 1) // and is not dead
        {
            //Debug.Log("bottom");
            currentPosition++;
            topPosition = Random.Range(positions.childCount - 2, positions.childCount);
            Debug.Log(topPosition);
            isFalling = false;
        }
        else if (currentPosition == topPosition)
        {
            //Debug.Log("top");
            currentPosition--;
            isFalling = true;
        }
        else if (currentPosition < topPosition && isFalling)
        {
            //Debug.Log("down");
            currentPosition--;
        }
        else if (currentPosition < topPosition && !isFalling)
        {
            //Debug.Log("up");
            currentPosition++;
        }

        transform.position = positions.GetChild(currentPosition).transform.position;

        lastMovement = Time.time;

        if (positions.GetChild(currentPosition).GetComponent<FoodPosition>().isLowestPosition)
        {
            Debug.Log("HEJ");
            //if (gameManager.IsEatenByRat(gameObject))
            //{
            //    Debug.Log("hej");
            //    DestroyFood();
            //}
        }
    }

    void DestroyFood()
    {
        Debug.Log("Eaten by rat!");
        Destroy(transform.parent.gameObject);
    }
}
