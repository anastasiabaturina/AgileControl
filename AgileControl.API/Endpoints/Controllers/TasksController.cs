using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AgileControl.API.Endpoints.Controllers;

[Route("api/v1/tasks")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public TasksController(IMapper mapper,
        IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
}
