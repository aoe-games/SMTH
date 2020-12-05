using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

[CustomEditor(typeof(QuestDatabase))]
public class QuestDatabaseEditor : Editor
{
  public override void OnInspectorGUI()
  {
    QuestDatabase myScript = target as QuestDatabase;
    if (GUILayout.Button("Build Database"))
    {
      List<QuestData> data = BuildDatabase();
      ((QuestDatabase)target).UpdateDatabase(data);
      EditorUtility.SetDirty(target);
      AssetDatabase.SaveAssets();
    }

    if (GUILayout.Button("Output Database"))
    {       
      Debug.Log(((QuestDatabase)target).ToString());
    }

    DrawDefaultInspector();
  }

  protected List<QuestData> BuildDatabase()
  {
    List<QuestData> questDatabaseContent = new List<QuestData>();
    string path = string.Empty;

    path = AssetDatabase.GetAssetPath(target);
    int lastIndex = path.LastIndexOf('/');
    path = path.Substring(0, lastIndex);

    IEnumerable<string> questDataPaths = Directory.GetFiles(path, "*.qd.asset", SearchOption.AllDirectories);

    string pathLog = string.Empty;
    foreach (string questDataPath in questDataPaths)
    {       
      QuestData questData = AssetDatabase.LoadAssetAtPath<QuestData>(questDataPath);
      questDatabaseContent.Add(questData);
      pathLog += questDataPath + "\n";
    }

    Debug.Log(pathLog);
    return questDatabaseContent;
  }

}
