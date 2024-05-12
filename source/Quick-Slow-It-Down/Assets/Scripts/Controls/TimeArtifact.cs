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
        private TrailRenderer artifactTrail;

        private AudioSource _audioSource;
    
        private bool active;

        private void Start()
        {
            item = GetComponent<ItemOffHand>();
            renderer = GetComponent<Renderer>();
            artifactLight = GetComponentInChildren<Light>();
            artifactTrail = GetComponent<TrailRenderer>();
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
                    artifactTrail.emitting = true;
                    _audioSource.Play();
                }
                else
                {
                    Time.timeScale = 1f;
                    Time.fixedDeltaTime = 0.02F * Time.timeScale;
                    SetArtifactColor(idleColor);
                    artifactTrail.emitting = false;
                }
            }
            else
            {
                active = false;
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
                SetArtifactColor(idleColor);
                artifactTrail.emitting = false;
            }
        }

        private void SetArtifactColor(Color color)
        {
            renderer.material.SetColor("_EmissionColor", color);
            artifactLight.color = color;
        }
    }
}