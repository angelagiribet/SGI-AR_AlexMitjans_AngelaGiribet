using Meta.XR.MRUtilityKit;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject ghostPrefab;
    private GameObject spawnedGhost;

    public GameObject projectilePrefab;
    public Transform playerHead;

    public float spawnTimer = 3f;
    public float shootDelay = 1f;
    public float heightMin = 1.0f;
    public float heightMax = 1.8f;

    private float timer = 0f;
    private bool hasTeleported = false;
    private bool hasShot = false;

    [Header("Audio")]
    public AudioClip spawnSound;
    private AudioSource audioSource;

    void Start()
    {
        if (playerHead == null && Camera.main != null)
            playerHead = Camera.main.transform;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f;   
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (MRUK.Instance == null || !MRUK.Instance.IsInitialized)
            return;

        if (spawnedGhost == null && ghostPrefab != null)
        {
            spawnedGhost = Instantiate(ghostPrefab, transform.position, Quaternion.identity);
        }

        timer += Time.deltaTime;

        if (!hasTeleported && timer >= 0f)
        {
            TeleportEnemy();
            hasTeleported = true;
        }

        if (!hasShot && timer >= shootDelay)
        {
            Shoot();
            hasShot = true;
        }

        if (timer >= spawnTimer)
        {
            timer = 0f;
            hasTeleported = false;
            hasShot = false;
        }
    }

    void TeleportEnemy()
    {
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();
        if (room == null) return;

        Vector3? pos = room.GenerateRandomPositionInRoom(0.3f, false);

        if (pos.HasValue)
        {
            Vector3 finalPos = pos.Value;
            finalPos.y = Random.Range(heightMin, heightMax);

            transform.position = finalPos;

            if (spawnedGhost != null)
            {
                spawnedGhost.transform.position = finalPos;

                Vector3 lookTarget = Camera.main.transform.position;
                lookTarget.y = spawnedGhost.transform.position.y;

                spawnedGhost.transform.LookAt(lookTarget);

                Vector3 e = spawnedGhost.transform.eulerAngles;
                e.x = 0f;
                e.z = 0f;
                spawnedGhost.transform.eulerAngles = e;
            }

            if (spawnSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(spawnSound);
            }
        }
    }

    void Shoot()
    {
        if (projectilePrefab == null) return;

        GameObject p = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        SlowProjectile sp = p.GetComponent<SlowProjectile>();
        if (sp != null)
            sp.target = playerHead;
    }
}
