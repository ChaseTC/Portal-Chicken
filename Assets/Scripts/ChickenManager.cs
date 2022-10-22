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
                GetComponent<Animator>().SetTrigger("Death");
            }
        }
        else if (u == EventManager.ChickenUpdateType.ChickenDied)
        {
            if (ChickenNumber == chickenNumber - 1)
            {
                gameObject.tag = "player";
                GetComponent<PlayerController>().enabled = true;
            }
        }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

}
