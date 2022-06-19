namespace DigitalFamilyCookbook.Handlers.Queries.Recipes;

public class GetRecipeImage
{
    public class Handler : IRequestHandler<Query, OperationResult<string>>
    {
        private readonly IFileService _fileService;

        public Handler(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<OperationResult<string>> Handle(Query query, CancellationToken cancellationToken)
        {
            try
            {
                var image = await Task.FromResult(_fileService.GetRecipeImage(query.Filename));

                if (image.Length == 0)
                {
                    return new OperationResult<string>(false, "", "Unable to retrieve image");
                }

                return new OperationResult<string>(true, image);
            }
            catch (Exception ex)
            {
                return new OperationResult<string>(false, "", ex.Message);
            }
        }
    }

    public class Query : IRequest<OperationResult<string>>
    {
        public string Filename { get; set; } = string.Empty;
    }
}