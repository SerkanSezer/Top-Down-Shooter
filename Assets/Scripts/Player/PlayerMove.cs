using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCam;
    private CinemachineFramingTransposer mFramingTransposer;
    private Animator animator;
    private Player player;
    private const int WALK_LAYER_INDEX = 4;
    private InputActions inputActions;
    private Camera mainCam;
    private Vector3 mousePos;
    private Vector2 mouseLook;
    private Vector2 move;
    private const float playerSpeed = 12f;
    [SerializeField] LayerMask walkableMask;

    private void Awake() {
        mainCam = Camera.main;
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        virtualCam = FindObjectOfType<CinemachineVirtualCamera>();
        mFramingTransposer = virtualCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        mFramingTransposer.m_ScreenX = 0.5f;
        mFramingTransposer.m_ScreenY = 0.5f;
    }

    private void Start() {
        inputActions = player.GetInputActions();
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        HeroLookAhead(mousePos);
        Move();
    }

    public void HeroLookAhead(Vector2 mousePos) {
        Vector2 diffVec = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        mouseLook.x = Remap(Mathf.Clamp(diffVec.x, -25f, 25f), -25f, 25f, 0.8f, 0.2f);
        mouseLook.y = Remap(Mathf.Clamp(diffVec.y, -15f, 15f), -15f, 15f, 0.2f, 0.8f);
        mFramingTransposer.m_ScreenX = mouseLook.x;
        mFramingTransposer.m_ScreenY = mouseLook.y;
        transform.right = (new Vector3(mousePos.x,mousePos.y,0) - transform.position).normalized;
    }

    public void Move() {
        move = inputActions.Player.Move.ReadValue<Vector2>().normalized;
        Vector2 validMove = Vector2.zero;
        if(move.x > 0 && !Physics2D.Raycast(transform.position, Vector3.right, 1.5f, walkableMask)) {
            validMove.x = move.x;
        }else if(move.x < 0 && !Physics2D.Raycast(transform.position, -Vector3.right, 1.5f, walkableMask)) {
            validMove.x = move.x;
        }
        if (move.y > 0 && !Physics2D.Raycast(transform.position, Vector3.up, 1.5f, walkableMask)) {
            validMove.y = move.y;
        }
        else if (move.y < 0 && !Physics2D.Raycast(transform.position, -Vector3.up, 1.5f, walkableMask)) {
            validMove.y = move.y;
        }
        if ((validMove.x != 0 || validMove.y != 0)) {
            transform.position += new Vector3(validMove.x, validMove.y, 0) * Time.deltaTime * playerSpeed;
            animator.SetLayerWeight(WALK_LAYER_INDEX, 1);
        }
        else {
            animator.SetLayerWeight(WALK_LAYER_INDEX, 0);
        }
    }

    public float Remap(float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}

