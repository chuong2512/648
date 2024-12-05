using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public struct TetrominoData
{
    public Tile tile;
    public Tetromino tetromino;

    public Vector2Int[] cells;
    public Vector2Int[,] wallKicks { get; private set; }

    public void Initialize()
    {
        cells = Data.Cells[tetromino];
        wallKicks = Data.WallKicks[tetromino];
    }
}

public enum Tetromino
{
    I, O, T, J, L, S, Z
}

