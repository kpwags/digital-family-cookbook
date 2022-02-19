using DigitalFamilyCookbook.Data.Repositories;
using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Commands.System;

public class SaveSiteSettings
{
    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ISystemRepository _systemRepository;

        public Handler(ISystemRepository systemRepository)
        {
            _systemRepository = systemRepository;
        }

        public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
        {
            await _systemRepository.SaveSiteSettings(new SiteSettings
            {
                Title = command.Title,
                IsPublic = command.IsPublic,
                AllowPublicRegistration = command.AllowPublicRegistration,
            });

            return Unit.Value;
        }
    }

    public class Command : IRequest<Unit>
    {
        public string Title { get; set; } = string.Empty;

        public bool IsPublic { get; set; }

        public bool AllowPublicRegistration { get; set; }
    }
}