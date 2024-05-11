namespace DDDToolkit.DomainModel
{
    /// <summary>
    /// ドメインライフサイクルの生成コマンド
    /// </summary>
    /// <typeparam name="TUserGuid"></typeparam>
    public sealed class DomainLifeCycleCreateCommand<TUserGuid>
    {
        public DomainLifeCycleCreateCommand(TUserGuid insertedUserGuid) { InsertedUserGuid = insertedUserGuid; }

        public TUserGuid InsertedUserGuid { get; }
    }

    /// <summary>
    /// ドメインライフサイクルをリポジトリから再構築するコマンド
    /// </summary>
    /// <typeparam name="TUserGuid"></typeparam>
    public sealed class DomainLifeCycleRecreateCommand<TUserGuid>
    {
        public DomainLifeCycleRecreateCommand(
            TUserGuid insertedUserGuid,
            DateTime insertedDateTime,
            OptionalProperty<TUserGuid> updatedUserGuid,
            OptionalProperty<DateTime> updatedDateTime)
        {
            InsertedUserGuid = insertedUserGuid;
            InsertedDateTime = insertedDateTime;
            UpdatedUserGuid = updatedUserGuid;
            UpdatedDateTime = updatedDateTime;
        }

        public DateTime InsertedDateTime { get; }

        public TUserGuid InsertedUserGuid { get; }

        public OptionalProperty<DateTime> UpdatedDateTime { get; }

        public OptionalProperty<TUserGuid> UpdatedUserGuid { get; }
    }

    /// <summary>
    /// ドメインライフサイクルの更新コマンド
    /// </summary>
    /// <typeparam name="TUserGuid"></typeparam>
    public sealed class DomainLifeCycleUpdateCommand<TUserGuid>
    {
        public DomainLifeCycleUpdateCommand(TUserGuid updatedUserGuid) { UpdatedUserGuid = updatedUserGuid; }

        public TUserGuid UpdatedUserGuid { get; }
    }

    /// <summary>
    /// ドメインライフサイクルの削除コマンド
    /// </summary>
    /// <typeparam name="TUserGuid"></typeparam>
    public class DomainLifeCycleDeleteCommand<TUserGuid>
    {
        public DomainLifeCycleDeleteCommand(TUserGuid updatedUserGuid) { UpdatedUserGuid = updatedUserGuid; }

        public TUserGuid UpdatedUserGuid { get; }
    }
}
