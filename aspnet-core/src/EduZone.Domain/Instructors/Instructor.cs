﻿using EduZone.Enum;
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
        public string CountryCode { get; set; }
        public string MobileNumber { get; set; }
        public string FullMobileNumber { get; set; }

        protected Instructor()
        {
            
        }

        public Instructor(Guid id,Guid tenantId,string firstName, string lastName, Gender gender, string email,
        string about, string countryCode, string mobileNumber)
        {
            Id = id;
            TenantId = tenantId;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Email = email;
            About = about;
            CountryCode = countryCode;
            MobileNumber = mobileNumber;
            FullMobileNumber = countryCode + mobileNumber;
        }

    }
}
