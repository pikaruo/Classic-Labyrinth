using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHole : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTER");
    }
}
