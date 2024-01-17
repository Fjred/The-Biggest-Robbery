using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoors : MonoBehaviour
{
    public Animator doorAnim;
    public AudioSource doorAudioSource;
    public AudioClip openSound;
    public AudioClip closeSound;

    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnim.ResetTrigger("Close");
            doorAnim.SetTrigger("Open");

            doorAudioSource.volume = volume;
            doorAudioSource.PlayOneShot(openSound);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnim.ResetTrigger("Open");
            doorAnim.SetTrigger("Close");

            doorAudioSource.volume = volume;
            doorAudioSource.PlayOneShot(closeSound);
        }
    }
}

