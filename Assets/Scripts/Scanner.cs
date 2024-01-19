using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public Animator vaultDoorAnim;
    public AudioSource doorAudioSource;
    public AudioClip openVaultSound;

    public StealItem player;

    public GameObject blockade;

    public bool vaultOpen = false;

    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && player.hasCard2 == true && Input.GetKey(KeyCode.E) && vaultOpen == false)
        {
            vaultOpen = true;

            blockade.SetActive(true);

            vaultDoorAnim.SetTrigger("Open");
            doorAudioSource.volume = volume;
            doorAudioSource.PlayOneShot(openVaultSound);

        }
    }
}

