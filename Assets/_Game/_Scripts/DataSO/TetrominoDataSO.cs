using UnityEngine;

[CreateAssetMenu()]
public class TetrominoDataSO : ScriptableObject
{
    [SerializeField] TetrominoData[] _tetrominoes;

    public TetrominoData GetRandomTetromino => _tetrominoes[Random.Range(0, _tetrominoes.Length)];

    private void OnEnable()
    {
        for (int i = 0; i < _tetrominoes.Length; i++)
        {
            _tetrominoes[i].Initialize();
        }
    }
}


