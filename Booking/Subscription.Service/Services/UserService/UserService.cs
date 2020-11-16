using System.Threading.Tasks;
using Subscription.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Subscription.Infrastructure.Exceptions;
using Subscription.Infrastructure.Exceptions.Model.Enum;
using Subscription.Core.Uow;
using Subscription.Core.Domain.Enums;
using Subscription.Service.Services.SubscriptionTypeService;
using System;
using System.Collections.Generic;

namespace Subscription.Service.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ISubscriptionTypeService _subscriptionTypeService;

        public UserService(UserManager<User> userManager, ISubscriptionTypeService subscriptionTypeService)
        {
            this._userManager = userManager;
            this._subscriptionTypeService = subscriptionTypeService;
        }

        public async Task<User> CreateUserAsync(User user, string password, ApplicationRolesEnum applicationRole = ApplicationRolesEnum.Common)
        {
            User existingUser = await this.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                throw new ApiException((int)ErrorEnum.UserAlreadyExist, nameof(ErrorEnum.UserAlreadyExist));
            }

            IdentityResult result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
                result = await _userManager.AddToRoleAsync(user, applicationRole.ToString());

            return user;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            User userResult = await _userManager.FindByEmailAsync(email);

            return userResult;
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<User> SubscribeUserAsync(string email, SubscriptionTypeEnum subscriptionType)
        {
            User user = await this.FindByEmailAsync(email);

            if (user is null)
            {
                throw new ApiException((int)ErrorEnum.UserAlreadyExist, nameof(ErrorEnum.UserAlreadyExist));
            }

            SubscriptionType subscriptionTypeResult = await _subscriptionTypeService.GetSubscriptionTypeById((int)subscriptionType);

            if (user.UserSubscriptions != null)
            {
                throw new ApiException((int)ErrorEnum.UserAlreadyExist, nameof(ErrorEnum.UserAlreadyExist));
            }
            var result = await _subscriptionTypeService.AddUserSubscriptionType(new UserSubscriptionType
            {
                SubscriptionTypeId = subscriptionTypeResult.Id,
                UserId = user.Id,
                CreateDateTime = DateTime.Now,
                IsActive = true
            });

            IdentityResult results = await _userManager.UpdateAsync(user);
            return user;
        }
    }
}
