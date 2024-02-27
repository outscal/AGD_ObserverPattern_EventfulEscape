using UnityEngine;
public class PlayerController
{
    private PlayerView playerView;
    private PlayerScriptableObject playerScriptableObject;
    private float velocity;
    private float horizontalAxis;
    private float verticalAxis;
    private float mouseX;
    private PlayerState playerState;

    public bool IsInteracted;
    public int KeysEquipped { get => playerScriptableObject.KeysEquipped; set => playerScriptableObject.KeysEquipped = value; }
    public PlayerState PlayerState { get => playerState; private set => playerState = value; }

    public PlayerController(PlayerView playerView, PlayerScriptableObject playerScriptableObject)
    {
        this.playerView = playerView;
        this.playerView.SetController(this);
        this.playerScriptableObject = playerScriptableObject;
        this.playerScriptableObject.KeysEquipped = 0;
        playerState = PlayerState.InDark;

        EventService.Instance.OnLightsOffByGhostEvent.AddListener(onLightsOffByGhost);
        EventService.Instance.OnLightSwitchToggled.AddListener(onLightsToggled);
        EventService.Instance.OnKeyPickedUp.AddListener(OnKeyPickedUp);
        EventService.Instance.OnPlayerEscapedEvent.AddListener(DisableControls);
    }
    ~PlayerController()
    {
        EventService.Instance.OnLightsOffByGhostEvent.RemoveListener(onLightsOffByGhost);
        EventService.Instance.OnLightSwitchToggled.RemoveListener(onLightsToggled);
        EventService.Instance.OnKeyPickedUp.RemoveListener(OnKeyPickedUp);
        EventService.Instance.OnPlayerEscapedEvent.RemoveListener(DisableControls);
    }
    public void Interact() => IsInteracted = Input.GetKeyDown(KeyCode.E) ? true : (Input.GetKeyUp(KeyCode.E) ? false : IsInteracted);

    public void Jump(Rigidbody playerRigidbody, Transform transform)
    {
        bool IsGrounded = Physics.Raycast(transform.position, -transform.up, playerScriptableObject.raycastLength);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            playerRigidbody.AddForce(Vector3.up * playerScriptableObject.jumpForce, ForceMode.Impulse);
        }
    }

    public void Move(Rigidbody playerRigidbody, Transform transform)
    {
        getInput();

        Quaternion rotation;
        Vector3 position;
        calculatePositionRotation(playerRigidbody, transform, out rotation, out position);

        playerRigidbody.MoveRotation(rotation);
        playerRigidbody.MovePosition(position);
    }

    public void KillPlayer()
    {
        PlayerState = PlayerState.Dead;
        EventService.Instance.OnPlayerDeathEvent.InvokeEvent();
    }

    private void onLightsOffByGhost() => PlayerState = PlayerState.InDark;
    private void OnKeyPickedUp(int keys) => KeysEquipped = keys;
    private void DisableControls() => playerView.enabled = false;

    private void getInput()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        velocity = Input.GetKey(KeyCode.LeftShift) ? playerScriptableObject.sprintSpeed : playerScriptableObject.walkSpeed;
    }
    private void calculatePositionRotation(Rigidbody playerRigidbody, Transform transform, out Quaternion rotation, out Vector3 position)
    {
        Vector3 lookRotation = new Vector3(0, mouseX * playerScriptableObject.sensitivity, 0);
        Vector3 movement = (transform.forward * verticalAxis + transform.right * horizontalAxis);

        rotation = playerRigidbody.rotation * Quaternion.Euler(lookRotation);
        position = (transform.position) + (velocity * movement) * Time.fixedDeltaTime;
    }
    private void onLightsToggled()
    {
        if (PlayerState == PlayerState.InDark)
            PlayerState = PlayerState.None;
        else
            PlayerState = PlayerState.InDark;
    }
}
