using System.Threading;

namespace DigitalFamilyCookbook.Handlers.Commands.System;

public class DeleteUserAccount
{
    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public Handler(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
        {
            await _userAccountRepository.DeleteUserAccount(command.Id);

            return Unit.Value;
        }
    }

    public class Command : IRequest<Unit>
    {
        public string Id { get; set; } = string.Empty;
    }
}