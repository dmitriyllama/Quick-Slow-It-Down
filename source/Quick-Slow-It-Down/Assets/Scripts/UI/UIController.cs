using Controls;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour {
        [SerializeField] PauseScreen pauseScreen;
    
        void Start() {
            pauseScreen.Close();
            DontDestroyOnLoad(this);
        }
    
        public void OnGamePause()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            var artifact = GameObject.FindGameObjectWithTag("Artifact");
        
            player.GetComponent<MouseLookX>().enabled = false;
            player.GetComponentInChildren<MouseLookY>().enabled = false;
            player.GetComponent<FPSInput>().enabled = false;
            artifact.GetComponent<TimeArtifact>().enabled = false;
        
            Time.timeScale = 0;
        
            pauseScreen.Open();
        }
    }
}