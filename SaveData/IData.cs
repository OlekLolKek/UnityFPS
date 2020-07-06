public interface IData<T>
{
    #region Methods

    void Save(T data, string path = null);

    T Load(string path = null);

    void SetOptions(string path);

    #endregion
}
