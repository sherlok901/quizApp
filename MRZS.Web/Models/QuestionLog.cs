using System.ComponentModel.DataAnnotations;

namespace MRZS.Web.Models
{
    public class QuestionLog
    {
        [Key]
        public int Id { get; set; }

        public string QuestionText { get; set; }
        public bool IsCorrect { get; set; }
    }
}