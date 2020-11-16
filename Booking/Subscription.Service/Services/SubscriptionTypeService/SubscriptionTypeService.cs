using Subscription.Core.Uow;
using System.Threading.Tasks;
using Subscription.Core.Domain;

namespace Subscription.Service.Services.SubscriptionTypeService
{
    public class SubscriptionTypeService : ISubscriptionTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionTypeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<SubscriptionType> GetSubscriptionTypeById(int id)
        {
            return _unitOfWork.SubscriptionTypes.GetById(id);
        }

        public async Task<UserSubscriptionType> AddUserSubscriptionType(UserSubscriptionType userSubscriptionType)
        {
            _unitOfWork.UserSubscriptionTypes.Add(userSubscriptionType);

            await _unitOfWork.CommitAsync();
            return userSubscriptionType;
        }
    }
}
