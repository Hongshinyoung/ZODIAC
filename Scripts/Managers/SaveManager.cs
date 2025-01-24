using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private string saveFilePath; // 저장 파일 경로

    protected override void Awake()
    {
        base.Awake();

        // 저장 파일 경로 설정
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");

        // 저장 경로 디렉토리 확인 및 생성
        if (!Directory.Exists(Application.persistentDataPath))
        {
            Directory.CreateDirectory(Application.persistentDataPath);
        }
    }

    public void SaveGame(SaveData data)
    {
        // 데이터가 null인지 확인
        if (data == null)
        {
            return;
        }

        // 경로가 유효한지 확인
        if (string.IsNullOrEmpty(saveFilePath))
        {
            return;
        }

        // SaveData를 JSON 형식으로 변환
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        // 파일에 저장
        if (!string.IsNullOrEmpty(json))
        {
            File.WriteAllText(saveFilePath, json);
        }
        else { return; }
    }

    public SaveData LoadGame()
    {
        // 저장 파일이 존재하지 않으면 null 반환
        if (!File.Exists(saveFilePath))
        {
            return null;
        }

        // 파일에서 JSON 읽기
        string json = File.ReadAllText(saveFilePath);

        if (string.IsNullOrEmpty(json))
        {
            return null;
        }

        // JSON을 SaveData 객체로 변환
        SaveData data = JsonConvert.DeserializeObject<SaveData>(json);
        return data;
    }

    public void DeleteSave()
    {
        // 저장 파일이 존재하면 삭제
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
        else
        {
            return;
        }
    }
}
