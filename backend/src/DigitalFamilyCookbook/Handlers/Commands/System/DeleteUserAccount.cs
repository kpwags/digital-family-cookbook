using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Commands.System;

public class DeleteUserAccount
{
    public class Handler : IRequestHandler<Command, OperationResult<string>>
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public Handler(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public async Task<OperationResult<string>> Handle(Command command, CancellationToken cancellationToken)
        {
            try
            {
                await _userAccountRepository.DeleteUserAccount(command.Id);
            }
            catch (Exception ex)
            {
                return new OperationResult<string>(false, "", ex.Message);
            }

            return new OperationResult<string>(true, "");
        }
    }

    public class Command : IRequest<OperationResult<string>>
    {
        public string Id { get; set; } = string.Empty;
    }
}