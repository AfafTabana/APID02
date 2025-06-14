using APID02.Models;

namespace APID02.Repository
{
    public class GenericRepo<TEntity> where TEntity : class
    {

        ITIContext iTIContext; 


        public GenericRepo(ITIContext db )
        {
            this.iTIContext = db;
        }

        public List<TEntity> getall()
        {
            return iTIContext.Set<TEntity>().ToList();
        }

        public TEntity getbyid(int id)
        {
            return iTIContext.Set<TEntity>().Find(id);
        }


        public void add(TEntity entity)
        {
            iTIContext.Set<TEntity>().Add(entity);
        }

        public void edit(int id , TEntity entity)
        {
            var existing = iTIContext.Set<TEntity>().Find(id);
            if (existing == null)
                throw new Exception("Entity not found");

            iTIContext.Entry(existing).CurrentValues.SetValues(entity);
        }

        public void delete(int id)
        {

          
            TEntity t = getbyid(id);
            iTIContext.Set<TEntity>().Remove(t);
        }

    }
}
