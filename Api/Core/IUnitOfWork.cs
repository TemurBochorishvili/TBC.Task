﻿namespace Api.Core;

public interface IUnitOfWork
{
    Task Complete();
}
