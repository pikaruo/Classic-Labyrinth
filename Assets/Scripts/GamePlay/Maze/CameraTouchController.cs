using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchController : MonoBehaviour
{

    [SerializeField, Range(0, 20)] float filterFactor = 1;
    [SerializeField, Range(0, 10)] float dragFactor = 1;
    [SerializeField, Range(0, 2)] float zoomFactor = 1;
    [Tooltip("equal camera y position")]
    [SerializeField] float minCamPos = 70;
    [SerializeField] float maxCamPos = 150;
    [SerializeField] Collider topCollider;
    float distance;

    void Start()
    {
        distance = this.transform.position.y;
    }

    Vector3 touchBeganWorldPos;
    Vector3 cameraBeganWorldPos;

    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        var touch0 = Input.GetTouch(0);

        // simpan posisi awal tapi posisi real world
        // TODO Old Code
        // if (touch0.phase == TouchPhase.Began)
        // {
        //     touchBeganWorldPos = Camera.main.ScreenToWorldPoint(
        //         new Vector3(touch0.position.x, touch0.position.y, distance)
        //     );
        //     cameraBeganWorldPos = this.transform.position;
        // }

        // atur posisi sekarang sesuai perubahan dari posisi began
        if (Input.touchCount == 1 && touch0.phase == TouchPhase.Moved)
        {

            // TODO New Code - posisi pada frame sebelumnya bergerak
            var touchPrevPos = touch0.position - touch0.deltaPosition;
            var touchPreWorldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(touchPrevPos.x, touchPrevPos.y, distance)
            );

            // posisi touch saat ini
            var touchMovedWorldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(touch0.position.x, touch0.position.y, distance)
            );

            // perbedaan posisi
            // var delta = touchMovedWorldPos - touchBeganWorldPos;
            var delta = touchMovedWorldPos - touchPreWorldPos;

            // menggunakan lerp sebagai filter agar move smooth
            // var targetPos = cameraBeganWorldPos - delta * dragFactor;
            var targetPos = this.transform.position - delta * 0.5f;

            // clamp targetpos
            targetPos = new Vector3(
                Mathf.Clamp(targetPos.x, topCollider.bounds.min.x, topCollider.bounds.max.x),
                targetPos.y,
                Mathf.Clamp(targetPos.z, topCollider.bounds.min.z, topCollider.bounds.max.z)
            );

            // this.transform.position = Vector3.Lerp(
            //     this.transform.position,
            //     targetPos,
            //     Time.deltaTime * filterFactor
            // );
            this.transform.position = targetPos;
        }

        if (Input.touchCount < 2)
        {
            return;
        }

        var touch1 = Input.GetTouch(1);
        // zoom
        if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
        {
            var touch0PrevPos = touch0.position - touch0.deltaPosition;
            var touch1PrevPos = touch1.position - touch1.deltaPosition;
            var prevDistance = Vector3.Distance(touch0PrevPos, touch1PrevPos);
            var currDistance = Vector3.Distance(touch0.position, touch1.position);
            var delta = currDistance - prevDistance;

            this.transform.position -= new Vector3(0, delta * zoomFactor, 0);

            // batas zoom
            this.transform.position = new Vector3(
                this.transform.position.x,
                Mathf.Clamp(this.transform.position.y, minCamPos, maxCamPos),
                this.transform.position.z
            );

            distance = this.transform.position.y;
        }

    }
}
