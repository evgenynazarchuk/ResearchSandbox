using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncEnumerable
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World!");
        }
    }

    class ReaderAsync<TObject> : IAsyncEnumerable<TObject>, IAsyncEnumerator<TObject>
    {
        public TObject Current => default;

        public ReaderAsync()
        {
            
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerator<TObject> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> MoveNextAsync()
        {
            throw new NotImplementedException();
        }
    }
}
