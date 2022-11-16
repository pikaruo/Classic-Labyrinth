using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koin : MonoBehaviour
{

    [SerializeField] inventory player;
    [SerializeField] AudioSource sfx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.coin++;
            sfx.Play();
            Destroy(gameObject);
        }
    }
}
