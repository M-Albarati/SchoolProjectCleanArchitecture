using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Entities
{
    public class Ins_Subject
    {
        [Key]
        public int InsId { get; set; }
        [Key]
        public int SubID { get; set; }

        [ForeignKey("InsId")]
        [InverseProperty("Ins_Subjects")]
        public Instructor? instructor { get; set; }
        
        [ForeignKey("SubID")]
        [InverseProperty("Ins_Subjects")]
        public Subject? Subject { get; set; }

    }
}
