using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AgileControl.API.Endpoints.Controllers;

[Route("api/v1/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UserController(IMapper mapper,
        IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
}
