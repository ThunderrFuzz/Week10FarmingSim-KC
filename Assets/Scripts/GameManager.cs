using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public TMP_Dropdown cropDropdown;
    public List<Crop> cropOptions;
    private Unit selectedUnit;
    private int currentSelectionIndex = -1;
    public WatchTower wt_;

    void Start()
    {
        List<string> options = new List<string>();
        foreach (Crop crop in cropOptions)
        {
            options.Add(crop.name);
        }
        cropDropdown.AddOptions(options);
        cropDropdown.onValueChanged.AddListener(delegate { OnCropSelected(cropDropdown); });
        cropDropdown.gameObject.SetActive(false);

        Debug.Log("GameManager initialized.");
    }

    void Update()
    {
        HandleUnitSelection();
        HandleCropDropdownToggle();
    }

    void HandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUIElement()) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("Unit"))
                {
                    Unit unit = hit.transform.GetComponent<Unit>();
                    if (unit != null)
                    {
                        OnUnitSelected(unit);
                    }
                }
                else if (selectedUnit != null)
                {
                    if (selectedUnit is Tractor tractor)
                    {
                        tractor.SetDestination(hit.point);
                    }
                    else if (selectedUnit is Harvester harvester)
                    {
                        harvester.SetDestination(hit.point);
                    }
                    else if (selectedUnit is PlanterManager planterManager)
                    {
                        planterManager.SetDestination(hit.point);
                    }
                    else if (selectedUnit is HarvesterManager harvesterManager)
                    {
                        harvesterManager.SetDestination(hit.point);
                    }
                }
            }
        }
    }

    bool IsPointerOverUIElement()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }

    void HandleCropDropdownToggle()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCropDropdown();
        }
    }

    void OnUnitSelected(Unit unit)
    {
        selectedUnit = unit;
        Debug.Log("Unit selected: " + unit.name);
        if (unit is Tractor tractor)
        {
            OnTractorSelected(tractor);
        }
    }

    public void OnTractorSelected(Tractor tractor)
    {
        cropDropdown.gameObject.SetActive(true);
        if (currentSelectionIndex >= 0)
        {
            cropDropdown.value = currentSelectionIndex;
        }
        Debug.Log("Tractor selected, showing crop dropdown.");
    }

    public void OnCropSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        if (index != currentSelectionIndex)
        {
            currentSelectionIndex = index;
            if (selectedUnit is Tractor tractor)
            {
                tractor.selectedCrop = cropOptions[index];
                Debug.Log("Crop selected: " + cropOptions[index].name);
            }
        }
    }

    void ToggleCropDropdown()
    {
        bool isActive = cropDropdown.gameObject.activeSelf;
        cropDropdown.gameObject.SetActive(!isActive);
        Debug.Log("Toggled crop dropdown, now " + (isActive ? "inactive" : "active"));
    }
}
