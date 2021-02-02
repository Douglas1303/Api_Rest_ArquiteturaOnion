namespace Infra.CrossCutting.Core.CQRS
{
    public static class CommandResultExtensions
    {
        public static void AddErrorMessage(this IResult cmd, string errorMessage)
        {
            ((CommandResult)cmd).AddErrorMessage(errorMessage);
        }

        public static void AddSecondaryProcessInfo(this IResult cmd, string messageInfo)
        {
            ((BaseResult)cmd).AddSecondaryProcessInfo(messageInfo);
        }

        public static QueryResult ToQueryResult(this IResult iResult)
        {
            return iResult as QueryResult;
        }
    }
}