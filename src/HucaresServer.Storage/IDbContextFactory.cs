namespace HucaresServer.Storage
{
    public interface IDbContextFactory
    {
        /// <summary>
        /// Builds and returns a new instance of HucaresContext.
        /// </summary>
        /// <returns>A new instance of HucaresContext.</returns>
        HucaresContext BuildHucaresContext();
    }
}