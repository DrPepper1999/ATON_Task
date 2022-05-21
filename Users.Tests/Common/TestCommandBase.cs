using System;
using Users.Persistence;

namespace Users.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly UsersDbContext Context;

        public TestCommandBase()
        {
            Context = UsersContextFactory.Create();
        }

        public void Dispose()
        {
            UsersContextFactory.Destroy(Context);
        }
    }
}
