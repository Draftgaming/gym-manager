using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class Trainer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrainerId { get; set; }

        public string Name { get; set; }
        public string Specialty { get; set; }

        [JsonIgnore]
        public ICollection<Trainee> Trainees { get; set; }
    }
}
