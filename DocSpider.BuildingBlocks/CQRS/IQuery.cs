﻿using MediatR;

namespace DocSpider.BuildingBlocks.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
        where TResponse : notnull
{

}
