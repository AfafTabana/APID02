using APID02.Models;
using APID02.Repository;

namespace APID02.UnitOfWorks
{
    public class UnitOfWork
    {
        ITIContext db;
        GenericRepo<Department> deptReps;
        GenericRepo<Student> studReps;

        public UnitOfWork(ITIContext db)
        {
            this.db = db;
        }

        public GenericRepo<Student> StudReps
        {
            get
            {
                if (studReps == null)
                    studReps = new GenericRepo<Student>(db);
                return studReps;
            }
        }

        public GenericRepo<Department> DeptReps
        {
            get
            {
                if (deptReps == null)
                    deptReps = new GenericRepo<Department>(db);
                return deptReps;
            }
        }


        public void save()
        {
            db.SaveChanges();
        }

    }
}
