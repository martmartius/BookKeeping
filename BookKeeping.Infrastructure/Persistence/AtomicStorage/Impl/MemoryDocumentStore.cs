﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace BookKeeping.Persistance.AtomicStorage
{
    public sealed class MemoryDocumentStore : IDocumentStore
    {
        ConcurrentDictionary<string, ConcurrentDictionary<string, byte[]>> _store;
        readonly IDocumentStrategy _strategy;

        public MemoryDocumentStore(ConcurrentDictionary<string, ConcurrentDictionary<string, byte[]>> store, IDocumentStrategy strategy)
        {
            _store = store;
            _strategy = strategy;
        }

        public IDocumentWriter<TKey, TEntity> GetWriter<TKey, TEntity>()
        {
            var bucket = _strategy.GetEntityBucket<TEntity>();
            var store = _store.GetOrAdd(bucket, s => new ConcurrentDictionary<string, byte[]>());
            return new MemoryDocumentReaderWriter<TKey, TEntity>(_strategy, store);
        }

        public void WriteContents(string bucket, IEnumerable<DocumentRecord> records)
        {
            var pairs = records.Select(r => new KeyValuePair<string, byte[]>(r.Key, r.Read())).ToArray();
            _store[bucket] = new ConcurrentDictionary<string, byte[]>(pairs);
        }

        public void ResetAll()
        {
            _store.Clear();
        }

        public void Reset(string bucketNames)
        {
            ConcurrentDictionary<string, byte[]> deletedValue;
            _store.TryRemove(bucketNames, out deletedValue);
        }


        public IDocumentReader<TKey, TEntity> GetReader<TKey, TEntity>()
        {
            var bucket = _strategy.GetEntityBucket<TEntity>();
            var store = _store.GetOrAdd(bucket, s => new ConcurrentDictionary<string, byte[]>());
            return new MemoryDocumentReaderWriter<TKey, TEntity>(_strategy, store);
        }

        public IDocumentStrategy Strategy
        {
            get { return _strategy; }
        }

        public IEnumerable<DocumentRecord> EnumerateContents(string bucket)
        {
            var store = _store.GetOrAdd(bucket, s => new ConcurrentDictionary<string, byte[]>());
            return store.Select(p => new DocumentRecord(p.Key, () => p.Value)).ToArray();
        }


    }
}
