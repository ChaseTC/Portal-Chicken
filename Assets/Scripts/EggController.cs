using UnityEngine;
using UnityEngine.SceneManagement;

public class EggController : MonoBehaviour
{
    public int ChickenNumber { get; set; }

    [SerializeField] private GameObject playerPrefab;
    private Rigidbody2D rb;

    private void Start()
    {
        EventManager.TriggerSignalCamera(transform);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.up = new Vector3(rb.velocity.x, rb.velocity.y, transform.up.z);
        if (Input.GetKeyDown("f"))
        {
            DestroyEgg();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "nest")
        {
            GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
            player.GetComponent<ChickenManager>().ChickenNumber = ChickenNumber;
            if (ChickenNumber >= ChickenManager.maxChickens)
            {
                EventManager.TriggerChickenUpdate(EventManager.ChickenUpdateType.KillOldest, 0);
            }
            Destroy(gameObject);
        }
        if (tag == "road")
        {
            DestroyEgg();
        }
    }

    private void DestroyEgg()
    {   
        if (ChickenNumber == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else {
            EventManager.TriggerChickenUpdate(EventManager.ChickenUpdateType.ChickenDied, ChickenNumber);
            Destroy(gameObject);
        }
    }
}
