namespace Framework;

public partial class PlayerNode : UnitNode
{
    public PlayerNode() : base()
    {
        PropertyManager.SetProperty<float>(UnitPropertyName.HP, 100f);
        PropertyManager.SetProperty<float>(UnitPropertyName.MaxHP, 100f);
        PropertyManager.SetProperty<float>(UnitPropertyName.Speed, 5f);
    }
}