using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDoor : MonoBehaviour
{
    public Animator doorUpAnim;
    public AudioSource doorAudioSource;
    public AudioClip doorUpSound;

    public StealItem player;

    public bool doorOpen = false;

    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && player.hasCard1 == true && Input.GetKey(KeyCode.E) && doorOpen == false)
        {
            doorOpen = true;

            doorUpAnim.SetTrigger("Provided");
            doorAudioSource.volume = volume;
            doorAudioSource.PlayOneShot(doorUpSound);

        }
    }
}

