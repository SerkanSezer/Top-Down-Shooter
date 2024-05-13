using UnityEngine;

public class CarManager : MonoBehaviour, IInteractable {

    private GameManager gameManager;
    private Transform hero;
    private Transform carLeftDoor;
    private float carSpeed = 2f;
    private Vector3 targetInPos;
    private Vector3 targetOutPos;
    private Quaternion targetInAngle;
    private Quaternion targetOutAngle;

    private bool isGameFinished = false;
    private bool isCarMovingIn = true;
    private bool isCarMoveOut = false;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnGameFinished += SetFinished;
        targetInPos = transform.position + transform.right * 30;
        targetInAngle = Quaternion.Euler(0, 0, -80);

        targetOutPos = transform.position + transform.right * 100;
        targetOutAngle = Quaternion.Euler(0, 0, 0);

        carLeftDoor = transform.Find("Door");
        hero = FindObjectOfType<Player>().transform;
        hero.transform.gameObject.SetActive(false);
    }

    void Update() {
        MoveInCar();
        MoveOutCar();
    }

    public void MoveInCar() {
        if (isCarMovingIn) {
            transform.position = Vector3.Lerp(transform.position, targetInPos, Time.deltaTime * carSpeed);
            if (Vector3.Distance(transform.position, targetInPos) < 0.1f) {
                carLeftDoor.rotation = Quaternion.RotateTowards(carLeftDoor.rotation, targetInAngle, carSpeed * 100 * Time.deltaTime);
                if (Vector3.Distance(carLeftDoor.rotation.eulerAngles, targetInAngle.eulerAngles) < 1) {
                    isCarMovingIn = false;
                    hero.position = transform.transform.position + transform.transform.up * 4;
                    hero.gameObject.SetActive(true);
                }
            }
        }
    }

    public void MoveOutCar() {
        if (isCarMoveOut) {
            carLeftDoor.rotation = Quaternion.RotateTowards(carLeftDoor.rotation, targetOutAngle, carSpeed * 100 * Time.deltaTime);
            if (Vector3.Distance(carLeftDoor.rotation.eulerAngles, targetOutAngle.eulerAngles) < 1) {
                transform.position = Vector3.Lerp(transform.position, targetOutPos, Time.deltaTime * carSpeed * 0.5f);
                if (Vector3.Distance(transform.position, targetOutPos) < 0.1f) {
                    isCarMoveOut = false;
                }
            }
        }
    }

    public void SetFinished(bool status) {
        if (status) {
            isGameFinished = true;
        }
    }

    public void Interact() {
        if (isGameFinished) {
            isCarMoveOut = true;
            gameManager.ShowGameWinPanel();
            hero.GetComponent<Player>().GetInputActions().Player.Disable();
            hero.gameObject.SetActive(false);
        }
    }
}
