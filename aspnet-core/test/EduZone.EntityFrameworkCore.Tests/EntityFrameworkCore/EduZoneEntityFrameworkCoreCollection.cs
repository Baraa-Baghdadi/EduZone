using Xunit;

namespace EduZone.EntityFrameworkCore;

[CollectionDefinition(EduZoneTestConsts.CollectionDefinitionName)]
public class EduZoneEntityFrameworkCoreCollection : ICollectionFixture<EduZoneEntityFrameworkCoreFixture>
{

}
