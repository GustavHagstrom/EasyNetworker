﻿using EasyNetworker.Shared.Abstractions;
using EasyNetworker.Shared.DependencyInjection;
using EasyNetworker.Shared.Exceptions;
using EasyNetworker.Shared.Utilities;
using System.Diagnostics;

namespace EasyNetworker.Shared.Services;
public class HandlerInvokerService : IHandlerInvokerService
{
    private readonly ServiceFactory serviceFactory;
    private const string HandleMethodName = "Handle";

    public HandlerInvokerService(ServiceFactory serviceFactory)
    {
        this.serviceFactory = serviceFactory;
    }
    public void Invoke(object payload, int id)
    {
        var description = Mappings.Instance.GetPacketsDescription(id);
        var handler = serviceFactory(description.HandlerType!);
        try
        {
            handler?.GetType().GetMethod(HandleMethodName)?.Invoke(handler, new object[] { payload });
        }
        catch (Exception e)
        {
            throw new PacketLossException(e);
        }
        
    }
}
