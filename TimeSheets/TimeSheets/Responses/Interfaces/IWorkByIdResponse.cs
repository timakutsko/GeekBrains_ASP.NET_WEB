using TimeSheets.DAL.Interfaces;

namespace TimeSheets.Responses.Interfaces
{
    public interface IWorkByIdResponse
    {
        public ITSModel GetById(int id);
        
        public ITSModel UpdateById(int id);

        public void DeleteById(int id);
    }
}
