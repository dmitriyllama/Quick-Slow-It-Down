using SceneScripts;
using UnityEngine;

namespace Controls
{
    public class UIController : MonoBehaviour {
    
        [SerializeField] PauseScreen pauseScreen;
        private Level level;

        private bool paused;
    
        void Start()
        {
            pauseScreen.gameObject.SetActive(false);
            pauseScreen.Close();
            level = GameObject.FindGameObjectWithTag("GameController").GetComponent<Level>();
            DontDestroyOnLoad(this);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (level.inAction)
                {
                    if (paused)
                    {
                        pauseScreen.gameObject.SetActive(true);
                        pauseScreen.Open();
                    }
                    else
                    {
                        pauseScreen.gameObject.SetActive(true);
                        pauseScreen.Close();
                    }
                }
            }
        }
    }
}