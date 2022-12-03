using System.Collections.Generic;
using TimeSheets.DAL.Interfaces;

namespace TimeSheets.Responses.Interfaces
{
    public interface IGetAllDataResponse
    {
        public IList<ITSModel> GetAllData();
    }
}
