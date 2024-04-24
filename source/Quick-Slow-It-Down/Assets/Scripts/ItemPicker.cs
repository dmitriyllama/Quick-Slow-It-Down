using SceneScripts;
using UnityEngine;
using UnityEngine.Events;

public class ItemPicker : MonoBehaviour {
    private Camera cam;
    private CharacterController character;

    [SerializeField] private float throwForce;
    [SerializeField] private float throwAngleMultiplier;
    
    [SerializeField] private Transform mainHandPos;
    [SerializeField] private Transform offHandPos;
    private ItemMainHand mainHandItem;
    private ItemOffHand offHandItem;
    
    private Level scene;
    private UnityEvent pickupEvent;
    
    private void Start() {
        cam = Camera.main;
        character = GetComponent<CharacterController>();    
        
        scene = GameObject.FindGameObjectWithTag("GameController").GetComponent<Level>();
        pickupEvent = new UnityEvent();
        pickupEvent.AddListener(scene.ReactToPlayerAction);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            var ray = cam.ViewportPointToRay(Vector3.one * 0.5f);
            if (Physics.Raycast(ray, out var hit, 5f)) {
                var mainHandItem = hit.transform.GetComponent<ItemMainHand>();
                if (mainHandItem)
                {
                    if (!mainHandItem.inHand)
                    {
                        if (this.mainHandItem) ThrowMainHand();
                        this.mainHandItem = mainHandItem;
                        mainHandItem.Pickup(mainHandPos);
                        pickupEvent.Invoke();
                    }
                }
                var offHandItem = hit.transform.GetComponent<ItemOffHand>();
                if (offHandItem)
                {
                    // Doesn't require item.inHand check: only player can hold in off hand
                    if (this.offHandItem) ThrowOffHand();
                    this.offHandItem = offHandItem;
                    offHandItem.Pickup(offHandPos);
                    pickupEvent.Invoke();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (mainHandItem) ThrowMainHand();
            else if (offHandItem) ThrowOffHand();
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (!mainHandItem) return;
            if (mainHandItem is not Gun gun) return;
            var ray = cam.ViewportPointToRay(Vector3.one * 0.5f);
            gun.Shoot(ray);
        }
    }

    private void ThrowMainHand()
    {
        ItemMainHand item = mainHandItem;
        mainHandItem = null;
        
        item.Throw(character.velocity, throwForce, throwAngleMultiplier);
    }

    private void ThrowOffHand()
    {
        ItemOffHand item = offHandItem;
        offHandItem = null;
        
        item.Throw(character.velocity, throwForce, throwAngleMultiplier);
    }
}