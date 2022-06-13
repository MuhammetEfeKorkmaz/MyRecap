using Recap.Entities.Concrete;
using Recap.Entities.Results;

namespace Recap.Business.Abstract
{
    public interface IUnvanService
    { 

        IDataResult<Unvan> Add(Unvan param);
        IResult Delete(int param);
        IResult Update(Unvan param);


        IDataResult<IList<Unvan>> GetAll(int skip = 0, int take = 0, bool IsActive = false, int Id = 0, string Name = "Q");
        IDataResult<Unvan> GetById(int Id);
      
    }
}
