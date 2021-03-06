﻿using BookKeeping.Domain.Contracts;
using BookKeeping.Persistance.AtomicStorage;
using BookKeeping.Persistance.Storage;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Linq;

namespace BookKeeping.Domain
{
    public class Session : ISession, IUnitOfWork, IDomainIdentityGenerator
    {
        readonly IEventStore _eventStore;
        readonly IEventBus _eventBus;
        readonly ICommandBus _commandBus;
        readonly IDocumentStore _projections;
        readonly IUnitOfWork _innerUnitOfWork;
        readonly IDomainIdentityGenerator _identityGenerator;

        public Session(IEventStore eventStore, IEventBus eventBus, IDocumentStore projections)
        {
            _eventBus = eventBus;
            _eventStore = eventStore;
            _projections = projections;
            _innerUnitOfWork = new UnitOfWork(_eventStore, _eventBus);
            _commandBus = new CommandBus(new CommandHandlerFactoryEvil(_projections, _innerUnitOfWork, _eventStore, _eventBus));
            _identityGenerator = new DomainIdentityGenerator(_projections);
        }

        public void Command<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            _commandBus.Send(command);
        }

        public IQueryFor<TResult> Query<TResult>()
        {
            return new QueryFor<TResult>(new ProjectionsQueryFactory(_projections));
        }

        public void Commit()
        {
            _innerUnitOfWork.Commit();
        }

        public void Rollback()
        {
            _innerUnitOfWork.Rollback();
        }

        public void RegisterForTracking<TAggregate>(TAggregate aggregateRoot, IIdentity id)
            where TAggregate : AggregateBase
        {
            _innerUnitOfWork.RegisterForTracking(aggregateRoot, id);
        }

        public TAggregate Get<TAggregate>(IIdentity id) where TAggregate : AggregateBase
        {
            return _innerUnitOfWork.Get<TAggregate>(id);
        }

        public long GetId()
        {
            return _identityGenerator.GetId();
        }

        public void Dispose()
        {
            _innerUnitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    internal sealed class ProjectionsQueryFactory : IQueryFactory
    {
        readonly IDocumentStore _projections;

        public ProjectionsQueryFactory(IDocumentStore projections)
        {
            _projections = projections;
        }

        public IQuery<TCriterion, TResult> Create<TCriterion, TResult>() where TCriterion : ICriterion
        {
            return new ProjectionQuery<TCriterion, TResult>(_projections);
        }
    }

    internal sealed class ProjectionQuery<TCriterion, TResult> : IQuery<TCriterion, TResult>
        where TCriterion : ICriterion
    {
        readonly IDocumentStore _projections;

        public ProjectionQuery(IDocumentStore projections)
        {
            _projections = projections;
        }

        public Maybe<TResult> Ask(TCriterion criterion)
        {
            return _projections.GetReader<TCriterion, TResult>().Load(criterion);
        }
    }
}
