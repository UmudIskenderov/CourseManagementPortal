using CourseManagementPortalEntities.Entities;
using CourseManagementPortalEntities.Interfaces;
using CourseManagementPortalWebUI.Models;
using CourseManagementPortalWebUI.Models.Interface;

namespace CourseManagementPortalWebUI.Mappers.Interfaces
{
    public interface IBaseMapper<TEntity, TModel> where TEntity : IEntity
                                                  where TModel : IModel, new()
    {
        TModel Map(TEntity entity);
        TEntity Map(TModel model);
    }
}
