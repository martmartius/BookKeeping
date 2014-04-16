﻿using BookKeeping.Core;
using BookKeeping.Core.AtomicStorage;
using BookKeeping.Core.Storage;
using System.Linq;

namespace BookKeeping
{
    internal sealed class CommandHandlerFactoryImpl : ICommandHandlerFactory
    {
        private readonly IDocumentStore _documentStore;
        private readonly IEventBus _eventBus;
        private readonly IEventStore _eventStore;

        public CommandHandlerFactoryImpl(IDocumentStore documentStore, IEventStore eventStore, IEventBus eventBus)
        {
            _documentStore = documentStore;
            _eventBus = eventBus;
            _eventStore = eventStore;
        }

        public ICommandHandler<T> GetHandler<T>() where T : ICommand
        {
            return (ICommandHandler<T>)DomainBoundedContext.EntityApplicationServices(_documentStore, _eventStore, _eventBus)
                .SingleOrDefault(service => service is ICommandHandler<T>);
        }
    }
}