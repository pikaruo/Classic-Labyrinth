using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMove : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] float force;

    Vector3 moveDir;

    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        if (moveDir != Vector3.zero)
            rb.AddForce(moveDir * force, ForceMode.Force);
    }
}
