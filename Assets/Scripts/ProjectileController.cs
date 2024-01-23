using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileController : MonoBehaviour
{

    public float speed = 50;

    // --- Explosion VFX ---
    public GameObject rocketExplosion;

    // --- Projectile Mesh ---
    public MeshRenderer projectileMesh;

    // --- Script Variables ---
    private bool targetHit;

    // --- Audio ---
    public AudioSource inFlightAudioSource;

    public AudioSource explosion;


    private void Update()
    {
        if (targetHit) return;
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {

        Explode();
        projectileMesh.enabled = false;
        targetHit = true;
        inFlightAudioSource.Stop();
        foreach (Collider col in GetComponents<Collider>())
        {
            col.enabled = false;
        }

    }

    private void Explode()
    {

        GameObject newExplosion = Instantiate(rocketExplosion, transform.position, rocketExplosion.transform.rotation, null);

        Destroy(newExplosion, 3f);

        explosion.Play();
    }
}