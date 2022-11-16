using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHole : MonoBehaviour
{
    bool entered = false;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject score;
    [SerializeField] AudioSource bolaMasukLubang;



    public bool Entered { get => entered; }

    private void OnTriggerEnter(Collider other)
    {
        entered = true;
        ball.SetActive(false);
        score.SetActive(false);
        bolaMasukLubang.Play();
        Debug.Log("ENTER");
    }
}
