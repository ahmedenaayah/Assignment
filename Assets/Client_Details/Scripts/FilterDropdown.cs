using UnityEngine;
using UnityEngine.UI;

public class FilterDropdown : MonoBehaviour
{
    public enum FilterOption
    {
        AllClients,
        ManagersOnly,
        NonManagersOnly
    }

    public Dropdown dropdown;
    public UIManager uiManager;

    void Start()
    {
        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown.value);
        });
    }

    void DropdownValueChanged(int value)
    {
        FilterOption selectedOption = (FilterOption)value;
       // uiManager.FilterClients(selectedOption);
    }
}
