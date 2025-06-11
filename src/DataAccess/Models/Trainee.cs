using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class Trainee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TraineeId { get; set; }

        public string FullName { get; set; }
        public DateTime DateJoined { get; set; }
        public int TrainerId { get; set; }
        
        [JsonIgnore]
        public Trainer Trainer { get; set; }
        
        public int TrainingPlanId { get; set; }

        [JsonIgnore]
        public TrainingPlan TrainingPlan { get; set; }

        [JsonIgnore]
        public ICollection<ProgressRecord> ProgressRecords { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; } = default!;

        [Required, MaxLength(100)]
        public string Password { get; set; } = default!;    // plaintext for demo only!

        [Required, MaxLength(20)]
        public string Role { get; set; } = "User";
    }
}
