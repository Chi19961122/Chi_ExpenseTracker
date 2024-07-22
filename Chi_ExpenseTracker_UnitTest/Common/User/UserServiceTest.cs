using Chi_ExpenseTracker_Repesitory.Database.Repository;
using Chi_ExpenseTracker_Repesitory.Models;
using Chi_ExpenseTracker_Service.Common.User;
using Chi_ExpenseTracker_Service.Common.UserService;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Chi_ExpenseTracker_UnitTest.Common.User
{
    public class UserServiceTest
    {
        private IUserService _userService;
        private IUserRepository _userRepository;

        public UserServiceTest()
        {
            IServiceProvider serviceProvider = Substitute.For<IServiceProvider>();

            _userRepository = Substitute.For<IUserRepository>();

            serviceProvider.GetService<IUserRepository>().Returns(_userRepository);

            //創建實體UserService
            _userService = new UserService(serviceProvider);
        }

        /// <summary>
        /// 方法_情境_期望
        /// </summary>
        [Fact]
        public void GetUserByAccount_SendAccount_ReturnUser()
        {
            #region Arrange

            _userRepository.Find(Arg.Any<Expression<Func<UserEntity, bool>>>())
                .Returns(new UserEntity
                {
                    Email = "Test@example.com",
                });

            #endregion

            #region Action

            var result = _userService.GetUserByAccount("Test@example.com");

            #endregion

            #region Assert

            Assert.Equal(result.Email, "Test@example.com");

            #endregion
        }
    }
}
