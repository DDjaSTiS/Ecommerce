using FluentMigrator;
using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;

namespace ECommerce.OrderService.Migrations.Migrator
{
    public abstract class SqlMigration : IMigration
    {
        public object ApplicationContext => throw new NotImplementedException();

        public string ConnectionString => throw new NotImplementedException();        

        public void GetUpExpressions(IMigrationContext context)
        {
            context.Expressions.Add(new ExecuteSqlStatementExpression()
            {
                SqlStatement = GetUpSql(context.ServiceProvider)
            });
        }

        public void GetDownExpressions(IMigrationContext context)
        {
            context.Expressions.Add(new ExecuteSqlStatementExpression()
            {
                SqlStatement = GetDownSql(context.ServiceProvider)
            });
        }

        protected abstract string GetUpSql(IServiceProvider services);
        protected abstract string GetDownSql(IServiceProvider services);
    }
}
