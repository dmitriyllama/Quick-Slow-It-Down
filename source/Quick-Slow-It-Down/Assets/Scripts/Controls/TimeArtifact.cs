using UnityEngine;

namespace Controls
{
    [RequireComponent(typeof(ItemOffHand))]
    public class TimeArtifact : MonoBehaviour
    {
        [SerializeField] private Color idleColor;
        [SerializeField] private Color activeColor;
        [SerializeField] private float slowMultiplier = 0.1f;

        private ItemOffHand item;
        private new Renderer renderer;
        private Light artifactLight;

        private AudioSource _audioSource;
    
        private bool active;

        private void Start()
        {
            item = GetComponent<ItemOffHand>();
            renderer = GetComponent<Renderer>();
            artifactLight = GetComponentInChildren<Light>();
            _audioSource = GetComponent<AudioSource>();
        
            SetArtifactColor(idleColor);
        }

        private void Update() {
            if (item.inHand)
            {
                if (!Input.GetMouseButtonDown(1)) return;
            
                active = !active;
                if (active)
                {
                    Time.timeScale = slowMultiplier;
                    Time.fixedDeltaTime = 0.02F * Time.timeScale;
                    SetArtifactColor(activeColor);
                    _audioSource.Play();
                }
                else
                {
                    Time.timeScale = 1f;
                    Time.fixedDeltaTime = 0.02F * Time.timeScale;
                    SetArtifactColor(idleColor);
                }
            }
            else
            {
                active = false;
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
                SetArtifactColor(idleColor);
            }
        }

        private void SetArtifactColor(Color color)
        {
            renderer.material.SetColor("_EmissionColor", color);
            artifactLight.color = color;
        }
    }
}