namespace EduZone;

public static class EduZoneDomainErrorCodes
{
    /* You can add your business exception error codes here, as constants */
    public const string NotFound = "NotFound";
    public const string AttachmentNotExist = "AttachmentNotExist";
    public const string AttachmentFailedDonload = "FailedDownload";
    public const string EmailalreadyExist = "EmailalreadyExist";
    public const string InvalidPassword = "InvalidPassword";
    public const string UserNotFound = "UserNotFound";
    public const string CourseShouldContainLesson = "CourseShouldContainLesson";
}
