using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreditMenu : Menu
{
    [Header("Inherit References :")]
    [SerializeField] private Button _backButton;

    [Space]
    [SerializeField] private TMP_Text _titleText;

    [Space]
    [SerializeField] private TMP_Text _devLabelText;
    [SerializeField] private TMP_Text _devText;

    [Space]
    [SerializeField] private TMP_Text _contactLabelText;
    [SerializeField] private TMP_Text _contactText;

    [Space]
    [SerializeField] private TMP_Text _sfxLabelText;
    [SerializeField] private TMP_Text _sfxText;

    [Header("Game Database :")]
    [SerializeField] private CreditDataSO _data;

    public override void SetEnable()
    {
        base.SetEnable();

        _backButton.interactable = true;
    }

    private void Start()
    {
        OnButtonPressed(_backButton, HandleBackButtonPressed);

        _titleText.text = _data.GetTitle;

        _devLabelText.text = _data.GetDevLabelText;
        _devText.text = _data.GetDevText;

        _contactLabelText.text = _data.GetContactLabelText;
        _contactText.text = _data.GetContactText;

        _sfxLabelText.text = _data.GetSfxLabelText;
        _sfxText.text = _data.GetSfxText;
    }

    private void HandleBackButtonPressed()
    {
        _backButton.interactable = false;

        MenuManager.Instance.CloseMenu();
    }
}
