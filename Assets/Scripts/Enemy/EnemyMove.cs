using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Player player;
    private GridCreator gridCreator;
    private const float PATROL_RADIUS = 5f;
    private List<Vector3> path;
    private int pathIndex = 0;
    private Vector3 pathRot;
    private bool isPatrolling = false; 
    private bool isChasing = false; 
    private Vector3 startPosition;
    private Vector3 heroPrevPosition;
    private float faceOnSpeed = 5;
    private const float PATROL_SPEED = 4;
    private const float CHASE_SPEED = 8;

    private void Awake() {
        gridCreator = FindObjectOfType<GridCreator>();
        player = FindObjectOfType<Player>(true);
        startPosition = transform.position;
    }

    public void Patrol() {
        if (!isPatrolling) {
            path = gridCreator.GetPathFinding().FindPath(transform.position, transform.position+Random.insideUnitSphere * PATROL_RADIUS);
            if (path != null) {
                pathIndex = 0;
                isPatrolling = true;
                pathRot = (path[pathIndex] - transform.position).normalized;
            }
        }
        else {
            if(Vector3.Distance(transform.position, path[pathIndex]) > 0.01f) {
                transform.position = Vector3.MoveTowards(transform.position, path[pathIndex], PATROL_SPEED * Time.deltaTime);
                transform.right = Vector3.Lerp(transform.right, pathRot, Time.deltaTime * faceOnSpeed*3);
            }
            else {
                pathIndex++;
                if (pathIndex >= path.Count) {
                    if (Vector3.Distance(transform.position, startPosition) > PATROL_RADIUS) {
                        var path = gridCreator.GetPathFinding().FindPath(transform.position, startPosition);
                        if (path != null) {
                            this.path = path;
                            isPatrolling = true;
                            pathIndex = 0;
                            pathRot = (path[pathIndex] - transform.position).normalized;
                        }
                        else {
                            pathIndex = 0;
                            isPatrolling = false;
                        }
                    }
                    else {
                        pathIndex = 0;
                        isPatrolling = false;
                    }
                }
                else {
                    pathRot = (path[pathIndex] - transform.position).normalized;
                }
            }
        }
        CheckDoor();
    }

    public void Chase() {
        if (isPatrolling) { isPatrolling = false; }
        if (!isChasing) {
            path = gridCreator.GetPathFinding().FindPath(transform.position, player.transform.position);
            if (path != null) {
                pathIndex = 0;
                isChasing = true;
                pathRot = (path[pathIndex] - transform.position).normalized;
                heroPrevPosition = player.transform.position;
            }
        }
        else {
            if (Vector3.Distance(transform.position, path[pathIndex]) > 0.01f) {
                transform.position = Vector3.MoveTowards(transform.position, path[pathIndex], CHASE_SPEED * Time.deltaTime);
                transform.right = Vector3.Lerp(transform.right, pathRot, Time.deltaTime * faceOnSpeed * 3);
            }
            else {
                pathIndex++;
                if (pathIndex >= path.Count) {
                    pathIndex = 0;
                    isChasing = false;
                }
                else {
                    pathRot = (path[pathIndex] - transform.position).normalized;
                }
                if (Vector3.Distance(heroPrevPosition, player.transform.position) > 3) {
                    isChasing = false;
                }
            }
        }
        CheckDoor();
    }

    public void FaceOnPlayer() {
        transform.right = Vector3.Lerp(transform.right, (player.transform.position - transform.position).normalized, Time.deltaTime* faceOnSpeed);
    }

    public void CheckDoor() {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.right, 1);
        foreach (var hit in hits) {
            if(hit.transform.TryGetComponent<IInteractable>(out IInteractable interactable)) {
                if(interactable is Door) {
                    interactable.Interact();
                }
            }
        }
    }
}
