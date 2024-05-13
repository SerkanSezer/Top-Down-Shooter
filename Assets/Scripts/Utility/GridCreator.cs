using UnityEngine;

public class GridCreator : MonoBehaviour {

    private Pathfinding pathfinding;
    private Grid grid;
    private BoxCollider2D boxCollider2D;
    private int[] walkableLayerList = {8,9};
    private Vector3 m_Min, m_Max;
    private Vector2 leftBottom;
    private Vector2 topRight;
    private int floorLayer;
    [SerializeField] private Transform gridNodeVisual;
    [SerializeField] private bool gridVisual = false;

    private void Awake() {
        boxCollider2D = GetComponent<BoxCollider2D>();
        floorLayer = LayerMask.NameToLayer("Floor");
        m_Min = boxCollider2D.bounds.min;
        m_Max = boxCollider2D.bounds.max;
        boxCollider2D.enabled = false;
        leftBottom.x = Mathf.FloorToInt(m_Min.x);
        leftBottom.y = Mathf.FloorToInt(m_Min.y);
        topRight.x = Mathf.FloorToInt(m_Max.x);
        topRight.y = Mathf.FloorToInt(m_Max.y);
        CreateGridMap(leftBottom, topRight);
    }

    public void CreateGridMap(Vector2 leftBottom, Vector2 topRight) {
        pathfinding = new Pathfinding((int)(topRight.x - leftBottom.x), (int)(topRight.y - leftBottom.y), 1, leftBottom);
        grid = pathfinding.GetGrid();
        GridNode[,] gridArray = grid.GetGridArray();
        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int y = 0; y < gridArray.GetLength(1); y++) {
                Vector2 tempPos = gridArray[x, y].worldPos;
                RaycastHit2D[] hits = Physics2D.RaycastAll(tempPos, Vector3.forward, 10);
                if (hits.Length == 1 && hits[0].transform.gameObject.layer == floorLayer) {
                    gridArray[x, y].SetIsWalkable(true);
                }
                else if ((hits.Length == 2 && hits[0].transform.gameObject.layer == floorLayer && IsWalkableLayer(hits[1].transform.gameObject.layer)) ||
                         (hits.Length == 2 && IsWalkableLayer(hits[0].transform.gameObject.layer) && hits[1].transform.gameObject.layer == floorLayer)) {
                    gridArray[x, y].SetIsWalkable(true);
                }
                else {
                    gridArray[x, y].SetIsWalkable(false);
                }
                if (gridVisual) {
                    var node = Instantiate(gridNodeVisual, tempPos, Quaternion.identity);
                    node.transform.position = new Vector3(node.transform.position.x, node.transform.position.y, -5);
                    if (gridArray[x, y].isWalkable) {
                        node.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.1f);
                    }else{
                        node.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.1f);
                    }
                }
            }
        }
    }
    public Pathfinding GetPathFinding() {
        return pathfinding;
    }
    public bool IsWalkableLayer(int lIndex) {
        foreach (var layerIndex in walkableLayerList) {
            if (lIndex == layerIndex)
                return true;
        }
        return false;
    }

}
