using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduZone.Enrollments
{
    public class EnrollmentConst
    {
        private const string DefaultSorting = "{0} CreationTime desc";
        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Enrollment." : string.Empty);
        }
    }
}
