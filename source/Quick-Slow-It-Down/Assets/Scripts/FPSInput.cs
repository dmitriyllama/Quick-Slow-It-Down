using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    [SerializeField] float speed = 6.0f;
    [SerializeField] float gravity = -9.8f;
    private CharacterController charController;
    
    private Tutorial scene;
    private UnityEvent moveEvent;

    void Start()
    {
        charController = GetComponent<CharacterController>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        scene = GameObject.FindGameObjectWithTag("GameController").GetComponent<Tutorial>();
        moveEvent = new UnityEvent();
        moveEvent.AddListener(scene.ReactToPlayerAction);
    }

    // Update is called once per frame
    void Update()
    {
        
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        
        if (!movement.Equals(Vector3.zero))
        {
            moveEvent.Invoke();
        }
        
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        charController.Move(movement);
    }
}