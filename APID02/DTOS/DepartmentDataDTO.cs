using APID02.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APID02.DTOS
{
    public class DepartmentDataDTO : AddDepartmentDTO
    {
      
        public int Students_count { get; set; }

    }
}
