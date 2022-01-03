using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WPFTrainningCSharp.Service
{
    public interface IRepository<T> : IDisposable where T : class
    {
        T Get(Expression<Func<T, bool>> predicate);
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        private ObjectContext context;
        private IObjectSet<T> objectSet;

        public Repository()
        {
        }

        public Repository(ObjectContext ctx)
        {
            context = ctx;
            objectSet = context.CreateObjectSet<T>();
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            T entity = objectSet.Where<T>(predicate).FirstOrDefault();

            if (entity == null)
                return null;

            return objectSet.Where<T>(predicate).FirstOrDefault();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }
    }
}
