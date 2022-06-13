namespace Recap.Core.Entities.Concrete
{
    public class UserOperationClaim : BaseModel, IEntity
    { 
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }
}
