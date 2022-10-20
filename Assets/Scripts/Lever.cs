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
            ToggleLever();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "player")
        {
            playerInRange = false;
        }
    }

    private void ToggleLever()
    {
        isOn = !isOn;
        if (isOn)
        {
            EventManager.Activate(frequency, color);
            sr.sprite = on;
        }
        else
        {
            EventManager.Deactivate(frequency, color);
            sr.sprite = off;
        }
    }
}
