
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CoinData))]
public class CoinDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CoinData coinData = (CoinData)target;

        GUIStyle headerStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 14,
            normal = { textColor = new Color(1f, 0.8f, 0.1f) }
        };

        GUILayout.Space(10);
        GUILayout.Label("💰 Coin Data Overview", headerStyle);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Current Coins", coinData.coinCount.ToString(), EditorStyles.helpBox);

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("➕ Add 10 Coins"))
        {
            coinData.AddCoins(10);
            EditorUtility.SetDirty(coinData);
        }
        if (GUILayout.Button("🧹 Reset Coins"))
        {
            coinData.ResetCoins();
            EditorUtility.SetDirty(coinData);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.HelpBox("This object holds and tracks the coin count in runtime. Useful for debugging or reward systems.", MessageType.Info);
    }
}