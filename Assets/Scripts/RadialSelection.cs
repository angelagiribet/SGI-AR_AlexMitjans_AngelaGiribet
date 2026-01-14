using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

public class RadialSelection : MonoBehaviour
{
    [Range(2, 10)]
    public int numberOfRadialParts;
    public GameObject radialPartPrefab;
    public Transform radialPartCanvas;
    public float angleBetweenParts = 10;
    public Transform handTransform;

    public UnityEvent<int> OnPartSelected;

    public ShieldController shieldController;
    public Color[] shieldColors;

    private List<GameObject> spawnedParts = new List<GameObject>();
    private int currentSelectedRadialPart = -1;

    void Start()
    {
        if (shieldColors == null || shieldColors.Length == 0)
        {
            shieldColors = new Color[] { Color.red, Color.blue, Color.green };
        }

        numberOfRadialParts = shieldColors.Length;

        SpawnRadialPart();
    }

    void Update()
    {
        GetSelectedRadialPart();
    }

    public void GetSelectedRadialPart()
    {
        Vector3 worldDir = handTransform.position - radialPartCanvas.position;

        Vector3 localDir = radialPartCanvas.InverseTransformDirection(worldDir);
        localDir.z = 0;

        Vector3 dir = localDir.normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;

        currentSelectedRadialPart = Mathf.FloorToInt(angle / (360f / numberOfRadialParts));

        UpdateVisualSelection();
    }

    void UpdateVisualSelection()
    {
        for (int i = 0; i < spawnedParts.Count; i++)
        {
            Image img = spawnedParts[i].GetComponent<Image>();

            if (i == currentSelectedRadialPart)
            {
                Color c = shieldColors[i];
                c.a = 1f;
                img.color = c;

                spawnedParts[i].transform.localScale = 1.1f * Vector3.one;
            }
            else
            {
                Color c = shieldColors[i];
                c.a = 0.4f;
                img.color = c;

                spawnedParts[i].transform.localScale = Vector3.one;
            }
        }
    }

    public void SpawnRadialPart()
    {
        foreach (var item in spawnedParts)
            Destroy(item);

        spawnedParts.Clear();

        for (int i = 0; i < numberOfRadialParts; i++)
        {
            float angle = i * 360f / numberOfRadialParts - angleBetweenParts / 2f;
            Vector3 radialPartEulerAngle = new Vector3(0, 0, angle);

            GameObject spawnedRadialPart = Instantiate(radialPartPrefab, radialPartCanvas);
            spawnedRadialPart.transform.position = radialPartCanvas.position;
            spawnedRadialPart.transform.localEulerAngles = radialPartEulerAngle;

            Image img = spawnedRadialPart.GetComponent<Image>();

            Color c = shieldColors[i];
            c.a = 0.4f;
            img.color = c;

            img.fillAmount = 1f / numberOfRadialParts - (angleBetweenParts / 360f);

            spawnedParts.Add(spawnedRadialPart);
        }
    }
    public void TriggerSelected()
    {
        OnPartSelected.Invoke(currentSelectedRadialPart);

        if (shieldController != null &&
            currentSelectedRadialPart >= 0 &&
            currentSelectedRadialPart < shieldColors.Length)
        {
            shieldController.SetShieldColor(shieldColors[currentSelectedRadialPart]);
        }
    }

    public void HideAndTriggerSelected()
    {
        OnPartSelected.Invoke(currentSelectedRadialPart);

        if (shieldController != null &&
            currentSelectedRadialPart >= 0 &&
            currentSelectedRadialPart < shieldColors.Length)
        {
            shieldController.SetShieldColor(shieldColors[currentSelectedRadialPart]);
        }

        radialPartCanvas.gameObject.SetActive(false);
    }
}