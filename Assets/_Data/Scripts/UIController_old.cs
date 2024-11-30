using UnityEngine;
using UnityEngine.UIElements;

public class UIController_old : MonoBehaviour
{
    [SerializeField] private UIDocument startupMenu;
    [SerializeField] private UIDocument mainMenu;
    [SerializeField] private UIDocument optionsMenu;

    private Button startupOptions;
    private Button startupSkip;

    private Button optionsBack;
    private Button optionsApply;

    private Button mainOptions;
    private Button mainExit;

    private void Awake()
    {
        startupOptions = startupMenu.rootVisualElement.Q("OptionsButton") as Button;
        startupOptions.RegisterCallback<ClickEvent>(OnOptionsButtonClicked);
        startupSkip = startupMenu.rootVisualElement.Q("SkipButton") as Button;
        startupSkip.RegisterCallback<ClickEvent>(OnStartupSkipButtonClicked);
    }

    private void OnOptionsButtonClicked(ClickEvent e)
    {
        optionsMenu.enabled = true;
        mainMenu.enabled = false;
        startupMenu.enabled = false;

        VisualElement accessorRow = optionsMenu.rootVisualElement.Q("AccessorRow");
        if (!optionsBack.IsBound())
        {
            optionsBack = accessorRow.Q("BackButton") as Button;
            optionsBack.RegisterCallback<ClickEvent>(OnOptionsBackButtonClicked);
        }
        if (!optionsApply.IsBound())
        {
            optionsApply = accessorRow.Q("ApplyButton") as Button;
            // for prototype use the same callback, no settings need to be applied yet
            optionsApply.RegisterCallback<ClickEvent>(OnOptionsBackButtonClicked);
        }
    }

    private void OnStartupSkipButtonClicked(ClickEvent e)
    {
        mainMenu.enabled = true;
        startupMenu.enabled = false;

        if (!mainOptions.IsBound())
        {
            mainOptions = mainMenu.rootVisualElement.Q("OptionsButton") as Button;
            mainOptions.RegisterCallback<ClickEvent>(OnOptionsButtonClicked);
        }

        if (!mainExit.IsBound())
        {
            mainExit = mainMenu.rootVisualElement.Q("ExitButton") as Button;
            mainExit.RegisterCallback<ClickEvent>(OnExitButtonClicked);
        }
    }

    private void OnOptionsBackButtonClicked(ClickEvent e)
    {
        mainMenu.enabled = true;
        optionsMenu.enabled = false;

        if (!mainOptions.IsBound())
        {
            mainOptions = mainMenu.rootVisualElement.Q("OptionsButton") as Button;
            mainOptions.RegisterCallback<ClickEvent>(OnOptionsButtonClicked);
        }

        if (!mainExit.IsBound())
        {
            mainExit = mainMenu.rootVisualElement.Q("ExitButton") as Button;
            mainExit.RegisterCallback<ClickEvent>(OnExitButtonClicked);
        }
    }

    private void OnExitButtonClicked(ClickEvent e)
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        if (startupOptions.IsBound()) startupOptions.UnregisterCallback<ClickEvent>(OnOptionsButtonClicked);
        if (startupSkip.IsBound()) startupSkip.UnregisterCallback<ClickEvent>(OnStartupSkipButtonClicked);

        if (optionsBack.IsBound()) optionsBack.UnregisterCallback<ClickEvent>(OnOptionsBackButtonClicked);
        if (optionsApply.IsBound()) optionsApply.UnregisterCallback<ClickEvent>(OnOptionsBackButtonClicked);

        if (mainOptions.IsBound()) mainOptions.UnregisterCallback<ClickEvent>(OnOptionsButtonClicked);
        if (mainExit.IsBound()) mainExit.UnregisterCallback<ClickEvent>(OnExitButtonClicked);
    }
}
