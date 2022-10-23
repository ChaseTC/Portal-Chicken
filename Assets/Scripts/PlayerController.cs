using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private Rigidbody2D eggrb;
    private LineRenderer lr;
    private BoxCollider2D coll;

    private ChickenManager cm;

    public static int EggCount { get; set; } = 100;

    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float jumpForce = 20;
    [SerializeField] private float shootPower = 5;

    [SerializeField] private float maxEggXVelocity = 15;
    [SerializeField] private float maxEggYVelocity = 30;

    [SerializeField] private GameObject eggPrefab;

    [SerializeField] private LayerMask terrain;
    [SerializeField] private LayerMask nest;

    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource shootSoundEffect;
    [SerializeField] private AudioSource eggCrackSoundEffect;
    [SerializeField] private AudioSource deathSoundEffect;

    private bool isAiming = false;

    private Vector2 velocity;

    private enum state {idle, running, shooting};
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        eggrb = eggPrefab.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        coll = GetComponent<BoxCollider2D>();
        cm = GetComponent<ChickenManager>();
        EventManager.TriggerSignalCamera(transform);
    }
    private void OnEnable()
    {
        EventManager.TriggerSignalCamera(transform);
        isAiming = false;
    }

    private void Update()
    {
        if (!CameraController.freeCam)
        {
            HandleKilling();
            HandleMovement();
            Transform();
            HandleShooting();
        }
    }


    private void Transform()
    {
        if (!isGrounded() && !Input.GetKeyDown("f"))
        {
            GameObject egg = Instantiate(eggPrefab, transform.position, Quaternion.identity);
            egg.GetComponent<EggController>().ChickenNumber = cm.ChickenNumber;
            egg.GetComponent<Rigidbody2D>().velocity = rb.velocity;
            Destroy(gameObject);
            eggCrackSoundEffect.Play();
        }
    }
    private void HandleMovement()
    {
        float dirx = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirx * moveSpeed, rb.velocity.y);
       
        if (rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
        if (rb.velocity.x > 0)
        {
            sr.flipX = false;
        }
        if (rb.velocity.x != 0)
        {
            anim.SetInteger("State", (int) state.running);
        }
        else
        {
            anim.SetInteger("State", (int) state.idle);
        }

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpSoundEffect.Play();
        }
    }
    private void HandleShooting()
    {
        if (EggCount > 0)
        {
            if (isAiming)
            {
                lr.enabled = true;
                velocity = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position) * shootPower;
                if (velocity.x > maxEggXVelocity)
                {
                    velocity.x = maxEggXVelocity;
                }
                if (velocity.x < -1 * maxEggXVelocity)
                {
                    velocity.x = -1 * maxEggXVelocity;
                }
                if (velocity.y > maxEggYVelocity)
                {
                    velocity.y = maxEggYVelocity;
                }
                if (velocity.y < -1 * maxEggYVelocity)
                {
                    velocity.y = -1 * maxEggYVelocity;
                }
                List<Vector3> trajectory = GetTrajectory(eggrb, transform.position, velocity, 500, 5);
                lr.positionCount = trajectory.Count;
                lr.SetPositions(trajectory.ToArray());

                if (Input.GetButtonDown("Fire1"))
                {
                    sr.flipX = (velocity.x > 0);
                    anim.SetInteger("State", (int) state.shooting);
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    isAiming = false;
                    lr.enabled = false;
                }
            }
            if (!isAiming && Input.GetButtonDown("Fire1"))
            {
                isAiming = true;
            }
        }
    }

    private void ShootEgg()
    {
        shootSoundEffect.Play();
        EggCount -= 1;
        GameObject egg = Instantiate(eggPrefab, transform.position, Quaternion.identity);
        egg.GetComponent<EggController>().ChickenNumber = cm.ChickenNumber + 1;
        egg.GetComponent<Rigidbody2D>().velocity = velocity;
        lr.enabled = false;
        isAiming = false;
        gameObject.tag = "old player";
        GetComponent<PlayerController>().enabled = false;
    }
    private void HandleKilling()
    {
        if (Input.GetKeyDown("f"))
        {
            if (cm.ChickenNumber != 0)
            {
                anim.SetTrigger("DeathSignal");
                deathSoundEffect.Play();
            }
        }
    }

    private void KillAndSignal()
    {
        EventManager.TriggerChickenUpdate(EventManager.ChickenUpdateType.ChickenDied, cm.ChickenNumber);
        Destroy(gameObject);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, .1f, nest);
    }

    private List<Vector3> GetTrajectory(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps, int stepsize)
    {
        List<Vector3> results = new List<Vector3>();

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations * stepsize;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;
        float drag = 1f - timestep * rigidbody.drag;
        Vector2 moveStep = velocity * timestep;

        results.Add(pos);
        for (int i = 1; i < steps / stepsize + 1; ++i)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            RaycastHit2D hit = Physics2D.BoxCast(results[i - 1], new Vector2(0.3f, 0.3f), 0, pos.normalized, moveStep.magnitude, terrain | nest);
            results.Add(pos);
            if (hit.collider != null)
            {
                break;
            }
        }

        return results;
    }

    public void SetChickenNumber(int n)
    {
        cm.ChickenNumber = n;
    }
}
