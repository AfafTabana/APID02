namespace APID02.DTOS
{
    public class AddStudentData
    {

      
        public string St_Fname { get; set; }
        public string St_Lname { get; set; }
        public string St_Address { get; set; }
        public int St_Age { get; set; }
        public int? Dept_id { get; set; }
        public int? Super_id { get; set; }
    }
}
