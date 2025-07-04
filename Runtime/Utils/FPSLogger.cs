using UnityEngine;
using System.Collections.Generic;

public class FPSLogger : MonoBehaviour
{
    private float deltaTime = 0.0f;
    private bool showFPS = true;
    private Rect windowRect = new Rect(20, 20, 250, 200);
    private Rect buttonRect = new Rect(10, 10, 80, 30);
    private int windowID = 0;
    private bool isDragging = false;
    private Vector2 dragOffset;

    // Performance tracking
    private float minFPS = float.MaxValue;
    private float maxFPS = 0f;
    private float avgFPS = 0f;
    private int frameCount = 0;
    private float timeElapsed = 0f;
    private Queue<float> fpsHistory = new Queue<float>();
    private const int HISTORY_SIZE = 60; // Store last 60 frames

    // Style variables
    private GUIStyle windowStyle;
    private GUIStyle buttonStyle;
    private GUIStyle labelStyle;
    private GUIStyle headerStyle;

    void Awake()
    {
        InitializeStyles();
    }

    void InitializeStyles()
    {
        // Window style
        windowStyle = new GUIStyle(GUI.skin.window);
        windowStyle.normal.background = CreateColorTexture(new Color(0.2f, 0.2f, 0.2f, 0.95f));
        windowStyle.onNormal.background = windowStyle.normal.background;
        windowStyle.border = new RectOffset(10, 10, 10, 10);
        windowStyle.padding = new RectOffset(10, 10, 10, 10);

        // Button style
        buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.normal.background = CreateColorTexture(new Color(0.3f, 0.3f, 0.3f, 1f));
        buttonStyle.hover.background = CreateColorTexture(new Color(0.4f, 0.4f, 0.4f, 1f));
        buttonStyle.active.background = CreateColorTexture(new Color(0.2f, 0.2f, 0.2f, 1f));
        buttonStyle.padding = new RectOffset(10, 10, 5, 5);
        buttonStyle.border = new RectOffset(5, 5, 5, 5);

        // Label style
        labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.normal.textColor = Color.white;
        labelStyle.fontSize = 12;

        // Header style
        headerStyle = new GUIStyle(GUI.skin.label);
        headerStyle.normal.textColor = new Color(1f, 0.8f, 0.2f);
        headerStyle.fontSize = 14;
        headerStyle.fontStyle = FontStyle.Bold;
    }

    private Texture2D CreateColorTexture(Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        return texture;
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // Update FPS statistics
        float currentFPS = 1.0f / Time.unscaledDeltaTime;
        minFPS = Mathf.Min(minFPS, currentFPS);
        maxFPS = Mathf.Max(maxFPS, currentFPS);

        // Update average FPS
        fpsHistory.Enqueue(currentFPS);
        if (fpsHistory.Count > HISTORY_SIZE)
            fpsHistory.Dequeue();

        float sum = 0;
        foreach (float fps in fpsHistory)
            sum += fps;
        avgFPS = sum / fpsHistory.Count;

        // Update frame count and time
        frameCount++;
        timeElapsed += Time.unscaledDeltaTime;

        // Handle window dragging
        if (showFPS)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
                if (windowRect.Contains(mousePos))
                {
                    isDragging = true;
                    dragOffset = mousePos - new Vector2(windowRect.x, windowRect.y);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            if (isDragging)
            {
                Vector2 mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
                windowRect.x = mousePos.x - dragOffset.x;
                windowRect.y = mousePos.y - dragOffset.y;
            }
        }
    }

    void OnGUI()
    {
        if (showFPS)
        {
            windowRect = GUI.Window(windowID, windowRect, DrawWindow, "Performance Monitor", windowStyle);
        }
        else
        {
            if (GUI.Button(buttonRect, "Show FPS", buttonStyle))
            {
                showFPS = true;
            }
        }
    }

    void DrawWindow(int id)
    {
        float yPos = 30;
        float lineHeight = 25;

        // Header
        GUI.Label(new Rect(10, yPos, 230, 20), "Performance Metrics", headerStyle);
        yPos += lineHeight + 5;

        // Current FPS
        float fps = 1.0f / deltaTime;
        GUI.Label(new Rect(10, yPos, 230, 20), $"Current FPS: {fps:0.0}", labelStyle);
        yPos += lineHeight;

        // Min/Max/Avg FPS
        GUI.Label(new Rect(10, yPos, 230, 20), $"Min FPS: {minFPS:0.0} | Max FPS: {maxFPS:0.0}", labelStyle);
        yPos += lineHeight;
        GUI.Label(new Rect(10, yPos, 230, 20), $"Average FPS: {avgFPS:0.0}", labelStyle);
        yPos += lineHeight;

        // Frame Time
        GUI.Label(new Rect(10, yPos, 230, 20), $"Frame Time: {Time.unscaledDeltaTime * 1000:0.0} ms", labelStyle);
        yPos += lineHeight;

        // Memory Usage
        float totalMemory = UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong() / (1024f * 1024f);
        float reservedMemory = UnityEngine.Profiling.Profiler.GetTotalReservedMemoryLong() / (1024f * 1024f);
        GUI.Label(new Rect(10, yPos, 230, 20), $"Memory Usage: {totalMemory:0.0} MB", labelStyle);
        yPos += lineHeight;
        GUI.Label(new Rect(10, yPos, 230, 20), $"Reserved Memory: {reservedMemory:0.0} MB", labelStyle);
        yPos += lineHeight;

        // Draw toggle button
        if (GUI.Button(new Rect(10, yPos, 80, 30), "Hide FPS", buttonStyle))
        {
            showFPS = false;
            buttonRect.x = windowRect.x;
            buttonRect.y = windowRect.y;
        }
    }

    void OnDestroy()
    {
        // Clean up textures
        if (windowStyle != null && windowStyle.normal.background != null)
            Destroy(windowStyle.normal.background);
        if (buttonStyle != null && buttonStyle.normal.background != null)
            Destroy(buttonStyle.normal.background);
    }
}
