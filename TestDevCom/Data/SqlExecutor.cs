using System.Data;
using Microsoft.Data.SqlClient;

namespace TestDevCom.Data
{
    public class SqlExecutor : ISqlExecutor
    {
        private readonly IDbConnection _connection;

        public SqlExecutor(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string procedure, Func<IDataReader, T> map, Dictionary<string, object?> parameters)
        {
            var result = new List<T>();
            using var cmd = CreateCommand(procedure, parameters);
            await OpenConnectionAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(map(reader));
            }
            return result;
        }

        public async Task ExecuteAsync(string procedure, Dictionary<string, object?> parameters)
        {
            using var cmd = CreateCommand(procedure, parameters);
            await OpenConnectionAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        private SqlCommand CreateCommand(string procedure, Dictionary<string, object?> parameters)
        {
            var cmd = (SqlCommand)_connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedure;

            foreach (var param in parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
            }

            return cmd;
        }

        private async Task OpenConnectionAsync()
        {
            if (_connection.State != ConnectionState.Open)
                await ((SqlConnection)_connection).OpenAsync();
        }
    }
}
