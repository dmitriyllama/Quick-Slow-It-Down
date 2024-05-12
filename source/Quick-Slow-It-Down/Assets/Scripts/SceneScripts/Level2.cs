using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneScripts
{
    public class Level2 : Level
    {
        [SerializeField] private GUIStyle guiStyle;
        private string label;
        private Camera cam;

        private Coroutine activeCoroutine;
        private int progress = 0;
        private bool playerDead;
        
        [SerializeField] private List<EnemyAI.EnemyAI> enemies;
        
        public override void ReactToPlayerAction()
        {
            if (progress != 0) return;
            progress = 1;
            
            label = "";
        
            StopCoroutine(activeCoroutine);
            activeCoroutine = StartCoroutine(ActivateEnemy());
        }

        public override void ReactToPlayerDeath()
        {
            playerDead = true;
            StopCoroutine(activeCoroutine);
            activeCoroutine = StartCoroutine(ResetLevel());
        }

        public override void ReactToEnemyDeath()
        {
            if (playerDead) return;
            progress++;

            if (progress == 2)
            {
                StopCoroutine(activeCoroutine);
                activeCoroutine = StartCoroutine(EndLevel());
            }
        }
        
        private IEnumerator StartLevel()
        {
            label = "Don't move!";
            yield return new WaitForSeconds(3.2f);
            guiStyle.fontSize = 24;
            label = "Is anything familiar yet?";
            yield return new WaitForSeconds(4.7f);
        }
        
        private IEnumerator ActivateEnemy()
        {
            inAction = true;
            foreach (var enemy in enemies)
            {
                enemy.active = true;
            }
            guiStyle.fontSize = 24;
            yield return new WaitForSeconds(10.0f);
            label = "You can't hide forever!";
            yield return new WaitForSeconds(1.4f);
            label = "There's a mission ahead of you!";
        }
        
        private IEnumerator ResetLevel()
        {
            inAction = false;
            yield return new WaitForSeconds(0.7f);
            guiStyle.fontSize = 24;
            label = "You have to be quicker than that!";
            yield return new WaitForSeconds(1.2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        private IEnumerator EndLevel()
        {
            inAction = false;
            yield return new WaitForSecondsRealtime(1.2f);
            guiStyle.fontSize = 24;
            label = "You got this!";
            yield return new WaitForSeconds(1.2f);
            SceneManager.LoadScene("Level3");
        }
        
        void Start()
        {
            cam = Camera.main;
            activeCoroutine = StartCoroutine(StartLevel());
        }

        private void OnGUI()
        {
            int w = 500;
            int h = 100;
            float posX = (cam.pixelWidth - w) / 2;
            float posY = (cam.pixelHeight) / 4;
            GUI.Label(new Rect(posX, posY, w, h), label, guiStyle);
        }
    }
}