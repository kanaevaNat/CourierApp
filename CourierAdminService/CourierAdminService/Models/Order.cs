using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourierAdminService.Models
{
    [Table("orders")]
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("to_date")]
        public DateTimeOffset ToDate { get; set; }

        [Column("good")]
        public string? Good { get; set; }

        [Column("courier_id")]
        public int? CourierId { get; set; }

        public Courier? Courier { get; set; }

        public Client Client { get; set; }

        [Column("client_id")]
        public int ClientId { get; set;  }
    }
}
