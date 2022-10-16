using UnityEngine;

public class EggController : MonoBehaviour
{
    public static event Delegates.SignalCamera signalCamera;
    public static event Delegates.ChickenUpdate ChickenUpdate;

    public int ChickenNumber {get; set;}

    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        if (signalCamera != null)
        {
            signalCamera(transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "nest")
        {
            GameObject player =Instantiate(playerPrefab, transform.position, Quaternion.identity);
            player.GetComponent<ChickenManager>().ChickenNumber = ChickenNumber;
            if (ChickenNumber >= ChickenManager.maxChickens && ChickenUpdate != null)
            {
                ChickenUpdate(ChickenManager.ChickenUpdate.KillOldest);
            }
            Destroy(gameObject);
        }
    }
}
