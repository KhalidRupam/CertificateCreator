using Dapper;
using System.Data.SqlClient;
using System.Data;
using CertificateCreatorApi.Models;

namespace CertificateCreatorApi.Repositories.EmployeesCertificateRepository
{
    public class EmployeesCertificateRepository:IEmployeesCertificateRepository
    {
        private readonly string _connectionString;
        public EmployeesCertificateRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateEmployeesCertificate(EmployeesCertificate employeesCertificate)
        {
            try
            {
                using (IDbConnection _dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@EmployeeId", employeesCertificate.EmployeeId, DbType.Int32);
                    parameters.Add("@CertificateId", employeesCertificate.CertificateId, DbType.Int32);
                    parameters.Add("@PDFUrl", employeesCertificate.PDFUrl, DbType.String);
                    parameters.Add("@CreatedBy", employeesCertificate.CreatedBy, DbType.String);

                    var results = await _dbConnection.ExecuteAsync("[dbo].[SP_CreateEmployeesCertificates]", parameters, commandType: CommandType.StoredProcedure);
                    return results;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> UpdateEmployeesCertificate(EmployeesCertificate employeesCertificate)
        {
            try
            {
                using (IDbConnection _dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@EmployeeId", employeesCertificate.EmployeeId, DbType.Int32);
                    parameters.Add("@CertificateId", employeesCertificate.CertificateId, DbType.Int32);
                    parameters.Add("@PDFUrl", employeesCertificate.PDFUrl, DbType.String);

                    var results = await _dbConnection.ExecuteAsync("[dbo].[SP_UpdateEmployeesCertificates]", parameters, commandType: CommandType.StoredProcedure);
                    return results;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<EmployeesCertificatesWithDetails>> GetAllEmployeesCertificates()
        {
            try
            {
                using (IDbConnection _dbConnection = new SqlConnection(_connectionString))
                {
                    var results = await _dbConnection.QueryAsync<EmployeesCertificatesWithDetails>("[dbo].[SP_GetAllEmployeesCertificates]", commandType: CommandType.StoredProcedure);
                    return results.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<EmployeesCertificatesWithDetails>> GetAllEmployeesCertificatesByEmployeeId(string creatorId)
        {
            try
            {
                using (IDbConnection _dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@creatorId", creatorId, DbType.String);
                    var results = await _dbConnection.QueryAsync<EmployeesCertificatesWithDetails>("[dbo].[SP_GetAllEmployeesCertificatesByEmployeeId]", parameters, commandType: CommandType.StoredProcedure);
                    return results.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> DeleteEmployeesCertificates(int id)
        {
            try
            {
                using (IDbConnection _dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@id", id, DbType.Int32);
                    var results = await _dbConnection.ExecuteAsync("[dbo].[SP_DeleteEmployeesCertificates]", parameters, commandType: CommandType.StoredProcedure);
                    return results;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
