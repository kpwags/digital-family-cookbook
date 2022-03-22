using DigitalFamilyCookbook.Data.Repositories;
using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Commands.System;

public class RefreshInvitationCode
{
    public class Handler : IRequestHandler<Command, OperationResult<SiteSettingsApiModel>>
    {
        private readonly ISystemRepository _systemRepository;

        public Handler(ISystemRepository systemRepository)
        {
            _systemRepository = systemRepository;
        }

        public async Task<OperationResult<SiteSettingsApiModel>> Handle(Command command, CancellationToken cancellationToken)
        {
            try
            {
                var settings = await _systemRepository.RegnerateInvitationCode();

                return new OperationResult<SiteSettingsApiModel>(SiteSettingsApiModel.FromDomainModel(settings));
            }
            catch (Exception ex)
            {
                return new OperationResult<SiteSettingsApiModel>(ex.Message);
            }
        }
    }

    public class Command : IRequest<OperationResult<SiteSettingsApiModel>>
    {

    }
}