using Sql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Super.User.Identity
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataTableContext _tableContext;

        public UserRepository(IDataTableContext tableContext)
        {
            _tableContext = tableContext;
        }

        public Guid AddUser(UserModle user)
        {
            ValidateUser(user);

            var commandType = CommandType.StoredProcedure;
            try
            {
                var spInsertUser = "InsertUser";
                var parameters = ConvertUserToParameter(user);
                var id = (Guid)_tableContext.ExecuteScalar(spInsertUser, commandType, parameters);

                var spInsertUserAddresses = "InsertUserAddresses";
                foreach (var address in user.Addresses)
                {
                    var addresses = ConvertAddress(id, address);
                    _tableContext.ExecuteNonQuery(spInsertUserAddresses, commandType, addresses);
                }

                return id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteUser(Guid id)
        {
            var commandType = CommandType.StoredProcedure;
            var spDeleteUser = "DeleteUser";
            var parameter = CreateParameter("id", id);

            try
            {
                _tableContext.ExecuteNonQuery(spDeleteUser, commandType, parameter);
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public UserModle GetUser(Guid id)
        {
            var commandType = CommandType.StoredProcedure;
            var spGetUser = "SelectUser";
            var parameter = CreateParameter("id", id);

            try
            {
                var user = _tableContext.Reader(spGetUser, commandType, parameter);

                return ConvertUsers(user).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<UserModle> GetUsers()
        {
            var commandType = CommandType.StoredProcedure;
            var spGetUser = "SelectAllUsers";

            try
            {
                var user = _tableContext.Reader(spGetUser, commandType);

                return ConvertUsers(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateUser(UserModle user)
        {
            ValidateUser(user);

            var commandType = CommandType.StoredProcedure;
            try
            {
                var spUpdateUser = "UpdateUser";
                var parameters = ConvertUserToParameter(user);
                _tableContext.ExecuteNonQuery(spUpdateUser, commandType, parameters);

                var spUpdateUserAddresses = "UpdateUserAddresses";
                var id = (Guid)user.Id;
                foreach (var address in user.Addresses)
                {
                    var addresses = ConvertAddress(id, address);
                    _tableContext.ExecuteNonQuery(spUpdateUserAddresses, commandType, addresses);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        private List<UserModle> ConvertUsers(DataTable results)
        {

            var dictionary = results.Rows.Cast<DataRow>()
                .GroupBy(k => k["Id"].ToString())
                .ToDictionary(k => k.Key, v => v.ToList());


            var result = new List<UserModle>();
            foreach (var key in dictionary)
            {
                var user = ConvertUser(key.Value.First());
                user.Addresses = key.Value.Select(row => (string)row["Address"]).ToList();
                result.Add(user);
            }

            return result;
        }

        private UserModle ConvertUser(DataRow row)
        {
            return new UserModle
            {
                Id = (Guid)row["Id"],
                FirstName = (string)row["FirstName"],
                LastName = (string)row["LastName"],
                Age = (int)row["Age"]
            };
        }

        private SqlParameter[] ConvertUserToParameter(UserModle user)
        {
            var parameters = new List<SqlParameter>();

            if (user.Id != null)
                parameters.Add(CreateParameter("id", user.Id));

            parameters.Add(CreateParameter("firstname", user.FirstName));
            parameters.Add(CreateParameter("lastname", user.LastName));
            parameters.Add(CreateParameter("age", user.Age));

            return parameters.ToArray();
        }

        private SqlParameter[] ConvertAddress(Guid id, string address)
        {
            var parameters = new List<SqlParameter>
            {
                CreateParameter("id", id),
                CreateParameter("address", address)
            };

            return parameters.ToArray();
        }

        private SqlParameter CreateParameter(string name, object value)
        {
            return new SqlParameter(name, value);
        }

        private void ValidateUser(UserModle user)
        {
            if (string.IsNullOrEmpty(user.FirstName))
                throw new Exception("User must have first name");

            if (string.IsNullOrEmpty(user.LastName))
                throw new Exception("User must have last name");

            if (user.Age < 18 || user.Age > 120)
                throw new Exception("User age must be 18-120");

            if (!user.Addresses.Any())
                throw new Exception("User must have at least one address");
        }
    }
}
