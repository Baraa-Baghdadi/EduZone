using Microsoft.Extensions.Localization;
using EduZone.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace EduZone.Blazor.WebApp;

[Dependency(ReplaceServices = true)]
public class EduZoneBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<EduZoneResource> _localizer;

    public EduZoneBrandingProvider(IStringLocalizer<EduZoneResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
