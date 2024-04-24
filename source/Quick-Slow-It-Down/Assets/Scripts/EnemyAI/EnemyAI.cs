using System;
using System.Collections;
using System.Collections.Generic;
using SceneScripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace EnemyAI
{
    public class EnemyAI : MonoBehaviour
    {
        public bool alive { get; private set; }
        
        private Transform visor;
        public Transform playerTransform { get; private set; }
        [SerializeField] public Gun assignedGun;

        public enum State
        {
            Idle,
            Arming,
            Shooting,
            Searching,
            Confused
        }
        
        private State state;
        private Dictionary<State, BaseState> stateDictionary;
        [SerializeField] public bool active;
        
        [SerializeField] private float speed = 4.0f;
        [SerializeField] private float rotationSpeed = 200.0f;
        public bool rotating;
        private float rotationTarget;
        private float deltaRotationTarget;

        private SceneScripts.Level levelController;
        private UnityEvent deathEvent;

        public void ChangeState(State newState)
        {
            if (!alive || !active) return;
            if (stateDictionary.ContainsKey(state))
            {
                stateDictionary[state].Exit();
            }
            
            state = newState;
            if (stateDictionary.ContainsKey(newState))
            {
                stateDictionary[newState].Enter();
            }
        }
        

        void Awake()
        {
            stateDictionary = new Dictionary<State, BaseState>
            {
                { State.Idle, new IdleState(this) },
                { State.Arming, new ArmingState(this) },
                { State.Shooting, new ShootingState(this) }
                // , { State.Searching, new SearchingState(this) },
                // { State.Confused, new ConfusedState(this) }
            };
        }

        void Start()
        {
            alive = true;
            visor = transform.GetChild(0);
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            levelController = GameObject.FindGameObjectWithTag("GameController").GetComponent<Level>();
            
            deathEvent = new UnityEvent();
            deathEvent.AddListener(levelController.ReactToEnemyDeath);
        }

        void FixedUpdate()
        {
            if (stateDictionary.ContainsKey(state))
            {
                stateDictionary[state].FixedUpdate();
            }
        }

        public void SetRotationTarget(Transform targetTransform)
        {
            rotating = true;
            rotationTarget = Quaternion.LookRotation(targetTransform.position - transform.position).eulerAngles.z;
            deltaRotationTarget = Mathf.Sign(rotationTarget) * rotationSpeed;
        }
        
        public void Rotate()
        {
            transform.Rotate(0, deltaRotationTarget * Time.fixedDeltaTime, 0);
        
            float savedRotationTarget = rotationTarget;
            rotationTarget -= deltaRotationTarget * Time.fixedDeltaTime;
            if (Math.Abs(Mathf.Sign(savedRotationTarget) - Mathf.Sign(rotationTarget)) > 0.2)
            {
                rotating = false;
            }
        }

        public void PickupGun()
        {
            var hand = transform.GetChild(1);
            assignedGun.Pickup(hand);
        }

        public void Shoot()
        {
            assignedGun.Shoot(new Ray(visor.position, transform.forward));
        }

        public void ReactToHit()
        {
            deathEvent.Invoke();
            StartCoroutine(Die());
        }

        private IEnumerator Die()
        {
            alive = false;
            transform.Rotate(-75, 0, 0);
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }
    }
}
