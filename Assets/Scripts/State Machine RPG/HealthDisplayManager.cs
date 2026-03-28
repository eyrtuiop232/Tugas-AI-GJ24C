using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Attach to any GameObject in the scene.
// Assign your existing Screen Space - Camera canvas to the Canvas field.
public class HealthDisplayManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private float fontSize = 24f;

    private Camera mainCam;
    private RectTransform canvasRect;

    // Maps each character to its health label
    private readonly Dictionary<Character, TMP_Text> labels = new();
    // Tracks destroyed characters whose labels still need cleanup
    private readonly List<Character> toRemove = new();

    private void Start()
    {
        mainCam = Camera.main;
        canvasRect = canvas.GetComponent<RectTransform>();

        foreach (Character c in FindObjectsByType<Character>(FindObjectsSortMode.None))
            SpawnLabel(c);
    }

    private void SpawnLabel(Character character)
    {
        GameObject go = new GameObject($"HP_{character.name}");
        go.transform.SetParent(canvasRect, false);

        TMP_Text text = go.AddComponent<TextMeshProUGUI>();
        text.fontSize = fontSize;
        text.alignment = TextAlignmentOptions.Center;
        text.text = character.Health.ToString("0");

        RectTransform rt = text.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(80f, 30f);

        labels[character] = text;
    }

    private void Update()
    {
        foreach (var kvp in labels)
        {
            Character character = kvp.Key;
            TMP_Text text = kvp.Value;

            // Character was destroyed — queue label for removal
            if (character == null)
            {
                Destroy(text.gameObject);
                toRemove.Add(character);
                continue;
            }

            // Update health value
            text.text = character.Health.ToString("0");

            // Convert world position to canvas local position
            Vector2 screenPos = mainCam.WorldToScreenPoint(character.transform.position);
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, mainCam, out Vector2 localPos))
                text.rectTransform.anchoredPosition = localPos;
        }

        // Clean up dead entries
        foreach (Character c in toRemove)
            labels.Remove(c);

        toRemove.Clear();
    }
}
