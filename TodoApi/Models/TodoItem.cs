using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    [Table("TODO_ITEMS")]
    public class TodoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public long Id { get; set; }

        [Column("NAME")]
        public string? Name { get; set; }

        [Column("IS_COMPLETE")]
        public bool IsComplete { get; set; }

        [Column("SECRET")]
        public string? Secret { get; set; }
    }
}
