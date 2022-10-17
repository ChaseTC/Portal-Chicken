using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "player" || col.gameObject.tag == "egg")
        {
            PlayerController.EggCount += 1;
            Destroy(gameObject);
        }
    }
}
