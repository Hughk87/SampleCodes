using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// CSV Reader
/// * Resources/CSV/[path] csv파일 저장
/// * Load(path) Method csv파일 로드 -> List<String[]> 반환
/// * OnLoadTextAsset(data) 로드된 csv TextAsset데이터 파싱
/// </summary>

public class CSVReader
{
    /// <summary>
    /// Parse CSV to TextAsset
    /// </summary>
    /// <param name="path"> CSV file path </param>
    public static List<string[]> Load(string _filename)
    {
        string file_path = "Table/";
        file_path = string.Concat(file_path, _filename);
        TextAsset ta = Resources.Load(file_path, typeof(TextAsset)) as TextAsset;

        var _retval = OnLoadTextAsset(ta.text);

        Resources.UnloadAsset(ta);
        ta = null;
        return _retval;
    }

    /// <summary>
    ///  CSV Data Load
    /// </summary>
    /// <param name="data"></param>
    static List<string[]> OnLoadTextAsset(string data)
    {
        List<string[]> _list_load = new List<string[]>();

        string[] _arr_lines = data.Split('\n');
        for (int i = 1; i < _arr_lines.Length; i++)
        {
            try
            {
                string _str_line = _arr_lines[i];
                _str_line = Regex.Replace(_str_line, "\r", string.Empty);

                _list_load.Add(_str_line.Split(','));
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        return _list_load;
    }
}

