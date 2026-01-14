using UnityEngine;

public class ShieldColor : MonoBehaviour
{
    public int colorIndex = 0;
    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    void Start()
    {
        ApplyColor();
    }

    public void ApplyColor()
    {
        if (ColorManager.Instance != null)
        {
            rend.material.color = ColorManager.Instance.GetColor(colorIndex);
        }
        else
        {
            Debug.LogError("ShieldColor: No hi ha ColorManager a l'escena!");
        }
    }

    public void SetDirectColor(Color c)
    {
        rend.material.color = c;
    }
}