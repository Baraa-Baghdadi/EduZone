using EduZone.Enum;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace EduZone.Students
{
    public class Student : FullAuditedAggregateRoot<Guid>
    {
        public string CustomStudenttId { get; set; }
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public Gender? Gender { get; set; } = null;
        public string Email { get; set; }
        public DateTime? DOB { get; set; } = null;
        public ApplicationLanguage? CurrentLanguage { get; set; } = ApplicationLanguage.en;

        protected Student()
        {
            
        }

        public Student(Guid id,string customStudenttId, string? firstName, string? lastName, Gender? gender, 
            string email, DateTime? dOB, ApplicationLanguage? currentLanguage)
        {
            Id = id;
            CustomStudenttId = customStudenttId;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Email = email;
            DOB = dOB;
            CurrentLanguage = currentLanguage;
        }
    }
}
