using System.Data.SqlClient;
using System.Data;
using CertificateCreatorApi.Models;
using Dapper;

namespace CertificateCreatorApi.Repositories.CertificateTypeRepository
{
    public class CertificateTypeRepository:ICertificateTypeRepository
    {
        private readonly string _connectionString;
        public CertificateTypeRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<List<CertificateType>> GetAllCertificateTypes()
        {
            try
            {
                using (IDbConnection _dbConnection = new SqlConnection(_connectionString))
                {
                    var results = await _dbConnection.QueryAsync<CertificateType>("[dbo].[SP_GetAllCertificateTypes]", commandType: CommandType.StoredProcedure);

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
