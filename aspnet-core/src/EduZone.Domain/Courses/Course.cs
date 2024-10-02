using EduZone.Categories;
using EduZone.Instructors;
using EduZone.Lessons;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;



namespace EduZone.Courses
{
    public class Course : FullAuditedAggregateRoot<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double NewPrice { get; set; }
        public string Icon { get; set; } // as thumbnail
        public Guid ImageId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid InstructorId { get; set; }
        public Category Category { get; set; }
        public Instructor Instructor { get; set; }
        public List<Lesson> Lessons { get; set; }


        public Course(Guid id,string title, string description, double price, double newPrice,
            string icon, Guid imageId, Guid categoryId, Guid instructorId, List<Lesson> lessons)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            NewPrice = newPrice;
            Icon = icon;
            ImageId = imageId;
            CategoryId = categoryId;
            InstructorId = instructorId;
            Lessons = lessons;
        }

        public Course()
        {
            
        }
    }
}
