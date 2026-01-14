using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance;

    public Color[] availableColors = {
        Color.red,
        Color.blue,
        Color.green
    };

    void Awake()
    {
        Instance = this;
    }

    public Color GetColor(int index)
    {
        return availableColors[index];
    }
}
