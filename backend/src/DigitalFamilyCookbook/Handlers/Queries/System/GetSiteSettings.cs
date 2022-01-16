using DigitalFamilyCookbook.Data.Repositories;
using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Queries.System;

public class GetSiteSettings
{
    public class Handler : IRequestHandler<Query, OperationResult<SiteSettingsApiModel>>
    {
        private readonly ISystemRepository _systemRepository;

        public Handler(ISystemRepository systemRepository)
        {
            _systemRepository = systemRepository;
        }

        public async Task<OperationResult<SiteSettingsApiModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            var settings = await Task.FromResult(_systemRepository.GetSiteSettings(1));

            if (settings.SiteSettingsId == 0)
            {
                return new OperationResult<SiteSettingsApiModel>("Unable to get site settings");
            }

            return new OperationResult<SiteSettingsApiModel>(SiteSettingsApiModel.FromDomainModel(settings));
        }
    }

    public class Query : IRequest<OperationResult<SiteSettingsApiModel>>
    {

    }
}
