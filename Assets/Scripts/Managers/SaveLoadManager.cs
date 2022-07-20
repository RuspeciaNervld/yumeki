using System.IO;
using UnityEngine;

public class SaveLoadManager : ISingleton<SaveLoadManager> {

    public void SaveByPlayerPrefs(string key, object data) {
        var json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();

#if UNITY_EDITOR
        Debug.Log("PlayerPrefs�������ݳɹ�");
#endif

    }

    public string LoadFromPlayerPrefs(string key) {
        return PlayerPrefs.GetString(key,null/*Ĭ��ֵ*/);
    }

    public void SaveByJson(string saveFileName,object data) {
        var json = JsonUtility.ToJson(data);
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        try {
            File.WriteAllText(path, json);

#if UNITY_EDITOR
            Debug.Log($"�ɹ��洢��{path}");
#endif

        } catch(System.Exception e) {
#if UNITY_EDITOR
            Debug.Log($"�洢��{path}ʧ��\n{e}");
#endif
        }
    }

    public T LoadFromJson<T>(string saveFileName) {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);

        try {
            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(json);

            return data;
        }catch(System.Exception e) {
#if UNITY_EDITOR
            Debug.Log($"��{path}��ȡ����ʧ��{e}");
#endif
            return default;
        }
    }

    public void DeleteSaveFile(string saveFileName) {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        try {
            File.Delete(path);
        }catch(System.Exception e) {
#if UNITY_EDITOR
            Debug.Log($"ɾ��{path}ʧ��\n{e}");
#endif
        }
    }
}
