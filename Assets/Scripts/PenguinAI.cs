using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PenguinAI : MonoBehaviour
{
    public float wanderRadius = 15f;
    public float chaseRadius = 15f;
    public float movementSpeed = 3f;
    public float chaseSpeedMultiplier = 1.5f;
    public float cameraRotationSpeed = 1f;
    public Camera cameraToRotate;

    public Animator animator;
    //public Gun gun;

    private Transform player;
    private Vector3 wanderPoint;
    private bool isChasing = false;
    private NavMeshAgent navMeshAgent;
    private float originalSpeed;
    private bool isCameraRotating = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        originalSpeed = navMeshAgent.speed;
        SetNewWanderPoint();
    }

    private void Update()
    {
        if (!isCameraRotating)
        {
            if (!isChasing)
            {
                // Check if the player is within the chase radius
                if (Vector3.Distance(transform.position, player.position) <= chaseRadius)
                {
                    isChasing = true;
                    navMeshAgent.speed = movementSpeed * chaseSpeedMultiplier;
                    navMeshAgent.SetDestination(player.position);
                    animator.SetBool("Running", true);
                }
                else if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
                {
                    // Wander if not chasing and reached the wander point
                    SetNewWanderPoint();
                    navMeshAgent.speed = movementSpeed;
                    navMeshAgent.SetDestination(wanderPoint);
                    animator.SetBool("Running", false);
                }
            }
            else if (Vector3.Distance(transform.position, player.position) > chaseRadius)
            {
                // Stop chasing if the player is outside the chase radius
                isChasing = false;
                navMeshAgent.ResetPath();
                navMeshAgent.speed = movementSpeed;
                animator.SetBool("Running", false);
            }
            else
            {
                // Move towards the player's current position
                navMeshAgent.SetDestination(player.position);
            }
        }
    }

    private void SetNewWanderPoint()
    {
        // Generate a new random point within the wander radius
        Vector3 randomPoint = Random.insideUnitSphere * wanderRadius;
        randomPoint.y = transform.position.y;

        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(transform.position + randomPoint, out navMeshHit, wanderRadius, NavMesh.AllAreas);
        wanderPoint = navMeshHit.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCameraRotating)
        {
            // gun.ammo = 0;

            isCameraRotating = true;
            StartCoroutine(RotateCameraTowardsEnemy());
        }
    }

    private IEnumerator RotateCameraTowardsEnemy()
    {

        Quaternion startCameraRotation = cameraToRotate.transform.rotation;
        Quaternion startEnemyRotation = transform.rotation;

        Quaternion targetCameraRotation = Quaternion.LookRotation(transform.position - player.position, Vector3.up);
        Quaternion targetEnemyRotation = Quaternion.LookRotation(player.position - transform.position, Vector3.up);

        float elapsedTime = 0f;
        while (elapsedTime < cameraRotationSpeed)
        {
            elapsedTime += Time.deltaTime;
            cameraToRotate.transform.rotation = Quaternion.Slerp(startCameraRotation, targetCameraRotation, elapsedTime / cameraRotationSpeed);
            transform.rotation = Quaternion.Slerp(startEnemyRotation, targetEnemyRotation, elapsedTime / cameraRotationSpeed);
            yield return null;
        }


        SceneManager.LoadScene(0);
    }
}
