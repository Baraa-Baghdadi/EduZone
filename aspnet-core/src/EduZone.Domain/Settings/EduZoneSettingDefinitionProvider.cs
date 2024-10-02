using Volo.Abp.Settings;

namespace EduZone.Settings;

public class EduZoneSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(EduZoneSettings.MySetting1));
    }
}
