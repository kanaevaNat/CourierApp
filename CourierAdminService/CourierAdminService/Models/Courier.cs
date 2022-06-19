using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourierAdminService.Models
{
    [Table("couriers")]
    public class Courier
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("surname")]
        public string? Surname { get; set; }
        [Column("middle_name")]
        public string? MiddleName { get; set; }

        [Column("status_id")]
        public int StatusId { get; set; }

        public Status Status { get; set; }
    }
}
