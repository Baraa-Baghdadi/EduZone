using EduZone.Samples;
using Xunit;

namespace EduZone.EntityFrameworkCore.Applications;

[Collection(EduZoneTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<EduZoneEntityFrameworkCoreTestModule>
{

}
