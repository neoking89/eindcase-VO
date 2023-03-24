using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace eindcaseAPI.DAL
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        [MaxLength(300)]
        public string? Title { get; set; }
        public int Duration { get; set; }
        [MaxLength(10)]
        
        public string Code { get; set; }
    }

    public class CourseInstance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public int CourseId { get; set; }
        [JsonIgnore]
        public Course? Course { get; set; }
    }
}
