using DigitalFamilyCookbook.Data.Repositories;
using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Commands.System;

public class SaveSiteSettings
{
    public class Handler : IRequestHandler<Command, OperationResult<string>>
    {
        private const string EmptyTextValue = "<p><br></p>";
        private readonly ISystemRepository _systemRepository;

        public Handler(ISystemRepository systemRepository)
        {
            _systemRepository = systemRepository;
        }

        public async Task<OperationResult<string>> Handle(Command command, CancellationToken cancellationToken)
        {
            try
            {
                await _systemRepository.SaveSiteSettings(new SiteSettings
                {
                    Title = command.Title,
                    IsPublic = command.IsPublic,
                    LandingPageText = command.LandingPageText == EmptyTextValue ? string.Empty : command.LandingPageText,
                    AllowPublicRegistration = command.AllowPublicRegistration,
                });

                return new OperationResult<string>(true, "");
            }
            catch (Exception ex)
            {
                return new OperationResult<string>(false, "", ex.Message);
            }
        }
    }

    public class Command : IRequest<OperationResult<string>>
    {
        public string Title { get; set; } = string.Empty;

        public string LandingPageText { get; set; } = string.Empty;

        public bool IsPublic { get; set; }

        public bool AllowPublicRegistration { get; set; }
    }
}