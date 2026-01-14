using UnityEngine;

public class ShieldSpawner : MonoBehaviour
{
    public static ShieldController currentShield; 

    [Header("Referències")]
    public GameObject shieldPrefab;
    public Transform userHead;
    public LayerMask realWorldLayer;

    [Header("Paràmetres de spawn")]
    public float forwardOffset = 0.5f;
    public float upOffset = 0.3f;
    public float maxDownDistance = 3f;
    public float surfaceOffset = 0.02f;

    [Header("Filtres d’alçada")]
    public float minSurfaceHeight = 0.4f;

    private bool spawned = false;

    void Update()
    {
        if (spawned) return;
        if (shieldPrefab == null || userHead == null) return;

        Vector3 origin = userHead.position
                       + userHead.forward * forwardOffset
                       + Vector3.up * upOffset;

        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, maxDownDistance, realWorldLayer))
        {
            if (hit.point.y < minSurfaceHeight)
                return;

            Vector3 spawnPos = hit.point + hit.normal * surfaceOffset;

            Vector3 projectedForward = Vector3.ProjectOnPlane(userHead.forward, hit.normal);
            if (projectedForward.sqrMagnitude < 0.001f)
                projectedForward = Vector3.forward;

            Quaternion spawnRot = Quaternion.LookRotation(projectedForward, hit.normal);

            GameObject shield = Instantiate(shieldPrefab, spawnPos, spawnRot);

            currentShield = shield.GetComponent<ShieldController>();

            if (currentShield != null)
                currentShield.SetShieldColorByIndex(0);

            spawned = true;
        }
    }
}
