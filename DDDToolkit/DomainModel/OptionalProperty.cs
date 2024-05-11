namespace DDDToolkit
{
    /// <summary>
    /// 値が存在しない可能性があるプロパティをNullにしないためのクラス。IsNoneがtrueの場合は値がない状態。
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public sealed class OptionalProperty<TValue>
    {
        /// <summary>
        /// 値の本体。非公開。
        /// </summary>
        private TValue? _value;

        /// <summary>
        /// コンストラクタ。非公開。
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="isNone">値が空かどうか</param>
        private OptionalProperty(TValue? value, bool isNone)
        {
            _value = value;
            IsNone = isNone;
        }

        /// <summary>
        /// 値を作成する。
        /// </summary>
        /// <param name="value">値。Nullの場合はNoneになる。</param>
        /// <returns></returns>
        public static OptionalProperty<TValue> Create(TValue? value)
        {
            if (value is null)
            {
                return None();
            }
            return Some(value);
        }

        /// <summary>
        /// Nullでない条件を渡して値を作成する。
        /// </summary>
        /// <param name="value">値。Nullは渡さないでください。</param>
        /// <param name="isSome">値がNullでない条件</param>
        /// <returns></returns>
        public static OptionalProperty<TValue> Create(TValue value, bool isSome)
        {
            if (!isSome || value is not null)
            {
                return None();
            }
            return Some(value);
        }

        /// <summary>
        /// Nullでない条件を返す関数と値の生成関数をもとに値を作成する。
        /// </summary>
        /// <param name="getValue">値を作成する関数</param>
        /// <param name="isSome">Nullでない条件を返す関数</param>
        /// <returns></returns>
        public static OptionalProperty<TValue> Create(Func<TValue> getValue, Func<bool> isSome)
        {
            if (isSome())
            {
                var value = getValue();
                if(value is null)
                {
                    return None();
                }
                return Some(value);
            }
            return None();
        }

        /// <summary>
        /// Nullでない条件を返す関数をもとに値を作成する。
        /// </summary>
        /// <param name="value">値。Nullは渡さないでください。</param>
        /// <param name="isSome">Nullでない条件を返す関数<</param>
        /// <returns></returns>
        public static OptionalProperty<TValue> Create(TValue value, Func<bool> isSome)
        {
            if (!isSome() || value is null)
            {
                return None();
            }
            return Some(value);
        }

        /// <summary>
        /// 値を取り出す。値がNullの場合、例外を投げるので先にIsNoneをチェックしてください。
        /// </summary>
        /// <returns>値</returns>
        /// <exception cref="ArgumentNullException">値がNullの場合、例外を投げるので先にIsNoneをチェックしてください</exception>
        public TValue GetValue()
        {
            if (IsNone || _value is null)
            {
                throw new ArgumentNullException();
            }
            return _value;
        }

        /// <summary>
        /// 値がない状態で生成する
        /// </summary>
        /// <returns></returns>
        public static OptionalProperty<TValue> None() { return new OptionalProperty<TValue>(default, true); }

        /// <summary>
        /// 値がある状態で生成する（非公開）
        /// </summary>
        /// <param name="value">値。Nullを渡さないでください。</param>
        /// <returns></returns>
        private static OptionalProperty<TValue> Some(TValue value) { return new OptionalProperty<TValue>(value, false); }
        
        /// <summary>
        /// 値が空かどうか
        /// </summary>
        public bool IsNone { get; private set; }
    }
}
