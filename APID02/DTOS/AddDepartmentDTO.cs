namespace APID02.DTOS
{
    public class AddDepartmentDTO
    {
       
        public string Dept_Name { get; set; }
        public string Dept_Desc { get; set; }
        public string Dept_Location { get; set; }
        public int Dept_Manager { get; set; }
        public DateTime Manager_hiredate { get; set; }
    }
}
