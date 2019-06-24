using DataEntities.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataEntities.Models
{
    [Table("Department")]
    public class Department
    {
        [Key]
        [Required(ErrorMessage = "Department Id is required")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SwaggerExclude]
        public int DeptID { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        [StringLength(60, ErrorMessage = "Department name can't be longer than 60 characters")]
        public string DeptName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
