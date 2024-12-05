using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CustomMenuItem : MonoBehaviour
{
    [MenuItem("SansDev/Open Game Scene", priority = 0)]
    static void LoadGameScene()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/_Game/Scenes/Game.unity");
        }
    }

    [MenuItem("SansDev/Customize/Admob Data")]
    static void OpenAdmobData()
    {
        string path = "Assets/Resources/ScriptableObject/Admob Data.asset";
        AdsDataSO data = (AdsDataSO)AssetDatabase.LoadAssetAtPath(path, typeof(AdsDataSO));
        Selection.activeObject = data;
    }

    [MenuItem("SansDev/Customize/Credit Panel")]
    static void OpenCreditData()
    {
        string path = "Assets/Resources/ScriptableObject/Credit Data.asset";
        CreditDataSO data = (CreditDataSO)AssetDatabase.LoadAssetAtPath(path, typeof(CreditDataSO));
        Selection.activeObject = data;
    }

    [MenuItem("SansDev/Customize/Privacy Policy Link")]
    static void OpenPolicyData()
    {
        string path = "Assets/Resources/ScriptableObject/Policy Data.asset";
        PolicyDataSO data = (PolicyDataSO)AssetDatabase.LoadAssetAtPath(path, typeof(PolicyDataSO));
        AssetDatabase.OpenAsset(data);
    }

    [MenuItem("SansDev/Customize/Game Mode")]
    static void OpenGameModeData()
    {
        string path = "Assets/Resources/ScriptableObject/Game Mode.asset";
        GameModeSO data = (GameModeSO)AssetDatabase.LoadAssetAtPath(path, typeof(GameModeSO));
        Selection.activeObject = data;
    }

    [MenuItem("SansDev/Customize/Tetromino Data")]
    static void OpenTetrominoData()
    {
        string path = "Assets/Resources/ScriptableObject/Tetromino Data.asset";
        TetrominoDataSO data = (TetrominoDataSO)AssetDatabase.LoadAssetAtPath(path, typeof(TetrominoDataSO));
        Selection.activeObject = data;
    }

    [MenuItem("SansDev/Customize/Audio Data")]
    static void OpenAudioData()
    {
        string path = "Assets/Resources/ScriptableObject/Audio Data.asset";
        AudioDataSO data = (AudioDataSO)AssetDatabase.LoadAssetAtPath(path, typeof(AudioDataSO));
        Selection.activeObject = data;
    }
}
