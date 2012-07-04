using System;
using System.ComponentModel.DataAnnotations;

namespace MRZS.Web.Models
{
    public class QuizLogDTO
    {
        [Key]
        public Guid Id { get; set; }

        public int QuizId { get; set; }
        public int EntityId { get; set; }
        public string DurationString { get; set; }
        public string SectionName { get; set; }
        public string BookName { get; set; }
        public DateTime StartTime { get; set; }
        public string CorrectAnswersString { get; set; }
    }
}