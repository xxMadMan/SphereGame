using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    Transform player;

    bool hasInteracted = false;


    void Start()
    {
        //player = PlayerManager.instance.player.transform;
    }

    public virtual void Interact()
    {
        //this method is meant to be overwritten
        //Debug.Log("Interacting with " + transform.name);
        
    }

    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10) && hit.transform.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                float distance = Vector3.Distance(player.position, gameObject.transform.position);
                if (distance <= radius && !hasInteracted)
                {
                    Interact();

                    hasInteracted = true;
                }
                else if (radius <= distance && hasInteracted)
                {
                    hasInteracted = false;
                }
            }
        }

        if (player == null)
        {
            player = PlayerManager.instance.player.transform;
        }
    }

    public void AttackingReset()
    {
        hasInteracted = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
