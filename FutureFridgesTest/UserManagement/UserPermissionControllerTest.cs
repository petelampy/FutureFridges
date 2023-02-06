using FutureFridges.Business.Enums;
using FutureFridges.Business.UserManagement;
using FutureFridges.Data.UserManagement;

namespace FutureFridgesTest.UserManagement
{
    [TestClass]
    public class UserPermissionControllerTest
    {
        [TestMethod]
        public void UserPermissionController_Create_GeneratesCorrectPermissionsForHeadChef ()
        {
            Mock<IUserPermissionRepository> _MockRepository = new Mock<IUserPermissionRepository>();

            UserPermissions _Result = new UserPermissions();

            _MockRepository.Setup(mock => mock.CreatePermissions(It.IsAny<UserPermissions>())).Callback((UserPermissions permissions) =>
            {
                _Result = permissions;
            }).Verifiable();

            IUserPermissionController _UserPermissionController = new UserPermissionController(_MockRepository.Object);

            Guid _MockUserUID = Guid.NewGuid();

            _UserPermissionController.CreatePermissions(_MockUserUID, UserType.HeadChef);

            Assert.AreEqual(_MockUserUID, _Result.User_UID);

            Assert.IsTrue(_Result.ViewStock);
            Assert.IsTrue(_Result.RemoveStock);
            Assert.IsTrue(_Result.AddStock);
            Assert.IsTrue(_Result.ManageProduct);
            Assert.IsTrue(_Result.ManageHealthAndSafetyReport);
            Assert.IsTrue(_Result.CreateOrder);
            Assert.IsTrue(_Result.ManageSuppliers);
            Assert.IsTrue(_Result.ManageOrders);
            Assert.IsTrue(_Result.ViewAuditLog);

            Assert.IsFalse(_Result.ManageUser);

            _MockRepository.Verify(mock => mock.CreatePermissions(It.IsAny<UserPermissions>()), Times.Once);
        }

        [TestMethod]
        public void UserPermissionController_Create_GeneratesCorrectPermissionsForChef ()
        {
            Mock<IUserPermissionRepository> _MockRepository = new Mock<IUserPermissionRepository>();

            UserPermissions _Result = new UserPermissions();

            _MockRepository.Setup(mock => mock.CreatePermissions(It.IsAny<UserPermissions>())).Callback((UserPermissions permissions) =>
            {
                _Result = permissions;
            }).Verifiable();

            IUserPermissionController _UserPermissionController = new UserPermissionController(_MockRepository.Object);

            Guid _MockUserUID = Guid.NewGuid();

            _UserPermissionController.CreatePermissions(_MockUserUID, UserType.Chef);

            Assert.AreEqual(_MockUserUID, _Result.User_UID);

            Assert.IsTrue(_Result.ViewStock);
            Assert.IsTrue(_Result.RemoveStock);
            Assert.IsTrue(_Result.AddStock);
            
            Assert.IsFalse(_Result.ManageProduct);
            Assert.IsFalse(_Result.ManageHealthAndSafetyReport);
            Assert.IsFalse(_Result.CreateOrder);
            Assert.IsFalse(_Result.ManageSuppliers);
            Assert.IsFalse(_Result.ManageOrders);
            Assert.IsFalse(_Result.ViewAuditLog);
            Assert.IsFalse(_Result.ManageUser);

            _MockRepository.Verify(mock => mock.CreatePermissions(It.IsAny<UserPermissions>()), Times.Once);
        }

        [TestMethod]
        public void UserPermissionController_Create_GeneratesCorrectPermissionsForAdministrator ()
        {
            Mock<IUserPermissionRepository> _MockRepository = new Mock<IUserPermissionRepository>();

            UserPermissions _Result = new UserPermissions();

            _MockRepository.Setup(mock => mock.CreatePermissions(It.IsAny<UserPermissions>())).Callback((UserPermissions permissions) =>
            {
                _Result = permissions;
            }).Verifiable();

            IUserPermissionController _UserPermissionController = new UserPermissionController(_MockRepository.Object);

            Guid _MockUserUID = Guid.NewGuid();

            _UserPermissionController.CreatePermissions(_MockUserUID, UserType.Administrator);

            Assert.AreEqual(_MockUserUID, _Result.User_UID);

            Assert.IsTrue(_Result.ViewStock);
            Assert.IsTrue(_Result.RemoveStock);
            Assert.IsTrue(_Result.AddStock);
            Assert.IsTrue(_Result.ManageProduct);
            Assert.IsTrue(_Result.ManageHealthAndSafetyReport);
            Assert.IsTrue(_Result.CreateOrder);
            Assert.IsTrue(_Result.ManageSuppliers);
            Assert.IsTrue(_Result.ManageOrders);
            Assert.IsTrue(_Result.ViewAuditLog);
            Assert.IsTrue(_Result.ManageUser);

            _MockRepository.Verify(mock => mock.CreatePermissions(It.IsAny<UserPermissions>()), Times.Once);
        }

    }
}
