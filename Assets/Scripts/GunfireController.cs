using UnityEngine;

public class GunfireController : MonoBehaviour
{

    public AudioClip GunShotClip;
    public AudioClip ReloadClip;
    public AudioSource source;
    public AudioSource reloadSource;
    public Vector2 audioPitch = new Vector2(.9f, 1.1f);

    // --- Muzzle ---
    public GameObject muzzlePrefab;
    public GameObject muzzlePosition;

    public GameObject projectilePrefab;

    public GameObject projectilePlace;
    
    public GameObject vaultDoor;

    public bool rocketActivated = false;

    private void Start()
    {
        if (source != null) source.clip = GunShotClip;
    }

    public void FireWeapon()
    {
        rocketActivated = true;

        Invoke("DestroyDoor", 2);

        var flash = Instantiate(muzzlePrefab, muzzlePosition.transform);

        if (projectilePrefab != null)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, muzzlePosition.transform.position, muzzlePosition.transform.rotation, transform);
        }

        if (source != null)
        {
            if (source.transform.IsChildOf(transform))
            {
                source.Play();
            }
            else
            {

                AudioSource newAS = Instantiate(source);
                if ((newAS = Instantiate(source)) != null && newAS.outputAudioMixerGroup != null && newAS.outputAudioMixerGroup.audioMixer != null)
                {
                    newAS.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", Random.Range(audioPitch.x, audioPitch.y));
                    newAS.pitch = Random.Range(audioPitch.x, audioPitch.y);


                    newAS.PlayOneShot(GunShotClip);

                    Destroy(newAS.gameObject, 4);
                }
            }
        }

        projectilePlace.SetActive(false);

    }

    public void DestroyDoor()
    {
        Destroy(vaultDoor);
    }
}