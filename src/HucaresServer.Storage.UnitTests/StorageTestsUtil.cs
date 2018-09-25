using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HucaresServer.Storage.UnitTests
{
    public class StorageTestsUtil
    {
        internal static DbSet<T> SetupFakeDbSet<T>(IQueryable<T> fakeIQueryable) where T : class
        {
            var fakeDbSet = A.Fake<DbSet<T>>(d => d.Implements(typeof(IQueryable<T>)));

            A.CallTo(() => ((IQueryable<T>)fakeDbSet).GetEnumerator())
                .Returns(fakeIQueryable.GetEnumerator());

            A.CallTo(() => ((IQueryable<T>)fakeDbSet).Provider)
                .Returns(fakeIQueryable.Provider);

            A.CallTo(() => ((IQueryable<T>)fakeDbSet).Expression)
                .Returns(fakeIQueryable.Expression);

            A.CallTo(() => ((IQueryable<T>)fakeDbSet).ElementType)
               .Returns(fakeIQueryable.ElementType);

            return fakeDbSet;
        }
    }
}
