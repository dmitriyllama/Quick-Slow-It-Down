using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneScripts
{
    public class Level3 : Level
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

            if (progress == 4)
            {
                StopCoroutine(activeCoroutine);
                activeCoroutine = StartCoroutine(EndLevel());
            }
        }
        
        private IEnumerator StartLevel()
        {
            yield return new WaitForSeconds(4.2f);
            guiStyle.fontSize = 24;
            label = "You need to be quick";
        }
        
        private IEnumerator ActivateEnemy()
        {
            inAction = true;
            foreach (var enemy in enemies)
            {
                enemy.active = true;
            }
            guiStyle.fontSize = 24;
            yield return new WaitForSeconds(1.6f);
            label = "The guards don't hesitate to keep this place secure";
            yield return new WaitForSeconds(3.2f);
            label = "Why don't they see the artifact in your hand?";
            yield return new WaitForSeconds(3.4f);
            label = "";
        }
        
        private IEnumerator ResetLevel()
        {
            inAction = false;
            yield return new WaitForSeconds(0.7f);
            guiStyle.fontSize = 24;
            label = "You have to be quicker!";
            yield return new WaitForSeconds(1.2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        private IEnumerator EndLevel()
        {
            inAction = false;
            yield return new WaitForSecondsRealtime(1.2f);
            guiStyle.fontSize = 24;
            label = "We must move!";
            yield return new WaitForSeconds(1.8f);
            // FIXME Next transition
            label = "";
            Debug.Log("Demo done!");
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