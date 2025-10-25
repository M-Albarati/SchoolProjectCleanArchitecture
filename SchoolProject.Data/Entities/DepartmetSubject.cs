using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Entities
{
    public class DepartmetSubject
    {
        [Key]
        public int DID { get; set; }
        [Key]
        public int SubID { get; set; }

        [ForeignKey("DID")]
        [InverseProperty(nameof(Department.DepartmentSubjects))]
        public virtual Department? Department { get; set; }

        [ForeignKey("SubID")]
        [InverseProperty(nameof(Subject.DepartmetSubjects))]
        public virtual Subject? Subject { get; set; }
    }

}
