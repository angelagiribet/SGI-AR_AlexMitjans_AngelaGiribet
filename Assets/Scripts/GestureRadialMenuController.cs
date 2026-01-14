using UnityEngine;

public class GestureRadialMenuController : MonoBehaviour
{
    public OVRHand leftHand;
    public RadialSelection radialMenu;

    public float menuDistanceFromPalm = 0.08f;
    public float menuScale = 0.002f;

    private bool wasPinching = false;

    void Start()
    {
        radialMenu.radialPartCanvas.gameObject.SetActive(true);
        radialMenu.radialPartCanvas.localScale = Vector3.one * menuScale;
    }

    void Update()
    {
        if (leftHand == null || radialMenu == null)
            return;

        UpdateMenuPosition();

        bool pinch = leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool pinchStarted = pinch && !wasPinching;

        if (pinchStarted)
            radialMenu.TriggerSelected();

        wasPinching = pinch;
    }

    void UpdateMenuPosition()
    {
        Vector3 palmPos = leftHand.transform.position;

        Vector3 outwardOffset = -leftHand.transform.up * menuDistanceFromPalm;

        Vector3 fingerOffset = leftHand.transform.forward * 0.010f;

        Vector3 lateralOffset = leftHand.transform.right * 0.01f;

        radialMenu.radialPartCanvas.position =
            palmPos + outwardOffset + fingerOffset + lateralOffset;

        Vector3 lookDir = Camera.main.transform.position - radialMenu.radialPartCanvas.position;
        radialMenu.radialPartCanvas.rotation = Quaternion.LookRotation(lookDir);
    }

}
