using Godot;

namespace Framework.Runtime;

public class GlobalManager : Singleton<GlobalManager>, IJsonSerializable
{
    public readonly PropertyManager PropsMgr;
    
    public string FileName => "globalConfig";

#region 全局游戏变量
    public float GlobalTimeScale = 1f;


#endregion

    public GlobalManager()
    {
        PropsMgr = new();
        Deserialize();
    }

    public void Deserialize()
    {
        string json = JsonHelper.ReadJsonFile(FileName);
        //var data = json.Deserialize();
    }

    public void Serialize()
    {
        
    }
}