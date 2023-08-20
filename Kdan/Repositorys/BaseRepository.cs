using Microsoft.EntityFrameworkCore; 
using Kdan.Context;
using Kdan.Repositorys.Interface;

namespace Kdan.Repositorys
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly KdanContext _kdanContext;
        public BaseRepository(KdanContext kdanContext)
        {
            _kdanContext = kdanContext;
        }
        /// <summary>
        /// 新增物件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task Create(TEntity entity)
        {
            await _kdanContext.AddAsync(entity);
            await _kdanContext.SaveChangesAsync();
        }
        /// <summary>
        /// 新增多個物件
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task CreateRange(IEnumerable<TEntity> entities)
        {
            await _kdanContext.AddRangeAsync(entities);
            await _kdanContext.SaveChangesAsync();
        }
        /// <summary>
        /// 更新物件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task Update(TEntity entity)
        {
            _kdanContext.Entry(entity).State = EntityState.Modified;
            await _kdanContext.SaveChangesAsync();
        }
        /// <summary>
        /// 刪除物件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task Delete(TEntity entity)
        {
            _kdanContext.Remove(entity);
            await _kdanContext.SaveChangesAsync();
        }
        /// <summary>
        /// 刪除多個物件
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task DeleteRange(IEnumerable<TEntity> entities)
        {
            _kdanContext.RemoveRange(entities);
            await _kdanContext.SaveChangesAsync();
        }
    }
}
