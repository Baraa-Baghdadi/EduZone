using EduZone.Localization;
using Volo.Abp.AspNetCore.Components;

namespace EduZone.Blazor.WebApp;

public abstract class EduZoneComponentBase : AbpComponentBase
{
    protected EduZoneComponentBase()
    {
        LocalizationResource = typeof(EduZoneResource);
    }
}
