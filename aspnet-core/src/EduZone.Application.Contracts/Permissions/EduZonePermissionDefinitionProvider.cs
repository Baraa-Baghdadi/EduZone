using EduZone.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace EduZone.Permissions;

public class EduZonePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {

        var EduZoneAppGroup = context.AddGroup(EduZonePermissions.GroupName, L("Permission:EduZone"));

        EduZoneAppGroup.AddPermission(EduZonePermissions.Dashboard.Host, L("Permission:Dashboard"), Volo.Abp.MultiTenancy.MultiTenancySides.Host);
        EduZoneAppGroup.AddPermission(EduZonePermissions.Dashboard.Tenant, L("Permission:Dashboard"), Volo.Abp.MultiTenancy.MultiTenancySides.Tenant);

        var allInstructorPermession = EduZoneAppGroup.AddPermission(EduZonePermissions.AllInstructors.Default, L("Permission:AllInstructors"),multiTenancySide:MultiTenancySides.Host);
        allInstructorPermession.AddChild(EduZonePermissions.AllInstructors.Edit, L("Permission:Edit"),multiTenancySide:MultiTenancySides.Host);
        allInstructorPermession.AddChild(EduZonePermissions.AllInstructors.View, L("Permission:View"),multiTenancySide:MultiTenancySides.Host);

        var allStudentsPermession = EduZoneAppGroup.AddPermission(EduZonePermissions.AllStudents.Default, L("Permission:AllStudents"), multiTenancySide: MultiTenancySides.Host);
        allStudentsPermession.AddChild(EduZonePermissions.AllStudents.Edit, L("Permission:Edit"), multiTenancySide: MultiTenancySides.Host);
        allStudentsPermession.AddChild(EduZonePermissions.AllStudents.View, L("Permission:View"), multiTenancySide: MultiTenancySides.Host);


        var InstructorCertificatePermession = EduZoneAppGroup.AddPermission(EduZonePermissions.InstructorCertificates.Default, L("Permission:Certificates"), multiTenancySide: MultiTenancySides.Tenant);
        InstructorCertificatePermession.AddChild(EduZonePermissions.InstructorCertificates.GetCertificate, L("Permission:GetCertificate"), multiTenancySide: MultiTenancySides.Tenant);
        InstructorCertificatePermession.AddChild(EduZonePermissions.InstructorCertificates.GenerateCertificate, L("Permission:GenerateCertificate"), multiTenancySide: MultiTenancySides.Tenant);

        var AdminCertificatePermession = EduZoneAppGroup.AddPermission(EduZonePermissions.AdminCertificates.Default, L("Permission:Certificates"), multiTenancySide: MultiTenancySides.Host);
        AdminCertificatePermession.AddChild(EduZonePermissions.AdminCertificates.GetCertificate, L("Permission:GetCertificate"), multiTenancySide: MultiTenancySides.Host);
        AdminCertificatePermession.AddChild(EduZonePermissions.AdminCertificates.GenerateCertificate, L("Permission:GenerateCertificate"), multiTenancySide: MultiTenancySides.Host);

        var allCoursesPermession = EduZoneAppGroup.AddPermission(EduZonePermissions.AllCourses.Default, L("Permission:AllCourses"), multiTenancySide: MultiTenancySides.Host);
        allCoursesPermession.AddChild(EduZonePermissions.AllCourses.Edit, L("Permission:Edit"), multiTenancySide: MultiTenancySides.Host);
        allCoursesPermession.AddChild(EduZonePermissions.AllCourses.View, L("Permission:View"), multiTenancySide: MultiTenancySides.Host);

        var coursesPermession = EduZoneAppGroup.AddPermission(EduZonePermissions.Courses.Default, L("Permission:AllCourses"), multiTenancySide: MultiTenancySides.Tenant);
        coursesPermession.AddChild(EduZonePermissions.Courses.Edit, L("Permission:Edit"), multiTenancySide: MultiTenancySides.Tenant);
        coursesPermession.AddChild(EduZonePermissions.Courses.View, L("Permission:View"), multiTenancySide: MultiTenancySides.Tenant);
        coursesPermession.AddChild(EduZonePermissions.Courses.Create, L("Permission:Create"), multiTenancySide: MultiTenancySides.Tenant);


    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EduZoneResource>(name);
    }
}
