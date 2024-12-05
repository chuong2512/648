using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    [SerializeField] Vector2Int _boardSize = new Vector2Int(10, 20);
    [SerializeField] Vector3Int _spawnPosition = new Vector3Int(-1, 8, 0);
    [SerializeField] Vector2Int _boundPosition = new Vector2Int(-5, -10);

    public Vector2Int boardSize => _boardSize;

    [SerializeField] TetrominoDataSO _tetrominoData;
    [SerializeField] NextShape _nextShapeHolder;
    [SerializeField] NextShape _holdShapeHolder;
    [SerializeField] InputHandler _inputHandler;

    Tilemap _tilemap;
    Piece _piece;

    TetrominoData _currentData;
    TetrominoData _nextData;
    TetrominoData _holdData;

    bool _isHoldingPiece = false;
    bool _holdTrigger = false;

    bool _isGameEnd = false;

    public event Action<int> OnClearLines;
    public event Action OnGameEnd;

    public Piece GetPiece => _piece;

    public RectInt Bounds
    {
        get
        {
            //Vector2Int position = new Vector2Int(-_boardSize.x / 2, -_boardSize.y / 2);
            return new RectInt(_boundPosition, _boardSize);
        }
    }

    private void Awake()
    {
        _tilemap = GetComponentInChildren<Tilemap>();
        _piece = GetComponent<Piece>();

        _inputHandler.Initialize(_piece, this);
    }

    public void GameEnd()
    {
        _isGameEnd = true;
        _piece.IsGameEnd = true;
    }

    public void StartGame()
    {
        _currentData = _tetrominoData.GetRandomTetromino;
        _nextData = _tetrominoData.GetRandomTetromino;

        GenerateNewPiece();
        _piece.IsGameStart = true;
    }

    public void HoldPiece()
    {
        if (_isGameEnd) return;

        if (_holdTrigger) return;
        _holdTrigger = true;

        if (!_isHoldingPiece)
        {
            _isHoldingPiece = true;
            _holdData = _currentData;
            _currentData = _nextData;
            _nextData = _tetrominoData.GetRandomTetromino;
        }
        else
        {
            TetrominoData tempData = _holdData;
            _holdData = _currentData;
            _currentData = tempData;
        }

        _holdShapeHolder.ChangePieceDisplay(_holdData);

        Clear(_piece);
        GenerateNewPiece();
    }

    public void GenerateNewPiece()
    {
        _piece.Initialize(this, _spawnPosition, _currentData);

        if (!IsValidPosition(_piece, _spawnPosition))
        {
            _piece.IsGameEnd = true;
            _isGameEnd = true;

            OnGameEnd?.Invoke();
        }
        else
        {
            Set(_piece);
        }

        _nextShapeHolder.ChangePieceDisplay(_nextData);
    }

    public void Set(Piece piece)
    {
        foreach (Vector3Int cellPosition in piece.cells)
        {
            Vector3Int tilePosition = cellPosition + piece.position;
            _tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }

    public void Clear(Piece piece)
    {
        foreach (Vector3Int cellPosition in piece.cells)
        {
            Vector3Int tilePosition = cellPosition + piece.position;
            _tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = Bounds;

        foreach (Vector3Int cellPosition in piece.cells)
        {
            Vector3Int tilePosition = cellPosition + position;

            if (!bounds.Contains((Vector2Int)tilePosition)) { return false; }
            if (_tilemap.HasTile(tilePosition)) { return false; }
        }
        return true;
    }

    public void ClearLines()
    {
        RectInt bounds = Bounds;
        int row = bounds.yMin;

        int clearCount = 0;

        // Clear from bottom to top
        while (row < bounds.yMax)
        {
            // Only advance to the next row if the current is not cleared
            // because the tiles above will fall down when a row is cleared
            if (IsLineFull(row))
            {
                LineClear(row);
                clearCount++;
            }
            else
            {
                row++;
            }
        }

        OnClearLines.Invoke(clearCount);

        _holdTrigger = false;
        _currentData = _nextData;
        _nextData = _tetrominoData.GetRandomTetromino;

        if (clearCount > 0) SoundController.Instance.PlayAudio(AudioType.CLEARLINE);
    }

    bool IsLineFull(int row)
    {
        RectInt bounds = Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);

            // The line is not full if a tile is missing
            if (!_tilemap.HasTile(position))
            {
                return false;
            }
        }

        return true;
    }

    void LineClear(int row)
    {
        RectInt bounds = Bounds;

        // Clear all tiles in the row
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            _tilemap.SetTile(position, null);
        }

        // Shift every row above down one
        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = _tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                _tilemap.SetTile(position, above);
            }

            row++;
        }
    }
}
