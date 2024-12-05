using UnityEngine;

[CreateAssetMenu()]
public class CreditDataSO : ScriptableObject
{
    [Space]
    [SerializeField] string _title;

    [Header("Credit Data :")]
    [SerializeField] string _textLabel1;
    [SerializeField] string _text1;
    [Space]
    [SerializeField] string _textLabel2;
    [SerializeField] string _text2;
    [Space]
    [SerializeField] string _textLabel3;
    [SerializeField] string _text3;

    public string GetTitle => _title;

    public string GetDevText => _text1;
    public string GetDevLabelText => _textLabel1;

    public string GetContactText => _text2;
    public string GetContactLabelText => _textLabel2;

    public string GetSfxText => _text3;
    public string GetSfxLabelText => _textLabel3;
}
