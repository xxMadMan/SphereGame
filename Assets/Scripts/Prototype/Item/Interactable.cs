using UnityEngine;

public class Interactable : MonoBehaviour
{
    
    public float radius = 3f; //distance the player is require to interact with item
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
        //if we press left mouse
        if (Input.GetMouseButton(0))
        {
            // we create a ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if the ray hits
            if (Physics.Raycast(ray, out hit, 10) && hit.transform.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                //here we have a new variable distance set = to
                //player.position between hit.transform.gameObj.etc
                //i think changing the hit.transfom details to something more layer defining for 
                //interactables could solve the issue with multiple items being picked up
                float distance = Vector3.Distance(player.position, hit.transform.gameObject.transform.position);
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
            //Here i had this as a start method however i ran into an issue where it wouldnt apply
            //with the start method, so if Transform player == null then i want to update it to
            //the start methord
            player = PlayerManager.instance.player.transform;
        }
    }

    public void AttackingReset()
    {
        hasInteracted = false;
    }

    //draws the radius you can interact with the item
    void OnDrawGizmosSelected()
    {
        //the set up for the interactive sphere you see in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
