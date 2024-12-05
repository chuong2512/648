using System.Collections;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] float _stepDelay = 1f;
    [SerializeField] float _lockDelay = 0.5f;
    [SerializeField] float _clearTime = 0.1f;

    private float _stepTime;
    private float _lockTime;

    //fix bug double tap hard drop: give it delay between hard drop.
    float hardDropDelay = .2f;
    float hardDropTime = 0;

    int _rotationIndex;
    bool _isClearTime = false;

    bool _isGameStart = false;
    bool _isGameEnd = false;

    public bool IsGameStart { set { _isGameStart = value; } }
    public bool IsGameEnd { set { _isGameEnd = value; } }

    Board _board;

    public TetrominoData data { get; private set; }
    public Vector3Int position { get; private set; }
    public Vector2Int[] cells { get; private set; }

    public void Initialize(Board board, Vector3Int position, TetrominoData data)
    {
        _board = board;
        this.data = data;
        this.position = position;

        cells = new Vector2Int[data.cells.Length];
        System.Array.Copy(data.cells, cells, data.cells.Length);

        _rotationIndex = 0;
        _stepTime = Time.time + _stepDelay;
    }

    private void Update()
    {
        if (!_isGameStart) return;
        if (_isGameEnd) return;

        _lockTime += Time.deltaTime;

        Step();
    }

    public void UpdateStepDelay(float delay)
    {
        _stepDelay = delay;
    }

    public void HardDrop()
    {
        if (_isGameEnd) return;

        if (Time.time < hardDropTime) return;
        hardDropTime = Time.time + hardDropDelay;

        while (MoveShape(Vector2Int.down))
        {
            continue;
        }

        Lock();
    }

    private void Step()
    {
        if (Time.time > _stepTime)
        {
            _stepTime = Time.time + _stepDelay;

            MoveShape(Vector2Int.down);

            if (_lockTime >= _lockDelay)
            {
                _lockTime = 0;
                Lock();
            }
        }
    }

    public void Rotate(int direction)
    {
        if (_isGameEnd) return;
        if (_isClearTime) return;

        _board.Clear(this);

        int originalRotationIndex = _rotationIndex;
        //_rotationIndex = (_rotationIndex + direction) % 4;
        _rotationIndex = (int)Mathf.Repeat(_rotationIndex + direction, 4);

        ApplyRotationMatrix(direction);

        // Revert the rotation if the wall kick tests fail
        if (!TestWallKicks(originalRotationIndex, direction))
        {
            ApplyRotationMatrix(-direction);
        }

        _board.Set(this);

        SoundController.Instance.PlayAudio(AudioType.ROTATE);
    }

    private void ApplyRotationMatrix(int direction)
    {
        float[] matrix = Data.RotationMatrix;

        for (int i = 0; i < cells.Length; i++)
        {
            Vector2 cell = cells[i];
            int x, y;

            switch (data.tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                    // "I" and "O" are rotated from an offset center point
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                    break;

                default:
                    x = Mathf.RoundToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                    break;
            }
            cells[i] = new Vector2Int(x, y);
        }
    }

    private bool TestWallKicks(int originalRotIndex, int direction)
    {
        int wallKickIndex = (originalRotIndex * 2) + (direction < 0 ? 1 : 0);
        for (int i = 0; i < data.wallKicks.GetLength(1); i++)
        {
            Vector2Int translation = data.wallKicks[wallKickIndex, i];

            if (Move(translation)) return true;
        }
        return false;
    }

    public bool MoveShape(Vector2Int translation)
    {
        if (_isGameEnd) return false;
        if (_isClearTime) return false;

        if (translation == Vector2Int.down)
            _stepTime = Time.time + _stepDelay;

        _board.Clear(this);
        bool canMove = Move(translation);
        _board.Set(this);

        bool isMoveHorizontal = translation == Vector2Int.left || translation == Vector2Int.right;
        if (canMove && isMoveHorizontal)
        {
            SoundController.Instance.PlayAudio(AudioType.MOVE);
        }

        return canMove;
    }

    private bool Move(Vector2Int translation)
    {
        Vector3Int newPosition = position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = _board.IsValidPosition(this, newPosition);

        if (valid)
        {
            position = newPosition;
            if (translation == Vector2.down)
                _lockTime = 0f;
        }
        return valid;
    }

    public void Lock()
    {
        if (_isGameEnd) return;
        _isClearTime = true;

        SoundController.Instance.PlayAudio(AudioType.DROP);

        StartCoroutine(LockRoutine());
    }

    IEnumerator LockRoutine()
    {
        yield return new WaitForSeconds(_clearTime);

        _board.ClearLines();
        _board.GenerateNewPiece();

        _isClearTime = false;
    }
}
