using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CoinPool))]
public class CoinPoolEditor : Editor
{
    private float animatedProgress = 0f; 

    private void OnEnable() // Subscribe to editor updates
    {
        EditorApplication.update += AnimateProgress; // Update the progress bar smoothly
    }

    private void OnDisable() // Unsubscribe from editor updates
    {
        EditorApplication.update -= AnimateProgress; // Prevent memory leaks
    }

    private void AnimateProgress() // Smoothly animate the progress bar
    {
        CoinPool pool = (CoinPool)target;
        if (pool == null) return;

        float targetProgress = Mathf.Clamp01((float)pool.CurrentPoolSize / pool.MaxPoolSize); // Calculate target progress based on current pool size
        animatedProgress = Mathf.Lerp(animatedProgress, targetProgress, 0.1f);               // Smoothly interpolate towards the target progress
        Repaint();                                                                        // Repaint the inspector to update the progress bar
    }

    public override void OnInspectorGUI() // Custom inspector GUI
    {
        DrawDefaultInspector();

        CoinPool pool = (CoinPool)target;

        GUILayout.Space(10);
        GUILayout.Label("📊 Pool Status", EditorStyles.boldLabel); 

        EditorGUILayout.HelpBox($"Current Pool Size: {pool.CurrentPoolSize} / Max: {pool.MaxPoolSize}", MessageType.Info);

        // Animated progress bar
        Color barColor = pool.CurrentPoolSize > pool.MaxPoolSize ? Color.red : Color.green;

        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.DrawRect(rect, Color.black);
        EditorGUI.DrawRect(new Rect(rect.x, rect.y, rect.width * animatedProgress, rect.height), barColor);
        EditorGUI.LabelField(rect, $"{(int)(animatedProgress * 100)}%", new GUIStyle(EditorStyles.whiteLabel) { alignment = TextAnchor.MiddleCenter });

        GUILayout.Space(10);
        GUILayout.Label("🛠 Debug Tools", EditorStyles.boldLabel);

        // 🎨 Custom button style
        GUIStyle bigBold = new GUIStyle(GUI.skin.button)
        {
            fontStyle = FontStyle.Bold,
            fontSize = 13,
            fixedHeight = 32,
            alignment = TextAnchor.MiddleCenter
        };

        Color originalColor = GUI.color;
        GUI.color = new Color(1f, 0.85f, 0.4f); 

        if (GUILayout.Button("🧹 Trim Pool (Remove Unnecessary Objects)", bigBold))
        {
            pool.TrimPool();
            Debug.Log("[Editor] TrimPool() manually called.");
        }

        GUI.color = originalColor;
    }
}
