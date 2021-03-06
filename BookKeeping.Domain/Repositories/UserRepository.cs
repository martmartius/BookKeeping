﻿using BookKeeping.Domain.Aggregates;
using BookKeeping.Domain.Contracts;
using BookKeeping.Domain.Projections.UserIndex;
using BookKeeping.Domain;
using BookKeeping.Persistance;
using BookKeeping.Persistance.AtomicStorage;
using BookKeeping.Persistance.Storage;
using System.Collections.Generic;

namespace BookKeeping.Domain.Repositories
{
    public class UserRepository : IUserRepository, IRepository<User, UserId>
    {
        readonly IEventStore _eventStore;
        readonly IDocumentReader<unit, UserIndexLookup> _userIndexReader;
        readonly IUnitOfWork _unitOfWork;

        public UserRepository(IEventStore eventStore, IUnitOfWork unitOfWork, IDocumentReader<unit, UserIndexLookup> userIndexReader)
        {
            _eventStore = eventStore;
            _userIndexReader = userIndexReader;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> All()
        {
            var index = _userIndexReader.Get<UserIndexLookup>();
            if (index.HasValue)
            {
                foreach (var item in index.Value.Identities)
                {
                    yield return Get(item);
                }
            }
            yield break;
        }

        public User Get(UserId id)
        {
            User user = null;
            user = _unitOfWork.Get<User>(id);
            if (user == null)
            {
                var stream = _eventStore.LoadEventStream(id);
                user = new User(stream.Events);
                _unitOfWork.RegisterForTracking(user, id);
            }
            return user;
        }

        public User Load(UserId id)
        {
            User user = null;
            user = _unitOfWork.Get<User>(id);
            if (user == null)
            {
                var stream = _eventStore.LoadEventStream(id);
                if (stream.Version > 0)
                {
                    user = new User(stream.Events);
                    _unitOfWork.RegisterForTracking(user, id);
                    return user;
                }
            }
            return user;
        }

        public virtual User Load(string login, string password)
        {
            var user = _userIndexReader.Get<UserIndexLookup>()
                .Convert(t => t.Logins.ContainsKey(login) ? t.Logins[login] : new UserId(-1))
                .Convert(t => Load(t), default(User));
            if (user == null)
                return null;
            if (user.Password.Check(password))
            {
                _unitOfWork.RegisterForTracking(user, user.Id);
                return user;
            }
            else return null;
        }
    }
}
