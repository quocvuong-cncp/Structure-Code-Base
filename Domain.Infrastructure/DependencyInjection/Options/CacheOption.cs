using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.DependencyInjection.Options;
public class CacheOption
{
    public string Type { get; set; }
    public SqlServerOptions SqlServerOptions { get; set; }
    public RedisOptions RedisOptions { get; set; }

}
public class SqlServerOptions
{
    public string ConnectionStringName { get; set; }
    public string TableName { get; set; }
}
public class RedisOptions
{
    public string ConnectionStringName { get; set; }

}
