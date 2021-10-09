using System;
using System.Collections.Generic;
using System.Text;

namespace Dot.Data
{
    /// <summary>
    /// Unit of work
    /// </summary>
    public interface IUoW
    {
        void Save();
    }

    public class DotUoW : IUoW
    {
        private readonly DotContext _dataContext;

        public DotUoW(DotContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Persist all changes to database
        /// </summary>
        public void Save()
        {
            _dataContext.SaveChanges();
        }
    }
}
