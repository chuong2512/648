using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour
{
    [SerializeField] Tile _tile;
    [SerializeField] Board _mainBoard;
    [SerializeField] Piece _trackingPiece;

    Tilemap _tilemap;
    Vector2Int[] _cells;
    Vector3Int _position;

    bool _isGameStart = false;

    public bool IsGameStart { set { _isGameStart = value; } }

    private void Awake()
    {
        _tilemap = GetComponentInChildren<Tilemap>();
        _cells = new Vector2Int[4];
    }

    private void LateUpdate()
    {
        if (!_isGameStart) return;

        Clear();
        Copy();
        Drop();
        Set();
    }

    void Clear()
    {
        foreach (Vector3Int cellPosition in _cells)
        {
            Vector3Int tilePosition = cellPosition + _position;
            _tilemap.SetTile(tilePosition, null);
        }
    }

    private void Copy()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i] = _trackingPiece.cells[i];
        }
    }

    private void Drop()
    {
        Vector3Int position = _trackingPiece.position;

        int current = position.y;
        int bottom = -_mainBoard.boardSize.y / 2 - 1;

        _mainBoard.Clear(_trackingPiece);

        for (int row = current; row >= bottom; row--)
        {
            position.y = row;

            if (_mainBoard.IsValidPosition(_trackingPiece, position))
            {
                _position = position;
            }
            else
            {
                break;
            }
        }

        _mainBoard.Set(_trackingPiece);
    }

    private void Set()
    {
        foreach (Vector3Int cellPosition in _cells)
        {
            Vector3Int tilePosition = cellPosition + _position;
            _tilemap.SetTile(tilePosition, _tile);
        }
    }

}
