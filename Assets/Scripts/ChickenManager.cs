using UnityEngine;

public class ChickenManager : MonoBehaviour
{
    public enum ChickenUpdate
    {
        NewestDied,
        KillOldest
    }
    public static readonly int maxChickens = 2;
    public int ChickenNumber {get; set;} = 0;

    private void OnEnable()
    {
        PlayerController.ChickenUpdate += HandleChickenUpdate;
        EggController.ChickenUpdate += HandleChickenUpdate;
    }

    private void OnDisable()
    {
        PlayerController.ChickenUpdate -= HandleChickenUpdate;
        EggController.ChickenUpdate -= HandleChickenUpdate;
    }

    private void HandleChickenUpdate(ChickenUpdate u)
    {
        if (u == ChickenUpdate.KillOldest)
        {
            ChickenNumber -= 1;
            if (ChickenNumber < 0)
            {
                Destroy(gameObject);
            }
        }
        else if (u == ChickenUpdate.NewestDied)
        {
            if (ChickenNumber == 0)
            {
                GetComponent<PlayerController>().enabled = true;
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

}
