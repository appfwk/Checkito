using MediatR;
using Microsoft.AspNetCore.Components;

namespace Checkito.Web.General.Components
{
    public class ScopedComponentBase : OwningComponentBase
    {
        private IMediator _mediator = default!;

        protected bool IsBussy { get; set; }

        protected IMediator Mediator
        {
            get
            {
                if (_mediator == null)
                {
                    _mediator = GetService<IMediator>();
                }

                return _mediator;
            }
        }

        protected TService GetService<TService>() where TService : notnull
        {
            return ScopedServices.GetRequiredService<TService>();
        }
    }
}

