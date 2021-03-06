﻿namespace BookKeeping.Domain.Contracts
{
    public interface ICommand { }

    public interface ICommand<out TIdentity> : ICommand
        where TIdentity : IIdentity
    {
        TIdentity Id { get; }
    }
}