using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    [System.Serializable]
    public class MouseInput
    {
        public Vector2 damping;
        public Vector2 sensitivity;
        public bool LockMouse;
    }

    [SerializeField] float runSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] AudioController runFootsteps;

    [SerializeField] MouseInput mouseControl;
    CharacterController moveController;
    InputController playerInput;
    Vector2 mouseInput;
    Crosshair crosshair;
    [HideInInspector] public PlayerShoot playerShoot;
    public PlayerAim playerAim;
    PlayerHealth playerHealth;

    public List<GameObject> enemies;
    float distanceToPlayerThreshold;
    float distanceToPlayer;
    public bool enemyActive;

    Vector3 lastPosition;
    public float minDist;


    public CharacterController MoveController
    {
        get
        {
            if (moveController == null)
                moveController = GetComponent<CharacterController>();
            return moveController;
        }
    }

    Crosshair CrossHair
    {
        get
        {
            if (crosshair == null)
                crosshair = GetComponentInChildren<Crosshair>();
            return crosshair;
        }
    }

    public PlayerHealth PlayerHealth
    {
        get
        {
            if (playerHealth == null)
                playerHealth = GetComponentInChildren<PlayerHealth>();
            return playerHealth;
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        playerShoot = GetComponent<PlayerShoot>();
        playerInput = Manager.ManagerInstance.Input;
        Manager.ManagerInstance.Player = this;
        distanceToPlayerThreshold = 50;

        if (mouseControl.LockMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!playerHealth.IsAlive)
            return;

        transform.position = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);

        Move();
        LookAround();
        UpdateEnemies();
    }

    void UpdateEnemies(){
        foreach (GameObject enemy in enemies)
        {
            distanceToPlayer = Vector3.Distance(enemy.transform.position, transform.position);
            if(!enemy.activeSelf && distanceToPlayer <= distanceToPlayerThreshold)
                enemy.SetActive(true);
            else if(enemy.activeSelf && distanceToPlayer > distanceToPlayerThreshold)
                enemy.SetActive(false);
        }
    }

    void Move()
    {
        float moveSpeed = walkSpeed;
        if (playerInput.isRunning)
            moveSpeed = runSpeed;

        Vector2 dir = new Vector2(playerInput.vertical * moveSpeed, playerInput.horizontal * moveSpeed);
        //MoveController.Move(transform.forward * dir.x * 0.02f + transform.right * dir.y * 0.02f);
        MoveController.SimpleMove(transform.forward * dir.x + transform.right * dir.y); // for character controller

        if (Vector3.Distance(lastPosition, transform.position) > minDist)
            runFootsteps.Play();

        lastPosition = transform.position;
    }

    void LookAround()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.mouseInput.x, 1f / mouseControl.damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.mouseInput.y, 1f / mouseControl.damping.y);

        transform.Rotate(Vector3.up * mouseInput.x * mouseControl.sensitivity.x);

        playerAim.SetRotation(mouseInput.y * mouseControl.sensitivity.y);
    }
}
