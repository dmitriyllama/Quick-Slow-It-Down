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
    
    private Tutorial scene;
    private UnityEvent pickupEvent;
    
    private void Start() {
        cam = Camera.main;
        character = GetComponent<CharacterController>();    
        
        scene = GameObject.FindGameObjectWithTag("GameController").GetComponent<Tutorial>();
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
                    if (this.mainHandItem) ThrowMainHand();
                    PickupMainHand(mainHandItem);
                    pickupEvent.Invoke();
                }
                var offHandItem = hit.transform.GetComponent<ItemOffHand>();
                if (offHandItem)
                {
                    PickupOffHand(offHandItem);
                    pickupEvent.Invoke();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (mainHandItem) ThrowMainHand();
            else if (offHandItem) ThrowOffHand();
        }
    }

    private void PickupMainHand(ItemMainHand item) {
        mainHandItem = item;
        item.inHand = true;
        
        item.rb.isKinematic = true;
        
        item.transform.SetParent(mainHandPos);
        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;
    }

    private void ThrowMainHand()
    {
        ItemMainHand item = mainHandItem;
        mainHandItem = null;
        
        item.inHand = false;
        item.transform.SetParent(null);
        item.rb.isKinematic = false;
        item.rb.AddForce(
            (item.transform.forward + transform.up * throwAngleMultiplier) * throwForce + character.velocity,
            ForceMode.VelocityChange);
    }
    
    private void PickupOffHand(ItemOffHand item) {
        offHandItem = item;
        item.inHand = true;

        item.rb.isKinematic = true;

        item.transform.SetParent(offHandPos);
        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;
    }

    private void ThrowOffHand()
    {
        ItemOffHand item = offHandItem;
        offHandItem = null;
        
        item.inHand = false;
        item.transform.SetParent(null);
        item.rb.isKinematic = false;
        item.rb.AddForce(
            (item.transform.forward + transform.up * throwAngleMultiplier) * throwForce + character.velocity,
            ForceMode.VelocityChange);
    }
}