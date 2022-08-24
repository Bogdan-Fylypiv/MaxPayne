using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform lookAt;
    [SerializeField]
    float damping;
    [SerializeField]
    Vector3 offset;
    public Player player;
    public Animator animator;

    // Start is called before the first frame update 
    void Awake()
    {
        Manager.ManagerInstance.playerJoinedEvent += playerJoinedHandler;
    }

    private void Start()
    {
        animator.SetTrigger("Fade");
    }

    private void playerJoinedHandler(Player player)
    {
        this.player = player;
        lookAt = player.transform.Find("Aiming Pivot");

        if (lookAt == null)
            lookAt = player.transform;
    }

    // Update is called once per frame
    void LateUpdate ()
    {
        if (player == null)
            return;


        Vector3 position = lookAt.position + player.transform.forward * offset.z + player.transform.up * offset.y + player.transform.right * offset.x;

        //Checking if there is anything between the player and the camera
        Vector3 collisionCheckEnd = player.transform.position + player.transform.up * 2;
        Debug.DrawLine(position, collisionCheckEnd, Color.blue) ;
        if(Physics.Linecast(collisionCheckEnd, position, out var hit))
        {
            if (hit.transform.tag.Equals("Scanner"))
                goto Skip;
            Vector3 hitPoint = new Vector3(hit.point.x + hit.normal.x * 0.3f, hit.point.y, hit.point.z + hit.normal.z * 0.3f);
            position = new Vector3(hitPoint.x, position.y, hitPoint.z);
        }

        Skip:
        transform.position = Vector3.Lerp(transform.position, position, damping * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAt.rotation, damping * Time.deltaTime);
    }
}
