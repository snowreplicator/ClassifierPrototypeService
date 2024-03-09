namespace Prototype.ClassifierPrototypeService.Bll.Common;

public static class Error
{
    #region O - OBJ - объекты (ошибки CRUD операций)
    
    /// <summary>
    /// Не найдена запись 
    /// </summary>
    public const string O100MovieNotFound = "ClassifierPrototypeService_Errors.O100";
    
    /// <summary>
    /// Запись не может быть создана
    /// </summary>
    public const string O101MovieCouldNotBeCreated = "ClassifierPrototypeService_Errors.O101";
    
    /// <summary>
    /// Запись не может быть изменена
    /// </summary>
    public const string O102MovieCouldNotBeUpdated = "ClassifierPrototypeService_Errors.O102";
    
    /// <summary>
    /// Запись не может быть удалена
    /// </summary>
    public const string O103MovieCouldNotBeDeleted = "ClassifierPrototypeService_Errors.O103";
    
    #endregion
    
    
    #region S - SYS - системные ошибки

    /// <summary>
    /// Ошибка обработки команды/запроса
    /// </summary>
    public const string S100ErrorHandlingRequest = "ClassifierPrototypeService_Errors.S100";

    /// <summary>
    /// Ошибка операции
    /// </summary>
    public const string S101DoubleTransaction = "ClassifierPrototypeService_Errors.S101";
    
    /// <summary>
    /// Неверные параметры настроек сервиса
    /// </summary>
    public const string S102WrongRequestContext = "ClassifierPrototypeService_Errors.S102";
    
    #endregion
}
