using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneScripts
{
    public class Tutorial : Level
    {
        [SerializeField] private GUIStyle guiStyle;
        private string label;
        private Camera cam;

        private Coroutine activeCoroutine;
        private int tutorialProgress;
        private bool readThroughEnoughTutorial;

        [SerializeField] private EnemyAI.EnemyAI enemy;
        public override void ReactToPlayerAction()
        {
            if (tutorialProgress != 0) return;
            tutorialProgress++;
        
            label = "";
        
            StopCoroutine(activeCoroutine);
            activeCoroutine = StartCoroutine(ActivateEnemy());
        }

        public override void ReactToPlayerDeath()
        {
            StopCoroutine(activeCoroutine);
            activeCoroutine = StartCoroutine(ResetTutorial());
        }
    
        public override void ReactToEnemyDeath()
        {
            if (tutorialProgress != 1) return;
            tutorialProgress++;
        
            StopCoroutine(activeCoroutine);
            activeCoroutine = StartCoroutine(EndTutorial());
        }
    
        private IEnumerator StartTutorial()
        {
            label = "Don't move!";
            yield return new WaitForSeconds(4.2f);
            guiStyle.fontSize = 24;
            label = "Don't do anything, or you get shot.";
            yield return new WaitForSeconds(3.7f);
            label = "And don't panic, this is a test.";
            yield return new WaitForSeconds(3.7f);
            label = "Now listen. You have to follow the one rule of this bizarre game.";
            yield return new WaitForSeconds(4.2f);
            label = "In front of you is a ball of shining rock, which we'll have to call the \"time orb\".";
            yield return new WaitForSeconds(4f);
            label = "A time orb will slow down everything around you if you push on it.";
            yield return new WaitForSeconds(6.2f);
            label = "Also, there's a gun. It should un-alive the other guy if you use it wisely.";
            yield return new WaitForSeconds(4.2f);
            label = "The rule here is simple. You're doomed if you try to reach the gun first.";
            yield return new WaitForSeconds(4.7f);
            label = "You might have to use the time orb instead.";
            yield return new WaitForSeconds(4.7f);
            label = "Focus now. You have to be quick.";
            readThroughEnoughTutorial = true;
            yield return new WaitForSeconds(3.2f);
            label = "Grab an item [E]\nSlow it all down [Right Click]\nShoot the guy! [Left Click]";
            yield return new WaitForSeconds(10f);
        }

        private IEnumerator ActivateEnemy()
        {
            inAction = true;
            yield return new WaitForSeconds(0.2f);
        
            enemy.PickupGun();
            
            yield return new WaitForSeconds(0.3f);

            enemy.ForceChangeState(EnemyAI.EnemyAI.State.Shooting);
        }

        private IEnumerator ResetTutorial()
        {
            inAction = false;
            yield return new WaitForSeconds(1.7f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private IEnumerator EndTutorial()
        {
            yield return new WaitForSeconds(3.2f);
            if (readThroughEnoughTutorial)
            {
                label = "There you go!";
                yield return new WaitForSeconds(3.2f);
            }
            else
            {
                guiStyle.fontSize = 24;
                label = "Woah";
                yield return new WaitForSeconds(1.6f);
                label = "Quick enough!";
                yield return new WaitForSeconds(1.9f);
            }
            label = "";
            SceneManager.LoadScene("Level2");
        }
    
        void Start()
        {
            cam = Camera.main;
            activeCoroutine = StartCoroutine(StartTutorial());
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
