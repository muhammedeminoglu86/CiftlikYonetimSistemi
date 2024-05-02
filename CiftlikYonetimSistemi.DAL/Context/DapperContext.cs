using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("CiftlikYonetimSistemiConnection");
    }

    public IDbConnection CreateConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}