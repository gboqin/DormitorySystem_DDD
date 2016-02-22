using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Infrastructure
{
    public interface IUnitOfWork
    {
        bool Committed { get; }
        int Commit();
        void Rollback();
    }
}
