using CertificateCreatorApi.Models;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace CertificateCreatorApi.Repositories.EmployeeRepository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly string _connectionString;
        public EmployeeRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<List<EmployeeDTO>> GetAllEmployeeDetails()
        {
            try
            {
                using (IDbConnection _dbConnection = new SqlConnection(_connectionString))
                {
                    var results = await _dbConnection.QueryAsync<EmployeeDTO>("[dbo].[SP_GetAllEmployeeDetails]", commandType: CommandType.StoredProcedure);

                    return results.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
