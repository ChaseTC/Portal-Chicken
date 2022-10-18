using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private Sprite off;
    [SerializeField] private Sprite on;

    [SerializeField] private int frequency;
    [SerializeField] private Color color;

    private bool isOn = false;
    private bool playerInRange = false;

    private SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = color;
    }


    private void Update()
    {
       // Debug.Log(playerInRange);
        if (playerInRange && Input.GetKeyDown("e"))
        {

            ToggleLever();
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "player")
        {
            playerInRange = true;
        }
        if (col.gameObject.tag == "egg" && !playerInRange)
        {
            Debug.Log("Here " + playerInRange);
            ToggleLever();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Triggered Exit: " + col.gameObject.name);
        if (col.gameObject.tag == "player")
        {
            playerInRange = false;
        }
    }

    private void ToggleLever()
    {
        Debug.Log("Called");
        EventManager.TriggerWireSignal(frequency, color);
        isOn = !isOn;
        if (isOn)
        {
            sr.sprite = on;
        }
        else
        {
            sr.sprite = off;
        }
    }
}
