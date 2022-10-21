using UnityEngine;
using UnityEngine.SceneManagement;

public class EggController : MonoBehaviour
{
    public int ChickenNumber { get; set; }
    private CircleCollider2D coll;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private LayerMask nest;
    private Rigidbody2D rb;

    private void Start()
    {
        EventManager.TriggerSignalCamera(transform);
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
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
        
        if (tag == "nest" && IsGrounded())
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

    private bool IsGrounded()
    {
        return Physics2D.Raycast(coll.bounds.center, Vector2.down, coll.radius + 0.1f, nest);
    }
    private void DestroyEgg()
    {   
        if (ChickenNumber == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            EventManager.ResetActive();
        }
        else {
            EventManager.TriggerChickenUpdate(EventManager.ChickenUpdateType.ChickenDied, ChickenNumber);
            Destroy(gameObject);
        }
    }
}
