namespace DigitalFamilyCookbook.Handlers.Commands.Recipes;

public class UploadRecipeImage
{
    public class Handler : IRequestHandler<Command, OperationResult<ImageUploadResponseApiModel>>
    {
        private readonly IFileService _fileService;

        public Handler(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<OperationResult<ImageUploadResponseApiModel>> Handle(Command command, CancellationToken cancellationToken)
        {
            try
            {
                if (command.Image is null)
                {
                    return new OperationResult<ImageUploadResponseApiModel>("No Image Uploaded");
                }

                var uploadResult = await _fileService.SaveRecipeImage(command.Image);

                return new OperationResult<ImageUploadResponseApiModel>(true, new ImageUploadResponseApiModel
                {
                    Filename = uploadResult.Filename,
                    ImageData = uploadResult.Image,
                    SecondImageData = uploadResult.LargeImage,
                });
            }
            catch (Exception ex)
            {
                return new OperationResult<ImageUploadResponseApiModel>(ex.Message);
            }
        }
    }

    public class Command : IRequest<OperationResult<ImageUploadResponseApiModel>>
    {
        public IFormFile? Image { get; set; }
    }
}