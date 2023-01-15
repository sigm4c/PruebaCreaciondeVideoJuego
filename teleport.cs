using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    new public Collider2D Collider;
    public LayerMask layer;
    public Transform destinationTransform;
    public float delayTime = 0.3f;

    WaitForSeconds delay;

    IEnumerator Activate(GameObject teleportee)
    {
        if (destinationTransform)
        {
            yield return delay;
            teleportee.transform.position = destinationTransform.position;
            teleportee.transform.rotation = destinationTransform.rotation;
        }
    }
    void Start()
    {
      Collider= GetComponent<Collider2D>();
        Collider.isTrigger= true;
    }
    void Awake()
    {
         delay = new WaitForSeconds(delayTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTeleportable(other)) StartCoroutine(Activate(other.gameObject));
        
    }
    bool isTeleportable(Collider2D other) { return 0 != (layer.value & 1 << other.gameObject.layer); }


}
