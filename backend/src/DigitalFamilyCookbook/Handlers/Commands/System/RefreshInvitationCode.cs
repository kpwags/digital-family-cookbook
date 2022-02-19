using DigitalFamilyCookbook.Data.Repositories;
using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Commands.System;

public class RefreshInvitationCode
{
    public class Handler : IRequestHandler<Command, SiteSettingsApiModel>
    {
        private readonly ISystemRepository _systemRepository;

        public Handler(ISystemRepository systemRepository)
        {
            _systemRepository = systemRepository;
        }

        public async Task<SiteSettingsApiModel> Handle(Command command, CancellationToken cancellationToken)
        {
            var settings = await _systemRepository.RegnerateInvitationCode();

            return SiteSettingsApiModel.FromDomainModel(settings);
        }
    }

    public class Command : IRequest<SiteSettingsApiModel>
    {

    }
}