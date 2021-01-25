using TMPro;
using UnityEngine;

public class FoodBowl : MonoBehaviour
{
    [SerializeField] GameObject FoodPrefab;
    [SerializeField] Transform LocationOnScreen;
    [SerializeField] TextMeshProUGUI FoodLabel;

    PlayerController playerController;
    Collider2D Collider;

    public int FoodPellets = 5;
    FoodPellet heldFood = null;
    void Start()
    {
        playerController = PlayerController.Instance;
        Collider = GetComponent<Collider2D>();
    }
    void Update()
    {
        transform.position = playerController.GetWorldPoint2D(LocationOnScreen.position);
        if (heldFood)
        {
            heldFood.transform.position = playerController.GetWorldPoint2D();
        }
        FoodLabel.text = "Food: " + FoodPellets;
    }

    void OnMouseUp()
    {
        if (heldFood) DropFood();
    }

    void OnMouseDown()
    {
        if (FoodPellets > 0)
        {
            heldFood = Instantiate(FoodPrefab, playerController.GetWorldPoint2D(), Quaternion.identity).GetComponent<FoodPellet>();
            FoodPellets--;
        }
    }

    public void AddFood(int pelletCount)
    {
        FoodPellets += pelletCount;
    }
    void DropFood()
    {
        if (Collider.IsTouching(heldFood.GetComponent<Collider2D>()))
        {
            heldFood.Remove();
            AddFood(1);
        }
        else
        {
            heldFood.Dropped();
        }
        heldFood = null;
    }
}
