using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private AudioSource collectCrackSoundEffect;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "player" || col.gameObject.tag == "egg")
        {
            AudioSource.PlayClipAtPoint(collectCrackSoundEffect.clip, transform.position);
            PlayerController.EggCount += 1;
            Destroy(gameObject);
        }
    }
}
