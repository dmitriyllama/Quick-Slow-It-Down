using SceneScripts;
using UnityEngine;
using UnityEngine.Events;

namespace Controls
{
    [RequireComponent(typeof(CharacterController))]
    [AddComponentMenu("Control Script/FPS Input")]
    public class FPSInput : MonoBehaviour
    {
        [SerializeField] float speed = 6.0f;
        [SerializeField] float gravity = -9.8f;
        private CharacterController charController;

        [SerializeField] private AudioClip walkSound;
        private AudioSource _audioSource;
        
        private Level level;
        private UnityEvent moveEvent;

        void Start()
        {
            charController = GetComponent<CharacterController>();

            level = GameObject.FindGameObjectWithTag("GameController").GetComponent<Level>();
            moveEvent = new UnityEvent();
            moveEvent.AddListener(level.ReactToPlayerAction);

            _audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            float deltaX = Input.GetAxis("Horizontal") * speed;
            float deltaZ = Input.GetAxis("Vertical") * speed;
            Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        
            if (!movement.Equals(Vector3.zero))
            {
                moveEvent.Invoke();
                if (!_audioSource.isPlaying) _audioSource.PlayOneShot(walkSound);
            }
        
            movement = Vector3.ClampMagnitude(movement, speed);
            movement.y = gravity;
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            charController.Move(movement);
        }
    }
}
