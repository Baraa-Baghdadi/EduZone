using System;
using System.Collections.Generic;
using System.Text;
using EduZone.Localization;
using Volo.Abp.Application.Services;

namespace EduZone;

/* Inherit your application services from this class.
 */
public abstract class EduZoneAppService : ApplicationService
{
    protected EduZoneAppService()
    {
        LocalizationResource = typeof(EduZoneResource);
    }
}
