using System;
using UnityEngine;
using static PuzzleHelper;

public class RiverPuzzle : MonoBehaviour
{
    [SerializeField] WaterTarget target;
    [SerializeField] WaterMovement waterOne;
    [SerializeField] WaterMovement waterTwo;
    [SerializeField] GameObject puzzleUI;
    [SerializeField] WaterHazards hazards;

    [Header("CustomizationFields")]
    [SerializeField] float waterSpeed = 5;

    PuzzleData currentPuzzle;

    private void Start()
    {
        TogglePuzzleDisplay(false);
    }

    public void StartPuzzle(PuzzleData puzzleData)
    {
        currentPuzzle = puzzleData;
        target.SetItem(puzzleData.Item.itemImage);
        target.SetSpeed(waterSpeed);
        waterOne.SetSpeed(waterSpeed);
        waterTwo.SetSpeed(waterSpeed);

        SubscribeToEvents();
        TogglePuzzleDisplay(true);
    }

    private void SubscribeToEvents()
    {
        Controller.OnCancel += EndPuzzle;
        target.OnTargetClicked += OnTargetClicked;
        hazards.OnHazardClicked += OnWaterHazardClicked;
    }

    public void OnWaterHazardClicked()
    {
        
    }

    public void OnTargetClicked()
    {
        InventoryManager.Instance.GainItem(currentPuzzle.Item);
        EndPuzzle();
    }

    public void TogglePuzzleDisplay(bool isEnabled)
    {
        puzzleUI.SetActive(isEnabled);
        target.enabled = isEnabled;
        waterOne.enabled = isEnabled;
        waterTwo.enabled = isEnabled;
    }

    private void EndPuzzle()
    {
        UnsubscribeToEvents();
        TogglePuzzleDisplay(false);
        PuzzleSystem.Instance.ExitPuzzle();
    }

    private void UnsubscribeToEvents()
    {
        Controller.OnCancel -= EndPuzzle;
        target.OnTargetClicked -= OnTargetClicked;
        hazards.OnHazardClicked -= OnWaterHazardClicked;
    }
}
