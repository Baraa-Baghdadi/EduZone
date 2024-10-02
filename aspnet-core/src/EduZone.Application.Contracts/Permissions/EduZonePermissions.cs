namespace EduZone.Permissions;

public static class EduZonePermissions
{
    public const string GroupName = "EduZone";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public static class Dashboard
    {
        public const string DashboardGroup = GroupName + ".Dashboard";
        public const string Host = DashboardGroup + ".Host";
        public const string Tenant = DashboardGroup + ".Tenant";
    }

    public static class AllInstructors 
    {
        public const string Default = GroupName + ".AllInstructors";
        public const string View = GroupName + ".AllInstructors.View";
        public const string Edit = GroupName + ".AllInstructors.Edit";
    }

    public static class AllStudents
    {
        public const string Default = GroupName + ".AllStudents";
        public const string View = GroupName + ".AllStudents.View";
        public const string Edit = GroupName + ".AllStudents.Edit";
    }

    public static class AllCourses
    {
        public const string Default = GroupName + ".AllCourses";
        public const string View = GroupName + ".AllCourses.View";
        public const string Edit = GroupName + ".AllCourses.Edit";
    }

    public static class Courses
    {
        public const string Default = GroupName + ".Courses";
        public const string View = GroupName + ".Courses.View";
        public const string Edit = GroupName + ".Courses.Edit";
        public const string Create = GroupName + ".Courses.Create";
    }


}
