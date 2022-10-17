using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float maxZoom = 10;
    [SerializeField] private float minZoom = 20;
    [SerializeField] private float zoomSensitivity = 1;

    [SerializeField] private float moveSensitivity = 1f;
    [SerializeField] private float speed = 30;
    private float targetZoom;
    private Transform following;

    public static bool freeCam = false;

    private void OnEnable()
    {
        EventManager.signalCamera += SetFollowing;
    }

    private void OnDisable()
    {
        EventManager.signalCamera -= SetFollowing;
    }

    private void Start()
    {
        targetZoom = cam.orthographicSize;
    }
    private void Update()
    {
        PositionCamera();
        Zoom();
    }

    private void Zoom()
    {
        targetZoom -= Input.mouseScrollDelta.y * zoomSensitivity;
        targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
        float newSize = Mathf.MoveTowards(cam.orthographicSize, targetZoom, speed * Time.deltaTime);
        cam.orthographicSize = newSize;
    }

    private void SetFollowing(Transform follow)
    {
        following = follow;
    }

    private void PositionCamera()
    {
        if (Input.GetKeyDown("q"))
        {
            freeCam = !freeCam;
            if (freeCam)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        if (!freeCam)
        {
            transform.position = new Vector3(following.position.x, following.position.y, transform.position.z);
        }
        else
        {

            transform.position = new Vector3(transform.position.x + Input.GetAxis("Mouse X") * moveSensitivity, transform.position.y + Input.GetAxis("Mouse Y") * moveSensitivity, transform.position.z);
        }

    }
}
