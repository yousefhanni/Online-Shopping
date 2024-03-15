using System.ComponentModel.DataAnnotations;

namespace Myshop.DAL.Models
{
    public class Category : BaseModel
    {
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]

        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "Description cannot exceed 100 characters")]
        public string Description { get; set; }

        public DateTime DateOfCreation { get; set; } = DateTime.Now;

    }

}
