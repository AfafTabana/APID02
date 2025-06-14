using APID02.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APID02.DTOS
{
    public class StudentDataDTO
    {
        public int St_Id { get; set; }
        public string St_Fname { get; set; }
        public string St_Lname { get; set; }
        public string St_Address { get; set; }
        public int St_Age { get; set; }
        public string Dept_Name { get; set; }
        public string Super_Name { get; set; }




    }
}
