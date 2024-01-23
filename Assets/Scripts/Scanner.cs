using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public Animator vaultDoorAnim;
    public AudioSource doorAudioSource;
    public AudioClip openVaultSound;
    public AudioClip closeVaultSound;

    public StealItem player;

    public GunfireController RPG;

    public GameObject blockade;

    public bool vaultOpen = false;

    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;

    public bool doorShut = false;



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
        if (player.StealedItem == true && doorShut == false && RPG.rocketActivated == false)
        {
            doorShut = true;

            vaultDoorAnim.SetBool("ItemStolen", true);

            blockade.SetActive(false);

            doorAudioSource.volume = volume;
            doorAudioSource.PlayOneShot(closeVaultSound);
        }
    }
}

