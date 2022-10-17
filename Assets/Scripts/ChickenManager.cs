using UnityEngine;

public class ChickenManager : MonoBehaviour
{
    public static readonly int maxChickens = 2;
    public int ChickenNumber { get; set; } = 0;

    private void OnEnable()
    {
        EventManager.chickenUpdate += HandleChickenUpdate;
    }

    private void OnDisable()
    {
        EventManager.chickenUpdate -= HandleChickenUpdate;
    }

    private void HandleChickenUpdate(EventManager.ChickenUpdateType u, int chickenNumber)
    {
        if (u == EventManager.ChickenUpdateType.KillOldest)
        {
            ChickenNumber -= 1;
            if (ChickenNumber < 0)
            {
                Destroy(gameObject);
            }
        }
        else if (u == EventManager.ChickenUpdateType.ChickenDied)
        {
            if (ChickenNumber == chickenNumber - 1)
            {
                GetComponent<PlayerController>().enabled = true;
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

}
