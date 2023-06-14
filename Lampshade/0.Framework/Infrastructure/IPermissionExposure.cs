namespace _0.Framework.Infrastructure
{
    public interface IPermissionExposure
    {
        Dictionary<string, List<PermissionDto>> Expose();
    }
}
