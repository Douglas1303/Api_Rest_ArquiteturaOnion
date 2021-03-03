using Moq;
using Poc.Api.Controllers;
using Poc.Application.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poc.Test.Api.Controllers
{
    public class UserControllerTest
    {
        private readonly Mock<IUserApplication> _mockedUserApplication;
        private readonly UserController _userController;

        public UserControllerTest()
        {
            _mockedUserApplication = new Mock<IUserApplication>();
            _userController = new UserController(_mockedUserApplication.Object); 
        }
    }
}
