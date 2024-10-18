﻿using EduZone.Enum;
using EduZone.Licenses;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EduZone.Instructors
{
    public class Instructor : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string About { get; set; }
        public Guid? LicenseId { get; set; }
        public License License { get; set; }

        protected Instructor()
        {
            
        }

        public Instructor(Guid id,Guid tenantId,string firstName, string lastName, Gender gender, string email,
        string about, Guid licenseId)
        {
            Id = id;
            TenantId = tenantId;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Email = email;
            About = about;
            LicenseId = licenseId;
        }

    }
}
