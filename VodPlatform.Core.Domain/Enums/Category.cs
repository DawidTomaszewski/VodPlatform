namespace VodPlatform.Core.Domain.Enums
{
    [Flags]
    public enum Category
    {
        Action = 1,
        Adventure = 2,
        Animation = 4,
        Biography = 8,
        Comedy = 16,
        Crime = 32,
        Documentary = 64,
        Drama = 128,
        Fantasy = 256,
        Historical = 512,
        Horror = 1024,
        Musical = 2048,
        Mystery = 4096,
        Romance = 8192,
        ScienceFiction = 16384,
        Sports = 32768,
        Thriller = 65536,
        War = 131072,
        Western = 262144,
        Family = 524288,
        Reality = 1048576,
        GameShow = 2097152,
        TalkShow = 4194304
    }
}
