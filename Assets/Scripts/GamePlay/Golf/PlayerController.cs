using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Ball ball;
    [SerializeField] LayerMask ballLayer;
    [SerializeField] LayerMask rayLayer;
    [SerializeField] FollowBall cameraPivot;
    [SerializeField] Camera cam;
    [SerializeField] Vector2 campSensitivity;
    [SerializeField] float shootForce;
    [SerializeField] GameObject arrow;
    [SerializeField] TMP_Text shootCounterText;
    [SerializeField] Image aim;
    [SerializeField] LineRenderer line;

    [SerializeField] AudioSource bolaDisodok;
    [SerializeField] AudioSource teleportAudio;

    Vector3 lastMousePosititon;
    float ballDistance;
    bool isShooting;

    Vector3 forceDir;
    float forceFactor;

    Renderer[] arrowRends;
    Color[] arrowOriginalColors;
    int shootCount = 0;
    public int ShootCount { get => shootCount; }

    private void Start()
    {
        ballDistance = Vector3.Distance(
            cam.transform.position, ball.Position
        ) + 1;
        arrow.SetActive(false);
        arrowRends = arrow.GetComponentsInChildren<Renderer>();

        arrowOriginalColors = new Color[arrowRends.Length];
        for (int i = 0; i < arrowRends.Length; i++)
        {
            arrowOriginalColors[i] = arrowRends[i].material.color;
        }

        shootCounterText.text = "Shoot Count: " + shootCount;


        line.enabled = false;

    }

    void Update()
    {
        if (ball.IsTeleporting)
        {
            teleportAudio.Play();
        }
        if (ball.IsMoving)
            return;


        // if (!cameraPivot.IsMoving && aim.gameObject.activeInHierarchy == false)
        // {
        aim.gameObject.SetActive(true);
        var rectx = aim.GetComponent<RectTransform>();
        rectx.anchoredPosition = cam.WorldToScreenPoint(ball.Position);
        // }

        if (this.transform.position != ball.Position)
        {
            this.transform.position = ball.Position;
            // mengaktifkan aim
            aim.gameObject.SetActive(true);
            var rect = aim.GetComponent<RectTransform>();
            rect.anchoredPosition = cam.WorldToScreenPoint(ball.Position);
        }

        if (Input.GetMouseButtonDown(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, ballDistance, ballLayer))
            {
                isShooting = true;
                arrow.SetActive(true);
                line.enabled = true;
            }
        }

        // shooting mode
        if (Input.GetMouseButton(0) && isShooting == true)
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, ballDistance * 2, rayLayer))
            {
                Debug.DrawLine(ball.Position, hit.point);

                var forceVector = ball.Position - hit.point;
                forceVector = new Vector3(forceVector.x, 0, forceVector.z);
                forceDir = forceVector.normalized;
                var forceMagnitude = forceVector.magnitude;
                Debug.Log(forceMagnitude);
                forceMagnitude = Mathf.Clamp(forceMagnitude, 0, 5);
                forceFactor = forceMagnitude / 5;
            }

            // arrow
            this.transform.LookAt(this.transform.position + forceDir);
            arrow.transform.localScale = new Vector3(1 + 0.5f * forceFactor, 1 + 0.5f * forceFactor, 1 + 2 * forceFactor);

            for (int i = 0; i < arrowRends.Length; i++)
            {
                arrowRends[i].material.color = Color.Lerp(arrowOriginalColors[i], Color.red, forceFactor);
            }

            // aim
            var rect = aim.GetComponent<RectTransform>();
            rect.anchoredPosition = Input.mousePosition;

            // line
            var ballScrPos = cam.WorldToScreenPoint(ball.Position);
            Debug.DrawLine(ballScrPos, Input.mousePosition, Color.red);
            line.SetPositions(new Vector3[]
            {
                ballScrPos,
                Input.mousePosition
            });
        }

        // camera mode
        if (Input.GetMouseButton(0) && isShooting == false)
        {
            var current = cam.ScreenToViewportPoint(Input.mousePosition);
            var last = cam.ScreenToViewportPoint(lastMousePosititon);
            var delta = current - last;

            // rotate horizontal
            cameraPivot.transform.RotateAround(ball.Position, Vector3.up, -delta.x * campSensitivity.x);
            // rotate vertical
            cameraPivot.transform.RotateAround(ball.Position, transform.right, -delta.y * campSensitivity.y);

            // angle camera
            var angle = Vector3.SignedAngle(Vector3.up, cam.transform.up, cam.transform.right);

            // kalau melewati batas putar
            // if (angle < 3 || angle > 65)
            // {
            //     cam.transform.RotateAround(ball.Position, transform.right, delta.y * campSensitivity.y);
            // }
            if (angle < 3)
            {
                cameraPivot.transform.RotateAround(ball.Position, transform.right, 3 - angle);
            }
            else if (angle > 65)
            {
                cameraPivot.transform.RotateAround(ball.Position, transform.right, 65 - angle);
            }
        }

        if (Input.GetMouseButtonUp(0) && isShooting)
        {
            ball.AddForce(forceDir * shootForce * forceFactor);
            bolaDisodok.Play();
            shootCount += 1;
            shootCounterText.text = "Shoot Count: " + shootCount;
            forceFactor = 0;
            forceDir = Vector3.zero;
            isShooting = false;
            arrow.SetActive(false);

            aim.gameObject.SetActive(false);
            line.enabled = false;
        }
        lastMousePosititon = Input.mousePosition;
    }
}
