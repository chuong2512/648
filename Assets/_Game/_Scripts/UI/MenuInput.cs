using UnityEngine;

public class MenuInput : MonoBehaviour
{
    MenuManager _menu;

    private void Awake()
    {
        _menu = GetComponent<MenuManager>();
    }

    private void Update()
    {
        GetMobileInput();
    }

    private void GetMobileInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_menu.GetCurrentMenu == MenuType.Main)
            {
                _menu.OpenMenu(MenuType.Exit);
            }
            else if (_menu.GetCurrentMenu == MenuType.Setting ||
                     _menu.GetCurrentMenu == MenuType.Credit ||
                     _menu.GetCurrentMenu == MenuType.Rate ||
                     _menu.GetCurrentMenu == MenuType.Exit)
            {
                _menu.CloseMenu();
            }
            else if(_menu.GetCurrentMenu == MenuType.Gameplay)
            {
                Time.timeScale = 0f;
                InputHandler.IsPaused = true;
                _menu.SwitchMenu(MenuType.Pause);
            }
            else if (_menu.GetCurrentMenu == MenuType.Pause)
            {
                Time.timeScale = 1f;
                InputHandler.IsPaused = false;
                _menu.SwitchMenu(MenuType.Gameplay);
            }
        }
    }
}
