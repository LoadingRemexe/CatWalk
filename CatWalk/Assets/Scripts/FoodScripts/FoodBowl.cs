using TMPro;
using UnityEngine;

public class FoodBowl : Singleton<FoodBowl>
{
    [SerializeField] GameObject FoodPrefab;
    [SerializeField] GameObject FoodInHandPrefab;
    [SerializeField] Transform LocationOnScreen;
    [SerializeField] TextMeshProUGUI FoodLabel;

    PlayerController playerController;
    Collider2D Collider;

    public int FoodPellets = 5;
    GameObject heldFood = null;
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
            heldFood = Instantiate(FoodInHandPrefab, playerController.GetWorldPoint2D(), Quaternion.identity);
            FoodPellets--;
            AudioManager.Instance.PlaySFX(4);
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
            Destroy(heldFood);
            AddFood(1);
        }
        else
        {
            Destroy(heldFood);
            Instantiate(FoodPrefab, playerController.GetWorldPoint2D(), Quaternion.identity, null);
        }
        AudioManager.Instance.PlaySFX(4);
        heldFood = null;
    }
}
