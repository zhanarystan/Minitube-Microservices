using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Minitube.Upload.Infrastructure.Data;
public class DapperContext
{
    private readonly string _connectionString;

    public DapperContext(string connectionString)
    {
        _connectionString = connectionString; 
    }

    public SqlConnection CreateConnection() => new SqlConnection(_connectionString);
}
