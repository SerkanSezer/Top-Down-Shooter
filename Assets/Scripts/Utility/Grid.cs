using UnityEngine;
using System;

[Serializable]
public class Grid {
    private GridNode[,] gridArray;
    private int width;
    private int height;
    private int cellSize;
    private Vector2 originPos;

    public Grid(int width, int height,int cellSize, Vector2 originPos) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPos = originPos;
        gridArray = new GridNode[width,height];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                gridArray[x, y] = new GridNode(this, x, y);
                gridArray[x, y].worldPos = GetWorldPosition(x, y);
            }
        }
    }

    public int GetWidth() {
        return width;
    }

    public int GetHeight() {
        return height;
    }
    public float GetCellSize() {
        return cellSize;
    }
    public Vector2 GetWorldPosition(int x, int y) {
        return new Vector2(x+ originPos.x+ (float)cellSize/2, y+ originPos.y+ (float)cellSize/2) * cellSize;
    }
    public void GetXY(Vector3 worldPosition, out int x, out int y) {
        x = Mathf.FloorToInt(worldPosition.x - originPos.x);
        y = Mathf.FloorToInt(worldPosition.y - originPos.y);
    }

    public GridNode[,] GetGridArray() {
        return gridArray;
    }
    public GridNode GetGridObject(int x, int y) {
        if (x >= 0 && y >= 0 && x < width && y < height) {
            return gridArray[x, y];
        }
        else {
            return default(GridNode);
        }
    }
}

[Serializable]
public class GridNode {
    public Grid grid;
    public int x;
    public int y;
    public int gCost;
    public int hCost;
    public int fCost;
    public Vector2 worldPos;
    public bool isWalkable;
    public GridNode cameFromNode;

    public GridNode(Grid grid, int x, int y) {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = true;
    }
    public void CalculateFCost() {
        fCost = gCost + hCost;
    }

    public void SetIsWalkable(bool isWalkable) {
        this.isWalkable = isWalkable;
    }
}

