﻿using FutureFridges.Business.Enums;
using FutureFridges.Business.UserManagement;
using FutureFridges.Data.StockManagement;

namespace FutureFridges.Data.UserManagement
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserPermissionRepository __UserPermissionRepository;
        //DATABASE CONNECTION VARIABLES GO UP HERE

        public UserRepository() :
            this(new UserPermissionRepository())
        {}

        internal UserRepository(IUserPermissionRepository userPermissionRepository)
        {
            __UserPermissionRepository = userPermissionRepository;
        }

        public User GetUser(Guid User_UID)
        {
            //TODO: GET USER FROM DATABASE, CONVERT TO LOCAL USER CLASS AND RETURN, CURRENTLY RETURNING A SAMPLE USER OBJECT

            return new User()
            {
                User_UID = User_UID,
                Forename = "EXAMPLE",
                Surname = "EXAMPLE",
                Email = "example@example.com",
                UserType = UserType.Chef,
                Permissions = __UserPermissionRepository.GetUserPermissions(User_UID)
            };
        }
    }
}
