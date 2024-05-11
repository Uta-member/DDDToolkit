namespace DDDToolkit
{
    /// <summary>
    /// 更新時の値の更新要否を表すクラス
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public sealed class OptionalParam<TValue>
    {
        /// <summary>
        /// 値の本体。非公開。
        /// </summary>
        private TValue? _value;

        /// <summary>
        /// 値が空かどうか
        /// </summary>
        public bool IsNone { get; private set; }

        /// <summary>
        /// コンストラクタ。非公開。
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="isNone">値が空かどうか</param>
        private OptionalParam(TValue? value, bool isNone)
        {
            _value = value;
            IsNone = isNone;
        }

        /// <summary>
        /// 値を作成する。
        /// </summary>
        /// <param name="value">値。Nullの場合はNoneになる。</param>
        /// <returns></returns>
        public static OptionalParam<TValue> Create(TValue? value)
        {
            if (value is null)
            {
                return None();
            }
            return Some(value);
        }

        /// <summary>
        /// 更新前の値と比較して値を生成する。もしprevValueとvalueが同じなら値が不要な状態として扱う。
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="prevValue">更新前の値</param>
        /// <returns></returns>
        public static OptionalParam<TValue> Create(TValue? value, TValue? prevValue)
        {
            if (value is null || value.Equals(prevValue))
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
        public static OptionalParam<TValue> Create(TValue value, bool isSome)
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
        public static OptionalParam<TValue> Create(Func<TValue> getValue, Func<bool> isSome)
        {
            if (isSome())
            {
                var value = getValue();
                if (value is null)
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
        public static OptionalParam<TValue> Create(TValue value, Func<bool> isSome)
        {
            if (!isSome() || value is null)
            {
                return None();
            }
            return Some(value);
        }

        /// <summary>
        /// 値を取り出す。もし値がない場合はdefaultValueを返す。
        /// </summary>
        /// <param name="defaultValue">値がない時に返す値</param>
        /// <returns></returns>
        public TValue GetValue(TValue defaultValue)
        {
            if (IsNone || _value is null)
            {
                return defaultValue;
            }
            return _value;
        }

        /// <summary>
        /// 値がない状態で生成する
        /// </summary>
        /// <returns></returns>
        public static OptionalParam<TValue> None() { return new OptionalParam<TValue>(default, true); }

        /// <summary>
        /// 値がある状態で生成する（非公開）
        /// </summary>
        /// <param name="value">値。Nullを渡さないでください。</param>
        /// <returns></returns>
        private static OptionalParam<TValue> Some(TValue value) { return new OptionalParam<TValue>(value, false); }
    }
}
