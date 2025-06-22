using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class ProgressRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProgressRecordId { get; set; }

        public DateTime RecordDate { get; set; }
        public string Notes { get; set; }
        public double Weight { get; set; }
        public double Bmi { get; set; }
        public int TraineeId { get; set; }

        [JsonIgnore]
        public Trainee Trainee { get; set; }

        public static implicit operator ProgressRecord(List<ProgressRecord> v)
        {
            throw new NotImplementedException();
        }
    }
}
