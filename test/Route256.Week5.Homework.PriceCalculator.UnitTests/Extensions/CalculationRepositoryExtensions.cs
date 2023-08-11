using System.Linq;
using System.Threading;
using System.Transactions;
using Moq;
using Route256.Week5.Homework.PriceCalculator.Dal.Entities;
using Route256.Week5.Homework.PriceCalculator.Dal.Migrations;
using Route256.Week5.Homework.PriceCalculator.Dal.Models;
using Route256.Week5.Homework.PriceCalculator.Dal.Repositories.Interfaces;
using Route256.Week5.Homework.PriceCalculator.UnitTests.Comparers;
using Route256.Week5.Homework.PriceCalculator.UnitTests.Fakers;

namespace Route256.Week5.Homework.PriceCalculator.UnitTests.Extensions;

public static class CalculationRepositoryExtensions
{
    public static Mock<ICalculationRepository> SetupAddCalculations(
        this Mock<ICalculationRepository> repository,
        long[] ids)
    {
        repository.Setup(p =>
                p.Add(It.IsAny<CalculationEntityV1[]>(), 
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(ids);

        return repository;
    }
    
    public static Mock<ICalculationRepository> SetupCreateTransactionScope(
        this Mock<ICalculationRepository> repository)
    {
        repository.Setup(p =>
                p.CreateTransactionScope(It.IsAny<IsolationLevel>()))
            .Returns(new TransactionScope());

        return repository;
    }
    
    public static Mock<ICalculationRepository> SetupQueryCalculation(
        this Mock<ICalculationRepository> repository,
        CalculationEntityV1[] calculations)
    {
        repository.Setup(p =>
                p.Query(It.IsAny<CalculationHistoryQueryModel>(), 
                        It.IsAny<CancellationToken>()))
            .ReturnsAsync(calculations);

        return repository;
    }

    public static Mock<ICalculationRepository> VerifyAddWasCalledOnce(
        this Mock<ICalculationRepository> repository,
        CalculationEntityV1[] calculations)
    {
        repository.Verify(p =>
                p.Add(
                    It.Is<CalculationEntityV1[]>(x => x.SequenceEqual(calculations, new CalculationEntityV1Comparer())),
                    It.IsAny<CancellationToken>()),
            Times.Once);

        return repository;
    }
    
    public static Mock<ICalculationRepository> VerifyQueryWasCalledOnce(
        this Mock<ICalculationRepository> repository,
        CalculationHistoryQueryModel query)
    {
        repository.Verify(p =>
                p.Query(
                    It.Is<CalculationHistoryQueryModel>(x => x == query),
                    It.IsAny<CancellationToken>()),
            Times.Once);
        
        return repository;
    }
    
    public static Mock<ICalculationRepository> VerifyCreateTransactionScopeWasCalledOnce(
        this Mock<ICalculationRepository> repository,
        IsolationLevel isolationLevel)
    {
        repository.Verify(p =>
                p.CreateTransactionScope(
                    It.Is<IsolationLevel>(x => x == isolationLevel)),
            Times.Once);
        
        return repository;
    }

    public static Mock<ICalculationRepository> SetupDeleteCalculation(
        this Mock<ICalculationRepository> repository,
        long[] goodIds)
    {
        repository.Setup(p =>
                p.Delete(It.IsAny<DeleteHistoryQueryModel>(),
                        It.IsAny<CancellationToken>()))
            .ReturnsAsync(goodIds);

        repository.Setup(p =>
                p.ExistIdsForNotCurUser(
                    It.IsAny<DeleteHistoryQueryModel>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ExistIdsForNotCurUserModel(new long[0]));

        repository.Setup(p =>
                p.ExistIdsInDB(
                    It.IsAny<DeleteHistoryQueryModel>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        return repository;
    }

    public static Mock<ICalculationRepository> VerifyDeleteWasCalledOnce(
        this Mock<ICalculationRepository> repository,
        DeleteHistoryQueryModel query)
    {
        repository.Verify(p =>
                p.Delete(
                    It.Is<DeleteHistoryQueryModel>(x => x == query),
                    It.IsAny<CancellationToken>()),
            Times.Once);

        repository.Verify(p =>
                p.ExistIdsForNotCurUser(
                    It.Is<DeleteHistoryQueryModel>(x => x == query),
                    It.IsAny<CancellationToken>()),
            Times.Once);

        repository.Verify(p =>
                p.ExistIdsInDB(
                    It.Is<DeleteHistoryQueryModel>(x => x == query),
                    It.IsAny<CancellationToken>()),
            Times.Once);

        return repository;
    }

    public static Mock<ICalculationRepository> SetupOneOrManyCalculationsToAnotherUserExeption(
        this Mock<ICalculationRepository> repository,
        long[] goodIds)
    {
        repository.Setup(p =>
                p.Delete(It.IsAny<DeleteHistoryQueryModel>(),
                        It.IsAny<CancellationToken>()))
            .ReturnsAsync(goodIds);

        repository.Setup(p =>
                p.ExistIdsForNotCurUser(
                    It.IsAny<DeleteHistoryQueryModel>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ExistIdsForNotCurUserModel(goodIds));

        repository.Setup(p =>
                p.ExistIdsInDB(
                    It.IsAny<DeleteHistoryQueryModel>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        return repository;
    }

    public static Mock<ICalculationRepository> SetupOneOrManyCalculationsNotFoundException(
        this Mock<ICalculationRepository> repository,
        long[] goodIds)
    {
        repository.Setup(p =>
                p.Delete(It.IsAny<DeleteHistoryQueryModel>(),
                        It.IsAny<CancellationToken>()))
            .ReturnsAsync(goodIds);

        repository.Setup(p =>
                p.ExistIdsForNotCurUser(
                    It.IsAny<DeleteHistoryQueryModel>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ExistIdsForNotCurUserModel(new long[0]));

        repository.Setup(p =>
                p.ExistIdsInDB(
                    It.IsAny<DeleteHistoryQueryModel>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        return repository;
    }
}