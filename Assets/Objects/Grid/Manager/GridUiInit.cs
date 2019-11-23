using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridUiInit : MonoBehaviour
{
    public Dropdown linkedUiDropdown;

    void Awake()
    {
        // FUTURE: Unity 2019
        // while (linkedUiDropdown.MenuItems().Count > 0)
        // {
        //     linkedUiDropdown.RemoveItemAt(0);
        // }

        // foreach (string state in Enum.GetNames(typeof(GridManager.GridState)))
        // {
        //     linkedUiDropdown.AppendAction(state, null, null, null);
        // }

        linkedUiDropdown.ClearOptions();

        List<string> options = new List<string>(Enum.GetNames(typeof(GridState)));
        linkedUiDropdown.AddOptions(options);

    }
}
