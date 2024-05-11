namespace DDDToolkit.DomainModel
{
    /// <summary>
    /// 値オブジェクトの基底クラス
    /// </summary>
    /// <typeparam name="TValue">値の型</typeparam>
    public abstract record ValueObjectBase<TValue> : IValueObject<TValue>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value">値</param>
        public ValueObjectBase(TValue value)
        {
            CheckNull(value);
            Invalidate(value);
            Value = value;
        }

        /// <summary>
        /// 値オブジェクトはNull値を許容しないこととして扱うため、Nullチェックを行う
        /// </summary>
        /// <param name="value">値</param>
        /// <exception cref="ArgumentNullException">値がNull</exception>
        private void CheckNull(TValue value)
        {
            if (value is null)
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// 値のバリデーション。
        /// </summary>
        /// <param name="value">値</param>
        protected abstract void Invalidate(TValue value);

        /// <summary>
        /// 値の本体
        /// </summary>
        public TValue Value { get; }
    }
}
