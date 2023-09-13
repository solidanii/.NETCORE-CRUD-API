using System.ComponentModel.DataAnnotations;

namespace crud_asp_api.Models
{
    public class branch
    {
        [Key]
        public int branchid { get; set; }

        [Required]
        public string branchName { get; set; }

        [Required]
        public string location { get; set; }
    }
}
