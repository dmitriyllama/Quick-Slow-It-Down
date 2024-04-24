using UnityEngine;

namespace SceneScripts
{
    public abstract class Level : MonoBehaviour
    {
        public bool inAction = false;
        
        public abstract void ReactToPlayerAction();
        public abstract void ReactToPlayerDeath();
        public abstract void ReactToEnemyDeath();
    }
}