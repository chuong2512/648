using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static bool IsPaused = false;

    [SerializeField] bool _keyboardInputEnabled = false;

    [Space]
    [SerializeField] float _moveDelay = 0.1f;
    [SerializeField] float _moveDownDelay = 0.1f;

    private float _moveTime;
    private float _moveDownTime;

    [SerializeField] ButtonDownUpHandler _moveLeftButton;
    [SerializeField] ButtonDownUpHandler _moveRightButton;
    [SerializeField] ButtonDownUpHandler _moveDownButton;

    [SerializeField] ButtonDownUpHandler _hardDropButton;

    [SerializeField] ButtonDownUpHandler _rotateClockwiseButton;
    [SerializeField] ButtonDownUpHandler _rotateAntiClockwiseButton;

    [SerializeField] ButtonDownUpHandler _holdButton;

    Piece _piece;
    Board _board;

    bool _isMoveLeft = false;
    bool _isMoveRight = false;
    bool _isMoveDown = false;

    bool _keyDownRelease = false;

    bool _isGameStart = false;

    private void Start()
    {
        _moveLeftButton.OnButtonDown += () =>
        {
            _isMoveLeft = true;
            _moveTime = Time.time + (_moveDelay * 2);
            _piece.MoveShape(Vector2Int.left);
        };
        _moveLeftButton.OnButtonUp += () => _isMoveLeft = false;

        _moveRightButton.OnButtonDown += () =>
        {
            _isMoveRight = true;
            _moveTime = Time.time + (_moveDelay * 2);
            _piece.MoveShape(Vector2Int.right);
        };
        _moveRightButton.OnButtonUp += () => _isMoveRight = false;

        _moveDownButton.OnButtonDown += () =>
        {
            _isMoveDown = true;
            _moveDownTime = Time.time + _moveDownDelay;
            _piece.MoveShape(Vector2Int.down);

            SoundController.Instance.PlayAudio(AudioType.MOVE);
        };
        _moveDownButton.OnButtonUp += () => _isMoveDown = false;

        _hardDropButton.OnButtonDown += () =>
        {
            _isMoveDown = false;
            _piece.HardDrop();
        };

        _rotateClockwiseButton.OnButtonDown += () => _piece.Rotate(1);
        _rotateAntiClockwiseButton.OnButtonDown += () => _piece.Rotate(-1);

        _holdButton.OnButtonDown += () =>
        {
            _board.HoldPiece();
            SoundController.Instance.PlayAudio(AudioType.MOVE);
        };
    }

    public void Initialize(Piece piece, Board board)
    {
        _piece = piece;
        _board = board;
    }

    void MoveHold()
    {
        if (Time.time > _moveTime)
        {
            if (_isMoveLeft)
            {
                _moveTime = Time.time + _moveDelay;
                _piece.MoveShape(Vector2Int.left);
            }

            else if (_isMoveRight)
            {
                _moveTime = Time.time + _moveDelay;
                _piece.MoveShape(Vector2Int.right);
            }
        }

        if (_isMoveDown && Time.time > _moveDownTime)
        {
            _moveDownTime = Time.time + _moveDownDelay;
            bool canMove = _piece.MoveShape(Vector2Int.down);
            if (!canMove)
            {
                _isMoveDown = false;
                _piece.Lock();
            }
        }
    }

    private void Update()
    {
        if (!_isGameStart) return;

        MoveHold();

#if UNITY_EDITOR
        if (IsPaused || !_keyboardInputEnabled) return;
        KeyboardMoveInputHandler();
#endif
    }

    public void StartGame()
    {
        _isGameStart = true;
    }

    void KeyboardMoveInputHandler()
    {
        if ((Input.GetKey(KeyCode.LeftArrow) && Time.time > _moveTime) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _moveTime = Time.time + _moveDelay;
            _piece.MoveShape(Vector2Int.left);
        }
        else if ((Input.GetKey(KeyCode.RightArrow) && Time.time > _moveTime) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _moveTime = Time.time + _moveDelay;
            _piece.MoveShape(Vector2Int.right);
        }

        if ((!_keyDownRelease && Input.GetKey(KeyCode.DownArrow) && Time.time > _moveDownTime) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _moveDownTime = Time.time + _moveDownDelay;
            //_piece.MoveShape(Vector2Int.down);
            bool canMove = _piece.MoveShape(Vector2Int.down);
            if (!canMove)
            {
                _keyDownRelease = true;
                _piece.Lock();
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _keyDownRelease = false;
            SoundController.Instance.PlayAudio(AudioType.MOVE);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _keyDownRelease = true;
            _piece.HardDrop();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _piece.Rotate(1);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            _piece.Rotate(-1);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            _board.HoldPiece();
        }
    }
}
