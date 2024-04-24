using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ItemOffHand : MonoBehaviour
{
    private Rigidbody rb;
    public bool inHand { get; private set; }

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    public void Pickup(Transform hand)
    {
        inHand = true;
        transform.SetParent(hand);
        rb.isKinematic = true;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

    public void Throw(Vector3 impulse, float throwForce, float throwAngleMultiplier)
    {
        inHand = false;
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.AddForce(
            (transform.forward + transform.up * throwAngleMultiplier) * throwForce + impulse,
            ForceMode.VelocityChange);
    }
}