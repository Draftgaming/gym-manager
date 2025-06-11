using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class TrainingPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrainingPlanId { get; set; }

        public string PlanName { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Trainee> Trainees { get; set; }
    }
}
