using System;
using System.Collections;
using System.Collections.Generic;
using SceneScripts;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace EnemyAI
{
    public class EnemyAI : MonoBehaviour
    {
        public bool alive { get; private set; }
        
        private Transform visor;
        public PlayerTarget player { get; private set; }
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
        [SerializeField] private float rotationSpeed = 300.0f;
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
        
        public void ForceChangeState(State newState)
        {
            if (!alive) return;
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
            alive = true;
            stateDictionary = new Dictionary<State, BaseState>
            {
                { State.Idle, new IdleState(this) },
                { State.Arming, new ArmingState(this) },
                { State.Shooting, new ShootingState(this) },
                { State.Confused, new ConfusedState(this) }
            };
        }

        void Start()
        {
            visor = transform.GetChild(0);
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTarget>();
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

        public void MoveForward()
        {
            // TODO Pathfinding
            // I assume the enemy will never have to avoid obstacles
            transform.Translate(0, 0, speed * Time.fixedDeltaTime);
        }

        public void SetRotationTarget(Transform targetTransform)
        {
            rotating = true;
            rotationTarget = Quaternion.LookRotation(transform.position - targetTransform.position).eulerAngles.y;
            if (rotationTarget > 180)
            {
                rotationTarget -= 360;
            }
            deltaRotationTarget = Mathf.Sign(rotationTarget) * rotationSpeed;
        }
        
        public void Rotate()
        {
            float savedRotationTarget = rotationTarget;
            rotationTarget -= deltaRotationTarget * Time.fixedDeltaTime;
            if (Math.Abs(Mathf.Sign(savedRotationTarget) + Mathf.Sign(rotationTarget)) < 0.1)
            {
                rotating = false;
                transform.Rotate(0, savedRotationTarget, 0);
            }
            else
            {
                transform.Rotate(0, deltaRotationTarget * Time.fixedDeltaTime, 0);
            }
        }

        public void PickupGun()
        {
            var hand = transform.GetChild(1);
            assignedGun.Pickup(hand);
        }

        public void Aim(Transform targetTransform)
        {
            transform.LookAt(targetTransform);
        }
        
        public void Shoot()
        {
            assignedGun.Shoot(new Ray(visor.position, transform.forward));
        }

        public void LookRandomly()
        {
            rotating = true;
            rotationTarget = Random.Range(-70, 70);
            deltaRotationTarget = Mathf.Sign(rotationTarget) * rotationSpeed;
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
