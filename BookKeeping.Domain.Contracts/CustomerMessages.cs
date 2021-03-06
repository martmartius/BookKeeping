﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookKeeping.Domain.Contracts
{
    // This file contains contracts for the sample domain. You can percieve these
    // classes and value objects as the foundation of the language, that is used
    // to express core business concepts in the code. In Domain-driven design
    // such classes become part of the Ubiquituous Language of this bounded context
    // (and may be some others).

    // These classes might be simple, but they are extremely important.
    // In more serious projects they are generally put into *.Contracts.dll,
    // which might be shared with the other bounded contexts.

    // Usually contracts include:
    // identities (strongly-typed references to other aggregates)
    // value objects
    // event and command contracts

    // classes below are events and commands for the current bounded context.
    // Normally they are auto-generated by a DSL (see Lokad.CQRS for a sample),
    // however in this case we just wrote them manually.

    // each command and event implements ToString method, which generates a human-
    // readable represenation of this object. This is EXTREMELY helpful for
    // logging, visualizing and testing.

    // such classes should be serializable properly by the current serializer that
    // you are using. Normally we use something like ProtoBuf (uses [DataContract] attributes)
    // but in this case we are staying simple and use BinarySerializer, which requires
    // use of [Serializable] attribute

    [Serializable]
    public class CustomerCreated : IEvent
    {
        public string Name { get; set; }

        public DateTime Created { get; set; }

        public CustomerId Id { get; set; }

        public Currency Currency { get; set; }

        public override string ToString()
        {
            return string.Format("Customer {0} created with {1}", Name, Currency);
        }
    }

    [Serializable]
    public sealed class CreateCustomer : ICommand
    {
        public CustomerId Id { get; set; }

        public string Name { get; set; }

        public Currency Currency { get; set; }

        public override string ToString()
        {
            return string.Format("Create {0} named '{1}' with {2}", Id, Name, Currency);
        }
    }

    [Serializable]
    public sealed class AddCustomerPayment : ICommand
    {
        public CustomerId Id { get; set; }

        public string Name { get; set; }

        public CurrencyAmount Amount { get; set; }

        public override string ToString()
        {
            return string.Format("Add {0} - '{1}'", Amount, Name);
        }
    }

    [Serializable]
    public sealed class ChargeCustomer : ICommand
    {
        public CustomerId Id { get; set; }

        public string Name { get; set; }

        public CurrencyAmount Amount { get; set; }

        public override string ToString()
        {
            return string.Format("Charge {0} - '{1}'", Amount, Name);
        }
    }

    [Serializable]
    public sealed class CustomerPaymentAdded : IEvent
    {
        public CustomerId Id { get; set; }

        public string PaymentName { get; set; }

        public CurrencyAmount Payment { get; set; }

        public CurrencyAmount NewBalance { get; set; }

        public int Transaction { get; set; }

        public DateTime TimeUtc { get; set; }

        public override string ToString()
        {
            return string.Format("Added '{2}' {1} | Tx {0} => {3}",
                Transaction, Payment, PaymentName, NewBalance);
        }
    }

    [Serializable]
    public sealed class CustomerChargeAdded : IEvent
    {
        public CustomerId Id { get; set; }

        public string ChargeName { get; set; }

        public CurrencyAmount Charge { get; set; }

        public CurrencyAmount NewBalance { get; set; }

        public int Transaction { get; set; }

        public DateTime TimeUtc { get; set; }

        public override string ToString()
        {
            return string.Format("Charged '{2}' {1} | Tx {0} => {3}",
                Transaction, Charge, ChargeName, NewBalance);
        }
    }

    [Serializable]
    public class RenameCustomer : ICommand
    {
        public CustomerId Id { get; set; }

        public string NewName { get; set; }

        public override string ToString()
        {
            return string.Format("Rename {0} to '{1}'", Id, NewName);
        }
    }

    [Serializable]
    public class LockCustomerForAccountOverdraft : ICommand
    {
        public CustomerId Id { get; set; }

        public string Comment { get; set; }
    }

    [Serializable]
    public class LockCustomer : ICommand
    {
        public CustomerId Id { get; set; }

        public string Reason { get; set; }
    }

    [Serializable]
    public class CustomerLocked : IEvent
    {
        public CustomerId Id { get; set; }

        public string Reason { get; set; }

        public override string ToString()
        {
            return string.Format("Customer locked: {0}", Reason);
        }
    }

    [Serializable]
    public class CustomerRenamed : IEvent
    {
        public string Name { get; set; }

        // normally you don't need old name. But here,
        // we include it just for demo
        public string OldName { get; set; }

        public CustomerId Id { get; set; }

        public DateTime Renamed { get; set; }

        public override string ToString()
        {
            return string.Format("Customer renamed from '{0}' to '{1}'", OldName, Name);
        }
    }
}
