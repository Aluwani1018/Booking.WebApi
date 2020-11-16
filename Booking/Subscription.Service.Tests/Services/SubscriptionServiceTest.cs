using Moq;
using Subscription.Core.Domain;
using Subscription.Core.Uow;
using Subscription.Service.Services.SubscriptionTypeService;
using System.Threading.Tasks;
using Xunit;

namespace Subscription.Service.Tests.Services
{
    public class SubscriptionServiceTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private SubscriptionTypeService _subscriptionTypeService;

        public SubscriptionServiceTest()
        {
            SetupMocks();
            _subscriptionTypeService = new SubscriptionTypeService(_unitOfWorkMock.Object);
        }

        private void SetupMocks()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _unitOfWorkMock.Setup(x => x.SubscriptionTypes.GetById(1)).Returns(new SubscriptionType() { Id = 1, Name = "test" }).Verifiable();

            _unitOfWorkMock.Setup(x => x.UserSubscriptionTypes.Add(new UserSubscriptionType() { UserId = 1, SubscriptionTypeId = 1, IsActive = true})).Verifiable();
        }

        [Fact]
        public async Task Should_Find_Existing_Subscription_Id()
        {
            var response = await _subscriptionTypeService.GetSubscriptionTypeById(1);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Return_Null_When_Trying_To_Find_Subscription_By_Id()
        {
            var response = await _subscriptionTypeService.GetSubscriptionTypeById(2);

            Assert.Null(response);
        }

        [Fact]
        public async Task Should_Create_Subscription_For_Existing_User()
        {
            UserSubscriptionType request = new UserSubscriptionType
            {
                UserId = 1,
                SubscriptionTypeId = 1,
                IsActive = true
            };

            var response = await _subscriptionTypeService.AddUserSubscriptionType(request);

            Assert.NotNull(response);
            Assert.Equal(response.UserId, request.UserId);
            Assert.Equal(response.SubscriptionTypeId, request.SubscriptionTypeId);
            Assert.Equal(response.IsActive, request.IsActive);
        }
    }
}
