using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InstructionView : MonoBehaviour
{
    [SerializeField] private InstructionSciprtableObject playerSpawnedInstruction;
    [SerializeField] private InstructionSciprtableObject interactionInstruction;
    [SerializeField] private InstructionSciprtableObject lightOffByGhostInstruction;

    [Header("Instruction Popup")]
    [SerializeField] private GameObject instructionPopup;
    [SerializeField] private TextMeshProUGUI instructionsText;

    private Coroutine instructionCoroutine;

    private void Start() => showInstruction(playerSpawnedInstruction);


    public void ShowInstruction(InstructionType type)
    {
        stopCoroutine(instructionCoroutine);
        switch (type)
        {
            case InstructionType.PlayerSpawned:
                instructionCoroutine = StartCoroutine(setInstructions(playerSpawnedInstruction));
                break;
            case InstructionType.Interact:
                instructionCoroutine = StartCoroutine(setInstructions(interactionInstruction));
                break;
            case InstructionType.LightsOff:
                instructionCoroutine = StartCoroutine(setInstructions(lightOffByGhostInstruction));
                break;
        }
    }

    public void HideInstruction() => hideInstructionPopup();

    private IEnumerator setInstructions(InstructionSciprtableObject instruction)
    {
        yield return new WaitForSeconds(instruction.WaitToTriggerDuration);
        showInstructionPopup(instruction);

        yield return new WaitForSeconds(instruction.DisplayDuration);
        hideInstructionPopup();
    }

    private void hideInstructionPopup()
    {
        instructionsText.SetText(string.Empty);
        instructionPopup.SetActive(false);
        stopCoroutine(instructionCoroutine);
    }

    private void showInstructionPopup(InstructionSciprtableObject instruction)
    {
        instructionsText.SetText(instruction.Instruction);
        instructionPopup.SetActive(true);
    }

    private void stopCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
    private void showInstruction(InstructionSciprtableObject instruction)
    {
        stopCoroutine(instructionCoroutine);
        instructionCoroutine = StartCoroutine(setInstructions(instruction));
    }
}
