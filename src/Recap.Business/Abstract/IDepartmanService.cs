using Recap.Entities.Concrete;
using Recap.Entities.Results;

namespace Recap.Business.Abstract
{
    public interface IDepartmanService
    {
        IDataResult<Departman> Add(Departman param);
        IResult Delete(int param);
        IResult Update(Departman param);


        IDataResult<IList<Departman>> GetAll(int skip = 0, int take = 0, bool IsActive = false, int Id = 0, string Name = "Q", string RootPath = "Q");
       IDataResult<Departman> GetById(int Id);
       
          
    }
}
