using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ItemMainHand : MonoBehaviour
{
    public Rigidbody rb;
    public bool inHand;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
}