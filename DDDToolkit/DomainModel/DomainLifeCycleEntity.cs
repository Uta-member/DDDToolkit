namespace DDDToolkit
{
    /// <summary>
    /// ドメインライフサイクルエンティティ
    /// </summary>
    /// <typeparam name="TUserGuid">ユーザIDの型</typeparam>
    public sealed class DomainLifeCycleEntity<TUserGuid> : IEntity
    {
        private DomainLifeCycleEntity(
            TUserGuid insertedUserGuid,
            DateTime insertedDateTime,
            OptionalProperty<TUserGuid> updatedUserGuid,
            OptionalProperty<DateTime> updatedDateTime,
            DomainLifeCycle lifeCycle)
        {
            InsertedUserGuid = insertedUserGuid;
            InsertedDateTime = insertedDateTime;
            UpdatedUserGuid = updatedUserGuid;
            UpdatedDateTime = updatedDateTime;
            LifeCycle = lifeCycle;
        }

        public static DomainLifeCycleEntity<TUserGuid> Create(DomainLifeCycleCreateCommand<TUserGuid> command)
        {
            return new DomainLifeCycleEntity<TUserGuid>(
                command.InsertedUserGuid,
                DateTime.Now,
                OptionalProperty<TUserGuid>.None(),
                OptionalProperty<DateTime>.None(),
                DomainLifeCycle.Create);
        }

        public DomainLifeCycleEntity<TUserGuid> Delete(DomainLifeCycleDeleteCommand<TUserGuid> command)
        {
            return new DomainLifeCycleEntity<TUserGuid>(
                InsertedUserGuid,
                InsertedDateTime,
                OptionalProperty<TUserGuid>.Create(command.UpdatedUserGuid),
                OptionalProperty<DateTime>.Create(DateTime.Now),
                DomainLifeCycle.Delete);
        }

        public static DomainLifeCycleEntity<TUserGuid> Recreate(DomainLifeCycleRecreateCommand<TUserGuid> command)
        {
            return new DomainLifeCycleEntity<TUserGuid>(
                command.InsertedUserGuid,
                command.InsertedDateTime,
                command.UpdatedUserGuid,
                command.UpdatedDateTime,
                DomainLifeCycle.Read);
        }

        public DomainLifeCycleEntity<TUserGuid> Update(DomainLifeCycleUpdateCommand<TUserGuid> command)
        {
            return new DomainLifeCycleEntity<TUserGuid>(
                InsertedUserGuid,
                InsertedDateTime,
                OptionalProperty<TUserGuid>.Create(command.UpdatedUserGuid),
                OptionalProperty<DateTime>.Create(DateTime.Now),
                DomainLifeCycle.Update);
        }

        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime InsertedDateTime { get; init; }
        /// <summary>
        /// 作成ユーザID
        /// </summary>
        public TUserGuid InsertedUserGuid { get; init; }
        /// <summary>
        /// ライフサイクル
        /// </summary>
        public DomainLifeCycle LifeCycle { get; init; }
        /// <summary>
        /// 更新日時
        /// </summary>
        public OptionalProperty<DateTime> UpdatedDateTime { get; init; }
        /// <summary>
        /// 更新ユーザID
        /// </summary>
        public OptionalProperty<TUserGuid> UpdatedUserGuid { get; init; }
    }

    /// <summary>
    /// ライフサイクルの種類
    /// </summary>
    public enum DomainLifeCycle
    {
        /// <summary>
        /// 新規作成
        /// </summary>
        Create,
        /// <summary>
        /// 読み取り
        /// </summary>
        Read,
        /// <summary>
        /// 更新
        /// </summary>
        Update,
        /// <summary>
        /// 削除
        /// </summary>
        Delete,
    }
}
