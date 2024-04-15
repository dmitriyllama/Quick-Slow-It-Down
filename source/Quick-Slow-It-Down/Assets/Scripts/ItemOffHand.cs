using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ItemOffHand : MonoBehaviour
{
    public Rigidbody rb;
    public bool inHand;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
}