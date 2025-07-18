using System.Data;

namespace TestDevCom.Data
{
    public interface ISqlExecutor
    {
        Task<IEnumerable<T>> QueryAsync<T>(string procedure, Func<IDataReader, T> map, Dictionary<string, object?> parameters);
        Task ExecuteAsync(string procedure, Dictionary<string, object?> parameters);
    }
}
