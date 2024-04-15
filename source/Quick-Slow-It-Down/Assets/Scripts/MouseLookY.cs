using UnityEngine;

public class MouseLookY : MonoBehaviour
{
    [SerializeField] private float mouseSensitivityV = 3.0f;
    [SerializeField] private float minimumVerticalRot = -70.0f;
    [SerializeField] private float maximumVerticalRot = 70.0f;
    private float verticalRot = 0;

    // Update is called once per frame
    void Update()
    {
        verticalRot -= Input.GetAxis("Mouse Y") * mouseSensitivityV;
        verticalRot = Mathf.Clamp(verticalRot, minimumVerticalRot, maximumVerticalRot);
        float horizontalRot = transform.localEulerAngles.y;
        transform.localEulerAngles = new Vector3(verticalRot, 0, 0);
    }
}
