using EduZone.Samples;
using Xunit;

namespace EduZone.EntityFrameworkCore.Domains;

[Collection(EduZoneTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<EduZoneEntityFrameworkCoreTestModule>
{

}
