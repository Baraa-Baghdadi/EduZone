using EduZone.Localization;
using Volo.Abp.AspNetCore.Components;

namespace EduZone.Blazor.WebApp.Tiered.Client;

public abstract class EduZoneComponentBase : AbpComponentBase
{
    protected EduZoneComponentBase()
    {
        LocalizationResource = typeof(EduZoneResource);
    }
}
