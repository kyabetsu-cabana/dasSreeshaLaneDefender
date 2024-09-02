using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    // Gives access to the control map
    public PlayerInput PlayerInputInstance;

    // List of actions
    public InputAction MoveAction;
    public InputAction ShootAction;
    public InputAction PauseAction;
    public InputAction RestartAction;
    public InputAction QuitAction;

    // Movement values
    [SerializeField] private float tankMoveDirection;
    [SerializeField] private float tankMoveSpeed;
    [SerializeField] private bool tankMoves;
    [SerializeField] private bool tankFires;
    [SerializeField] private float fireForce;

    // Grabs the player's Rigidbody2D
    public Rigidbody2D TankRB2D;

    // Setups for making the bullet function
    public GameObject Bullet;
    public Transform BulletSpawnPoint;
    [SerializeField] private float fireTimer;
    [SerializeField] private float timerMax;

    // Start is called before the first frame update
    void Start()
    {
        // Enables the current action map
        PlayerInputInstance = GetComponent<PlayerInput>();
        PlayerInputInstance.currentActionMap.Enable();

        // Finds these actions
        MoveAction = PlayerInputInstance.currentActionMap.FindAction("Move");
        ShootAction = PlayerInputInstance.currentActionMap.FindAction("Shoot");
        PauseAction = PlayerInputInstance.currentActionMap.FindAction("Pause");
        RestartAction = PlayerInputInstance.currentActionMap.FindAction("Restart");
        QuitAction = PlayerInputInstance.currentActionMap.FindAction("Quit");

        // Starts listening for these events
        MoveAction.performed += MoveAction_performed;
        MoveAction.canceled += MoveAction_canceled;
        ShootAction.performed += ShootAction_performed;
        ShootAction.canceled += ShootAction_canceled;
        PauseAction.performed += PauseAction_performed;
        RestartAction.performed += RestartAction_performed;
        QuitAction.performed += QuitAction_performed;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks to see if the tank is moving
        if (tankMoves == true)
        {
            // Checks what direction the tank is moving
            tankMoveDirection = MoveAction.ReadValue<float>();

            // Moves the tank according to direction
            TankRB2D.velocity = new Vector2(TankRB2D.velocity.x, tankMoveDirection * tankMoveSpeed);
        }
        // Stops moving the tank
        else
        {
            // Sets the velocity of the tank to 0
            TankRB2D.velocity = new Vector2(TankRB2D.velocity.x, 0);
        }  

        // Checks to see if the tank is firing
        if (tankFires == true)
        {
            // Start counting down
            fireTimer += Time.deltaTime;

            // Checks to see if the timer reaches the maximum number
            if (fireTimer > timerMax)
            {
                // Resets timer to 0
                fireTimer = 0;

                // Spawns bullets at the bullet spawn point
                GameObject bullet = Instantiate(Bullet, BulletSpawnPoint.position, Quaternion.identity);

                // Makes the bullets fire to the right
                bullet.GetComponent<Rigidbody2D>().AddForce(BulletSpawnPoint.right * fireForce, ForceMode2D.Impulse);
            }
        }
    }

    // Starts moving the tank across lanes
    private void MoveAction_performed(InputAction.CallbackContext obj)
    {
        tankMoves = true;
    }

    // Stops moving the tank
    private void MoveAction_canceled(InputAction.CallbackContext obj)
    {
        tankMoves = false;
    }

    // Makes the tank fire bullets
    private void ShootAction_performed(InputAction.CallbackContext obj)
    {
        tankFires = true;
    }

    // Makes the tank stop firing bullets
    private void ShootAction_canceled(InputAction.CallbackContext obj)
    {
        tankFires = false;
    }

    // Pauses the game
    private void PauseAction_performed(InputAction.CallbackContext obj)
    {

    }

    // Resets the game to its original state
    private void RestartAction_performed(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(0);
    }

    // Quits the game
    private void QuitAction_performed(InputAction.CallbackContext obj)
    {
        Application.Quit();
    }

    // Stops listening for these events
    public void OnDestroy()
    {
        // Starts listening for these events
        MoveAction.performed -= MoveAction_performed;
        MoveAction.canceled -= MoveAction_canceled;
        ShootAction.performed -= ShootAction_performed;
        ShootAction.canceled -= ShootAction_canceled;
        PauseAction.performed -= PauseAction_performed;
        RestartAction.performed -= RestartAction_performed;
        QuitAction.performed -= QuitAction_performed;
    }
}
