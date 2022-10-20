using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{

    private Animator anim;
    private SpriteRenderer sr;


    private static float cooldownTime = 1;
    private bool onCooldown = false;
    [SerializeField] private bool isActive = true;
    [SerializeField] private int frequency = -1;
    [SerializeField] private Color color;

    private void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        if (isActive)
        {
            sr.color = color;
            anim.SetBool("Active", true);
        }
    }

    private void Update()
    {
        if (frequency != - 1 && EventManager.isActive(frequency))
        {
            color = EventManager.GetColor(frequency);
            sr.color = color;
            isActive = true;
        }
        else if (frequency != - 1)
        {
            sr.color = Color.white;
            isActive = false;
        }
        anim.SetBool("Active", isActive);
    }

    private void OnEnable()
    {
        EventManager.portalTeleport += HandlePortalTeleport;
    }

    private void OnDisable()
    {
        EventManager.portalTeleport -= HandlePortalTeleport;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isActive && !onCooldown && col.gameObject.tag == "egg")
        {
            Vector2 eggVelocity = col.gameObject.GetComponent<Rigidbody2D>().velocity;
            float angle = Vector2.Angle(transform.right, eggVelocity);
            if (angle > 90)
            {
                EventManager.TriggerPortalTeleport(gameObject.GetInstanceID(), color);
            }
        }
    }

    private IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
    }

    private void HandlePortalTeleport(int from, Color c)
    {
        if (isActive && gameObject.GetInstanceID() != from && color == c)
        {
            GameObject egg = GameObject.FindGameObjectWithTag("egg");
            Rigidbody2D eggrb = egg.GetComponent<Rigidbody2D>();
            float vx = eggrb.velocity.x;
            float vy = vy = eggrb.velocity.y;
            if ((transform.right == Vector3.right && vx < 0) || (transform.right == Vector3.left && vx > 0))
            {
                vx *= -1;
            }
            else if ((transform.right == Vector3.up && vy < 0) || (transform.right == Vector3.down && vy > 0))
            {
                vy *= -1;
            }
            eggrb.velocity = new Vector2(vx, vy);
            egg.transform.position = transform.position;
            StartCoroutine(Cooldown());
        }
    }
}
