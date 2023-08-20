namespace Kdan.Repositorys.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 新增物件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task Create(TEntity entity);
        /// <summary>
        /// 新增多個物件
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public Task CreateRange(IEnumerable<TEntity> entities);
        /// <summary>
        /// 更新物件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task Update(TEntity entity);
        /// <summary>
        /// 刪除物件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task Delete(TEntity entity);
        /// <summary>
        /// 刪除多個物件
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public Task DeleteRange(IEnumerable<TEntity> entities);
    }
}
