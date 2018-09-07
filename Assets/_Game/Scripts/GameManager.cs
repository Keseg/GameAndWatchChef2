using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject FoodPrefab;
    public GameObject ChefPrefab;
    public LifeController lifeController;
    public ScoreController scoreController;

    public Vector3 foodOneLocation = new Vector3(-5, 0, 0);
    public Vector3 foodTwoLocation = new Vector3(0, 0, 0);
    public Vector3 foodThreeLocation = new Vector3(5, 0, 0);

    Collider2D ChefCollider;
    bool continueGame = true;

	// Use this for initialization
	void Start ()
    {
        ChefCollider = ChefPrefab.GetComponentInChildren<Collider2D>();

        lifeController.RestoreAllLives();

        StartCoroutine(FoodSpawner());
    }

    IEnumerator FoodSpawner()
    {
        StartCoroutine(NewFood());

        yield return new WaitForSeconds(1f);
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    IEnumerator NewFood()
    {
        GameObject newFood1 = Instantiate(FoodPrefab, foodOneLocation, transform.rotation);
        yield return new WaitForSeconds(0.3f);
        GameObject newFood2 = Instantiate(FoodPrefab, foodTwoLocation, transform.rotation);
        yield return new WaitForSeconds(0.3f);
        GameObject newFood3 = Instantiate(FoodPrefab, foodThreeLocation, transform.rotation);
        //FoodController foodController = newFood1.GetComponentInChildren<FoodController>();
        //foodController.gameManager = this;
        //jumperController.moveDelay = delay;
    }

    //public bool IsEatenByRat(GameObject Food)
    //{

    //    Collider2D FoodCollider = FoodPrefab.GetComponentInChildren<Collider2D>();

    //    if (FoodCollider == null || ChefCollider == null)
    //    {
    //        Debug.Log("Collider not found");
    //    }

    //    if (FoodCollider.IsTouching(ChefCollider))
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        Debug.Log("Eaten by rat");
    //        return true;
    //    }
    //}

    public bool IsEatenByRat(GameObject food)
    {
        LayerMask mask = LayerMask.GetMask("Chef");
        RaycastHit2D hit = Physics2D.Raycast(food.transform.position, Vector2.down, Mathf.Infinity, mask);
        if (hit.collider != null)
        {
            Debug.Log("Saved!");
            return false;
        }
        else
        {
            LoseOneLife();
            Debug.Log("Nom nom");
            return true;
        }
    }

    void LoseOneLife()
    {
        if (!lifeController.RemoveLife())
        {
            Debug.Log("Game Over!");
            continueGame = false;
        }
    }
}

