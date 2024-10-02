using EduZone.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EduZone.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class EduZoneController : AbpControllerBase
{
    protected EduZoneController()
    {
        LocalizationResource = typeof(EduZoneResource);
    }
}
