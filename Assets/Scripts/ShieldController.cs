using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [Header("Referencies")]
    public Transform rightHandAnchor;
    public GameObject shieldPrefab;

    [Header("Offsets locals")]
    public Vector3 localPositionOffset = new Vector3(0, 0, 0.25f);
    public Vector3 localRotationOffset = Vector3.zero;

    private GameObject shieldInstance;
    private ShieldColor shieldColorComponent;

    void Start()
    {
        if (rightHandAnchor == null || shieldPrefab == null)
        {
            Debug.LogError("ShieldController: falta rightHandAnchor o shieldPrefab");
            return;
        }

        shieldInstance = Instantiate(shieldPrefab, rightHandAnchor);
        shieldInstance.transform.localPosition = localPositionOffset;
        shieldInstance.transform.localRotation = Quaternion.Euler(localRotationOffset);

        shieldColorComponent = shieldInstance.GetComponent<ShieldColor>();

        if (shieldColorComponent == null)
        {
            Debug.LogError("ShieldController: el prefab de l'escut NO te ShieldColor!");
        }
    }
    public void SetShieldColorByIndex(int colorIndex)
    {
        if (shieldColorComponent != null)
        {
            shieldColorComponent.colorIndex = colorIndex;
            shieldColorComponent.ApplyColor();
        }
    }

    public void SetShieldColor(Color color)
    {
        if (shieldColorComponent != null)
        {
            shieldColorComponent.SetDirectColor(color);
        }
    }
}