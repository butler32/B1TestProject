namespace B1TestProject.Interfaces
{
    public interface ICancellationTokenService
    {
        CancellationToken GetToken();
        void Cancel();
    }
}
