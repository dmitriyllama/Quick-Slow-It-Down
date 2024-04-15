using UnityEngine;

public class FanRotation : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speed*Time.deltaTime, 0);
    }
}
