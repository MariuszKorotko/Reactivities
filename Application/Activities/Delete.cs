using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
            public class Handler : IRequestHandler<Command, Result<Unit>>
            {
                private readonly DataContext _context;

                public Handler(DataContext context)
                {
                    _context = context;
                }
                public async Task<Result<Unit>> Handle(Command request, CancellationToken ct)
                {
                    var activity = await _context.Activities.FindAsync(request.Id);

                    // if (activity is null) return null;

                    _context.Remove(activity);

                    var success = await _context.SaveChangesAsync() > 0;

                    return success ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to delete the activity");

                }
            }
        }
    }
}