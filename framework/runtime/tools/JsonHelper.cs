using System.IO;
using System.Text;
using Godot;
using Godot.Collections;

namespace Framework;

/// <summary>
/// 可序列化为Json
/// </summary>
public interface IJsonSerializable
{

    void Serialize();

    void Deserialize();
}

public static class JsonHelper
{
    /// <summary>
    /// 读取Json文件
    /// </summary>
    public static string ReadJsonFile(string pathName)
    {
        if(!Directory.Exists("data")) Directory.CreateDirectory("data");
        using FileStream fs = new($"data/{pathName}.json", FileMode.OpenOrCreate, System.IO.FileAccess.Read);
        using BufferedStream bufferedStream = new(fs);
        // 创建一个字节数组来存储文件内容
        byte[] bytes = new byte[fs.Length];

        // 读取文件内容到字节数组
        int bytesRead = bufferedStream.Read(bytes, 0, bytes.Length);

        // 将字节数组转换为字符串，文件编码为 UTF-8
        return Encoding.UTF8.GetString(bytes, 0, bytesRead);
    }

    /// <summary>
    /// 写入Json文件
    /// </summary>
    public static void WriteJsonFile(string pathName, string content)
    {
        if(!Directory.Exists("data")) Directory.CreateDirectory("data");
        using FileStream fs = new($"data/{pathName}.json", FileMode.OpenOrCreate, System.IO.FileAccess.Write);
        using BufferedStream bufferedStream = new(fs);
        // 创建一个字节数组来存储文件内容
        byte[] bytes = Encoding.UTF8.GetBytes(content);
        // 异步读取文件内容到字节数组
        bufferedStream.Write(bytes, 0, bytes.Length);
    }

    public static Dictionary Deserialize(this string json)
    {
        Variant JsonObject = Json.ParseString(json);
        var dict = JsonObject.AsGodotDictionary();
        return dict;
    }

    public static string Serialize(this Dictionary json)
    {
        return Json.Stringify(json);
    }

    public static Dictionary SetValue(this Dictionary json, string key, Variant value)
    {
        json.Add(key, value);
        return json;
    }
}