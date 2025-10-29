using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace BlogDataLibrary.Database;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config; // holds connection strings

    public SqlDataAccess(IConfiguration config) => _config = config;

    public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters,
                                                     string connectionStringName = "BlogDB")
    {
        string cs = _config.GetConnectionString(connectionStringName)!;
        using IDbConnection conn = new SqlConnection(cs);
        return await conn.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task SaveData<T>(string storedProcedure, T parameters,
                                  string connectionStringName = "BlogDB")
    {
        string cs = _config.GetConnectionString(connectionStringName)!;
        using IDbConnection conn = new SqlConnection(cs);
        await conn.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }
}

// LE2: Quick Actions → Extract Interface (accept defaults) → ISqlDataAccess
public interface ISqlDataAccess
{
    Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName = "BlogDB");
    Task SaveData<T>(string storedProcedure, T parameters, string connectionStringName = "BlogDB");
}
