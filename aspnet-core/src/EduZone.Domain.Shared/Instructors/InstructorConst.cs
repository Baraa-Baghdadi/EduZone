using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduZone.Instructors
{
    public class InstructorConst
    {
        private const string DefaultSorting = "{0} FirstName asc";
        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Instructor." : string.Empty);
        }
    }
}
