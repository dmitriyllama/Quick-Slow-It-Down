using UnityEngine;

public class MouseLookX : MonoBehaviour
{
    [SerializeField] private float mouseSensitivityH = 3.0f;

    // Update is called once per frame
    void Update()
    {
        float horizontalRot = Input.GetAxis("Mouse X") * mouseSensitivityH;
        transform.Rotate(0, horizontalRot, 0);
    }
}
