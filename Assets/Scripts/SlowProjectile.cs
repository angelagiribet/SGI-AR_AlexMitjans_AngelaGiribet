using UnityEngine;

public class SlowProjectile : MonoBehaviour
{
    public Transform target;
    public float speed = 1.2f;
    public float stopDistance = 0.2f;

    public GameObject explosionVFX; 
    public float destroyDelay = 0.1f;

    public int colorIndex ;
    private Renderer rend;

    [Header("Audio")]
    public AudioClip hitSound;
    private AudioSource audioSource;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f;
        audioSource.playOnAwake = false;
    }

    void Start()
    {
        rend.material = new Material(rend.material);
        rend.material.color = Color.red;
    }

    void Update()
    {
        if (target == null) return;

        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) < stopDistance)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        ShieldColor shield = other.GetComponent<ShieldColor>();

        if (shield != null && shield.GetComponent<Renderer>().material.color == Color.red)
        {
            if (hitSound != null && audioSource != null) { 
                audioSource.PlayOneShot(hitSound); 
            }

            if (explosionVFX != null) { 
                Instantiate(explosionVFX, transform.position, Quaternion.identity); 
            }
            Destroy(gameObject, destroyDelay);
        }
    }
}
