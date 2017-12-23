using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Sql.Infrastructure;
using Super.User.Identity;
using System.Data;
using System.Data.SqlClient;

namespace SuperCom.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        private IDataTableContext _tableContext;
        private UserRepository _target;

        [TestInitialize]
        public void Initialized()
        {
            _tableContext = MockRepository.GenerateStub<IDataTableContext>();
            _target = new UserRepository(_tableContext);
        }

        [TestMethod]
        public void AddUser_AddSuccess()
        {
            var user = new UserModle
            {
                Age = 18,
                FirstName = "First name",
                LastName = "Last name",
                Addresses = new List<string> { "address" }
            };

            var expected = Guid.NewGuid();

            _tableContext.Stub(table =>
                table.ExecuteScalar(Arg<string>.Is.Anything,
                Arg<CommandType>.Is.Anything,
                Arg<SqlParameter>.Is.Anything)).Return(expected);

            var actual = _target.AddUser(user);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddUser_UserMissProperty()
        {
            var user = new UserModle
            {
                Age = 5,
                LastName = "Last name",
                Addresses = new List<string> { "address" }
            };

            var actual = _target.AddUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddUser_UserMissAge()
        {
            var user = new UserModle
            {
                FirstName = "FirstName",
                LastName = "Last name",
                Addresses = new List<string> { "address" }
            };
            
            var actual = _target.AddUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddUser_IDataTableContext_Throw()
        {
            var user = new UserModle
            {
                Age = 5,
                FirstName = "First name",
                LastName = "Last name",
                Addresses = new List<string> { "address" }
            };

            _tableContext.Stub(table =>
                table.ExecuteScalar(Arg<string>.Is.Anything,
                Arg<CommandType>.Is.Anything,
                Arg<SqlParameter>.Is.Anything)).Throw(new Exception());

            var actual = _target.AddUser(user);
        }
    }
}
