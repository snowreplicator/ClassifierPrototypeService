namespace Prototype.ClassifierPrototypeService.Bll.Common;

/// <summary>
/// Error codes constants
///     S - SYS - системные ошибки
/// </summary>
public static class Error
{
    #region S - SYS - системные ошибки

    /// <summary>
    /// Ошибка обработки команды/запроса
    /// </summary>
    public const string S100ErrorHandlingRequest = "ClassifierPrototypeService_Errors.S100";

    /// <summary>
    /// Ошибка операции
    /// </summary>
    public const string S101DoubleTransaction = "ClassifierPrototypeService_Errors.S101";
    
    #endregion
}
