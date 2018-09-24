namespace HucaresServer.Storage
{
    public interface IDbContextFactory
    {
        HucaresContext BuildHucaresContext();
    }
}