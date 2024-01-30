using CertificateCreatorApi.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using static System.Net.WebRequestMethods;

namespace CertificateCreatorApi.Repositories.LoginRepository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly string _connectionString;
        public LoginRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<string> CreateUser(LoginEntity loginEntity)
        {
            try
            {
                using (IDbConnection _dbConnection = new SqlConnection(_connectionString))
                {
                    var id = Guid.NewGuid().ToString();

                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.String);
                    parameters.Add("@UserName", loginEntity.UserName, DbType.String);
                    parameters.Add("@Password", loginEntity.Password, DbType.String);
                    parameters.Add("@Email", loginEntity.Email, DbType.String);
                    parameters.Add("@UserTypeId", 0, DbType.Int32);
                    parameters.Add("@EmailVerified", false, DbType.Boolean);
                    parameters.Add("@CreationDate", DateTime.Now, DbType.DateTime);
                    parameters.Add("@ModificationDate", DateTime.Now, DbType.DateTime);

                    var results = await _dbConnection.ExecuteAsync("[dbo].[SP_CreateUser]", parameters, commandType: CommandType.StoredProcedure);

                    if (results < 1)
                    {
                        id = "";
                    }

                    return id;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> CreateOtp(UserOTP otp)
        {
            try
            {
                using (IDbConnection _dbConnection = new SqlConnection(_connectionString))
                {

                    DynamicParameters parameters = new();
                    parameters.Add("@OTP", otp.OTP, DbType.String);
                    parameters.Add("@ExpiryDate", otp.ExpiryDate, DbType.DateTime);
                    parameters.Add("@UserId", otp.UserId, DbType.String);

                    var results = await _dbConnection.ExecuteAsync("[dbo].[SP_CreateOTP]", parameters, commandType: CommandType.StoredProcedure);
                    return results;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<LoginDetailsWithOTP> GetLoginByEmailId(string EmailId)
        {
            try
            {
                using (IDbConnection _dbConnection = new SqlConnection(_connectionString))
                {

                    DynamicParameters parameters = new();
                    parameters.Add("@EmailId", EmailId, DbType.String);

                    var results = await _dbConnection.QueryMultipleAsync("[dbo].[SP_GetLoginAndOtpDetailsByEmailId]", parameters, commandType: CommandType.StoredProcedure);

                    LoginDetailsWithOTP loginDetailsWithOTP = new LoginDetailsWithOTP();

                    loginDetailsWithOTP.loginEntity = (await results.ReadAsync<LoginEntity>()).FirstOrDefault();
                    loginDetailsWithOTP.userOTP = (await results.ReadAsync<UserOTP>()).FirstOrDefault();

                    return loginDetailsWithOTP;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> UpdateUser(LoginEntity loginEntity)
        {
            try
            {
                using (IDbConnection _dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", loginEntity.Id, DbType.String);
                    parameters.Add("@EmailVerified", loginEntity.EmailVerified, DbType.Boolean);
                    parameters.Add("@ModificationDate", DateTime.Now, DbType.DateTime);

                    var results = await _dbConnection.ExecuteAsync("[dbo].[SP_UpdateUser]", parameters, commandType: CommandType.StoredProcedure);
                    return results;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
